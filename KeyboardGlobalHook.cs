using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKeyChanger {
    // ref
    // http://qiita.com/exliko/items/3135e4413a6da067b35d
    // http://inputsimulator.codeplex.com/
    // https://stackoverflow.com/questions/12761169/send-keys-through-sendinput-in-user32-dll
    // http://nonsoft.la.coocan.jp/SoftSample/CS.NET/SampleSendInput.html
    // http://d.hatena.ne.jp/ken_2501jp/20130406/1365235955

    /// <summary>
    /// Keyboard Global Hook
    /// </summary>
    public static class KeyboardGlobalHook {

        #region Declaration - Dll
        private static class NativeMethods {
            // callback 
            public delegate IntPtr KeyboardGlobalHookCallback(int code, uint msg, ref KBDLLHOOKSTRUCT hookData);

            // https://msdn.microsoft.com/ja-jp/library/cc430103.aspx
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardGlobalHookCallback lpfn, IntPtr hMod, uint dwThreadId);

            // https://msdn.microsoft.com/ja-jp/library/cc429591.aspx
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, uint msg, ref KBDLLHOOKSTRUCT hookData);

            // https://msdn.microsoft.com/ja-jp/library/cc430120.aspx
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
            public extern static int MapVirtualKey(int wCode, int wMapType);

            // https://msdn.microsoft.com/ja-jp/library/cc411004.aspx
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public extern static void SendInput(int nInputs, Input[] pInputs, int cbsize);

            // https://msdn.microsoft.com/ja-jp/library/cc364822.aspx
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

            // https://msdn.microsoft.com/ja-jp/library/cc364746.aspx
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern IntPtr GetMessageExtraInfo();
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        private static IntPtr _keyEventHandle;
        private static event NativeMethods.KeyboardGlobalHookCallback _hookCallback;

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public struct Input {
            [System.Runtime.InteropServices.FieldOffset(0)]
            public int Type;

            [System.Runtime.InteropServices.FieldOffset(4)]
            public MouseInput Mouse;

            [System.Runtime.InteropServices.FieldOffset(4)]
            public KeyboardInput Keyboard;

            [System.Runtime.InteropServices.FieldOffset(4)]
            public HardwareInput Hardware;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct MouseInput {
            public int X;
            public int Y;
            public int Data;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        // https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms646271(v=vs.85).aspx
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct KeyboardInput {
            public short VirtualKey;    // 1 to 254. if thd Flags menmber specifes KEYEVENTF_UNICODE, VirtualKey must be 0.
            public ushort ScanCode;      // A hardware scan code for the key. If Flags specifies KEYEVENTF_UNICODE, wScan specifies a Unicode character which is to be sent to the foreground application.
            public int Flags;           // 
            public int Time;            // timestamp
            public int ExtraInfo;
        }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct HardwareInput {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        #endregion

        #region Declaration
        private static class Const {
            public const int Action = 0;            // 0のみフックするのがお約束らしい
            public const int HookTypeLL = 13;       // WH_KEYBOARD_LL
        }
        private static class InputType {
            public const int Mouse = 0;
            public const int Keyboard = 1;
            public const int Hardware = 2;
        }
        private static class KeyStroke {
            public const int KeyDown = 0x100;
            public const int KeyUp = 0x101;
            public const int SysKeyDown = 0x104;
            public const int SysKeyup = 0x105;
        }
        private static class Flags {
            public const int None = 0x00;
            public const int KeyDown = 0x00;
            public const int KeyUp = 0x02;
            public const int ExtendeKey = 0x01;     //  拡張コード(これを設定することで修飾キーも有効になる)
            public const int Unicode = 0x04;
            public const int ScanCode = 0x08;
        }
        private static class ExtraInfo {
            public const int SendKey = 1;
        }

        private class KeySet {
            public short VirtualKey;
            public ushort ScanCode;
            public int Flag;
            public KeySet(short virtualKey, ushort scanCode, int flag) {
                this.VirtualKey = virtualKey;
                this.ScanCode = scanCode;
                this.Flag = flag;
            }
        }
        private class KeySetInt {
            public short VirtualKey;
            public int ScanCode;
            public int Flag;
            public KeySetInt(short virtualKey, int scanCode, int flag) {
                this.VirtualKey = virtualKey;
                this.ScanCode = scanCode;
                this.Flag = flag;
            }
        }
        private static class ModifiedKey {
            public const int None = 0x00;
            public const int User1 = 0x01 << 1;
            public const int User2 = 0x01 << 2;
            public const int User3 = 0x01 << 3;
            public const int User4 = 0x01 << 4;

        }
        private static int _modified = ModifiedKey.None;

        // ScanCode Modified Key Pair
        private static Dictionary<ushort, byte> _modifiedKeys = new Dictionary<ushort, byte> {
            { ScanCode.Muhenkan, ModifiedKey.User1 },
            { ScanCode.Henkan, ModifiedKey.User2 },
            { ScanCode.F13, ModifiedKey.User3 },
            { ScanCode.AtMark, ModifiedKey.User4 },
        };

        // User1(Muhenkan)
        private static Dictionary<ushort, KeySet> _convertMapping1 = new Dictionary<ushort, KeySet> {
            // line 5
            { ScanCode.Num1, new KeySet( VirtualKey.F1, ScanCode.F1,Flags.None)},                           // F1
            { ScanCode.Num2, new KeySet( VirtualKey.F2, ScanCode.F2,Flags.None)},                           // F2
            { ScanCode.Num3, new KeySet( VirtualKey.F3, ScanCode.F3,Flags.None)},                           // F3
            { ScanCode.Num4, new KeySet( VirtualKey.F4, ScanCode.F4,Flags.None)},                           // F4
            { ScanCode.Num5, new KeySet( VirtualKey.F5, ScanCode.F5,Flags.None)},                           // F5
            { ScanCode.Num6, new KeySet( VirtualKey.F6, ScanCode.F6,Flags.None)},                           // F6
            { ScanCode.Num7, new KeySet( VirtualKey.F7, ScanCode.F7,Flags.None)},                           // F7
            { ScanCode.Num8, new KeySet( VirtualKey.F8, ScanCode.F8,Flags.None)},                           // F8
            { ScanCode.Num9, new KeySet( VirtualKey.F9, ScanCode.F9,Flags.None)},                           // F9
            { ScanCode.Num0, new KeySet( VirtualKey.F10, ScanCode.F10,Flags.None)},                         // F10
            { ScanCode.Minus, new KeySet( VirtualKey.F11, ScanCode.F11,Flags.None)},                        // F11
            { ScanCode.Caret, new KeySet( VirtualKey.F12, ScanCode.F12,Flags.None)},                        // F12

            // line 4
            { ScanCode.Q, new KeySet( VirtualKey.Num1, ScanCode.Num1,Flags.None)},                          // 1
            { ScanCode.W, new KeySet( VirtualKey.Num2, ScanCode.Num2,Flags.None)},                          // 2
            { ScanCode.E, new KeySet( VirtualKey.Num3, ScanCode.Num3,Flags.None)},                          // 3
            { ScanCode.R, new KeySet( VirtualKey.Num4, ScanCode.Num4,Flags.None)},                          // 4
            { ScanCode.T, new KeySet( VirtualKey.Num5, ScanCode.Num5,Flags.None)},                          // 5
            { ScanCode.Y, new KeySet( VirtualKey.Num6, ScanCode.Num6,Flags.None)},                          // 6
            { ScanCode.U, new KeySet( VirtualKey.Num7, ScanCode.Num7,Flags.None)},                          // 7
            { ScanCode.I, new KeySet( VirtualKey.Num8, ScanCode.Num8,Flags.None)},                          // 8
            { ScanCode.O, new KeySet( VirtualKey.Num9, ScanCode.Num9,Flags.None)},                          // 9
            { ScanCode.P, new KeySet( VirtualKey.Num0, ScanCode.Num0,Flags.None)},                          // 0

            // line 3
            { ScanCode.A, new KeySet( VirtualKey.Caret, ScanCode.Caret,Flags.None)},                        // へ
            { ScanCode.S, new KeySet( VirtualKey.BackSlash, ScanCode.BackSlash,Flags.None)},                // ろ
            { ScanCode.D, new KeySet( VirtualKey.BracketsR, ScanCode.BracketsR,Flags.None)},                // む
            { ScanCode.F, new KeySet( VirtualKey.Yen, ScanCode.Yen,Flags.None)},                            // ー
            { ScanCode.G, new KeySet( 0, 0x3063, Flags.Unicode)},                                           // っ
            { ScanCode.H, new KeySet( 0, 0x3092, Flags.Unicode)},

            { ScanCode.J, new KeySet( VirtualKey.Home, ScanCode.Home,Flags.ExtendeKey)},                    // Home
            { ScanCode.K, new KeySet( VirtualKey.End, ScanCode.End,Flags.ExtendeKey)},                      // End
            { ScanCode.L, new KeySet( VirtualKey.Enter, ScanCode.Enter,Flags.None)},                        // Enter 
            { ScanCode.SemiColon, new KeySet( VirtualKey.BackSpace, ScanCode.BackSpace,Flags.ExtendeKey)},  // BackSpace

            // line 2
            { ScanCode.Z, new KeySet( 0, 0x3092, Flags.Unicode)},                                           // を
            { ScanCode.X,  new KeySet( VirtualKey.Minus, ScanCode.Minus,Flags.None)},                       // ほ
            { ScanCode.C, new KeySet( VirtualKey.Astarsk, ScanCode.Astarsk,Flags.None)},                    // け
            { ScanCode.V, new KeySet( 0, 0x3063, Flags.Unicode)},                                           // っ

            { ScanCode.N, new KeySet( VirtualKey.Delete, ScanCode.Delete,Flags.ExtendeKey)},                // Delete
            { ScanCode.M, new KeySet( VirtualKey.PageUp, ScanCode.PageUp,Flags.ExtendeKey)},                // Page Up
            { ScanCode.LessThan, new KeySet( VirtualKey.PageDown, ScanCode.PageDown,Flags.ExtendeKey)},     // Page Down

            // line 1
        };

        // User2(Henkan)
        private static Dictionary<ushort, KeySet> _convertMapping2 = new Dictionary<ushort, KeySet> {
            // line 4
            { ScanCode.Q, new KeySet( VirtualKey.Num1, ScanCode.Num1,Flags.None)},                          // 1
            { ScanCode.W, new KeySet( VirtualKey.Num2, ScanCode.Num2,Flags.None)},                          // 2
            { ScanCode.E, new KeySet( VirtualKey.Num3, ScanCode.Num3,Flags.None)},                          // 3
            { ScanCode.R, new KeySet( VirtualKey.Num4, ScanCode.Num4,Flags.None)},                          // 4
            { ScanCode.T, new KeySet( VirtualKey.Num5, ScanCode.Num5,Flags.None)},                          // 5
            { ScanCode.Y, new KeySet( VirtualKey.Num6, ScanCode.Num6,Flags.None)},                          // 6
            { ScanCode.U, new KeySet( VirtualKey.Num7, ScanCode.Num7,Flags.None)},                          // 7
            { ScanCode.I, new KeySet( VirtualKey.Num8, ScanCode.Num8,Flags.None)},                          // 8
            { ScanCode.O, new KeySet( VirtualKey.Num9, ScanCode.Num9,Flags.None)},                          // 9
            { ScanCode.P, new KeySet( VirtualKey.Num0, ScanCode.Num0,Flags.None)},                          // 0

            // line 3
            { ScanCode.A, new KeySet( 0, 0x3063, Flags.Unicode)},                                           // っ
            { ScanCode.S, new KeySet( VirtualKey.Minus, ScanCode.Minus,Flags.None)},                        // ほ
            { ScanCode.D, new KeySet( VirtualKey.Astarsk, ScanCode.Astarsk,Flags.None)},                    // け
            { ScanCode.F, new KeySet( VirtualKey.Num0, ScanCode.Num0,Flags.None)},                          // 0
            { ScanCode.G, new KeySet( 0, 0x3092, Flags.Unicode)},                                           // を
            { ScanCode.H, new KeySet( 0, 0x3083, Flags.Unicode)},                                           // ゃ
            { ScanCode.J, new KeySet( 0, 0x3085, Flags.Unicode)},                                           // ゅ
            { ScanCode.K, new KeySet( 0, 0x3087, Flags.Unicode)},                                           // ょ

            { ScanCode.L, new KeySet( VirtualKey.Enter, ScanCode.Enter,Flags.None)},                        // Enter
            { ScanCode.SemiColon, new KeySet( VirtualKey.Tab, ScanCode.Tab,Flags.ExtendeKey)},              // Tab

            // line 2
            { ScanCode.M, new KeySet( VirtualKey.Left, ScanCode.Left,Flags.ExtendeKey)},                    // Left
            { ScanCode.LessThan, new KeySet( VirtualKey.Down, ScanCode.Down,Flags.ExtendeKey)},             // Down
            { ScanCode.GreaterThan, new KeySet( VirtualKey.Up, ScanCode.Up,Flags.ExtendeKey)},              // Up
            { ScanCode.Slash, new KeySet( VirtualKey.Right, ScanCode.Right,Flags.ExtendeKey)},              // Right
        };


        // User3(F13)
        private static Dictionary<ushort, KeySet> _convertMapping3 = new Dictionary<ushort, KeySet> {
            // line 4
            { ScanCode.Q, new KeySet( VirtualKey.F1, ScanCode.F1,Flags.None)},                              // F1
            { ScanCode.W, new KeySet( VirtualKey.F2, ScanCode.F2,Flags.None)},                              // F2
            { ScanCode.E, new KeySet( VirtualKey.F3, ScanCode.F3,Flags.None)},                              // F3
            { ScanCode.R, new KeySet( VirtualKey.F4, ScanCode.F4,Flags.None)},                              // F4
            { ScanCode.T, new KeySet( VirtualKey.F5, ScanCode.F5,Flags.None)},                              // F5
            { ScanCode.Y, new KeySet( VirtualKey.F6, ScanCode.F6,Flags.None)},                              // F6
            { ScanCode.U, new KeySet( VirtualKey.F7, ScanCode.F7,Flags.None)},                              // F7
            { ScanCode.I, new KeySet( VirtualKey.F8, ScanCode.F8,Flags.None)},                              // F8
            { ScanCode.O, new KeySet( VirtualKey.F9, ScanCode.F9,Flags.None)},                              // F9
            { ScanCode.P, new KeySet( VirtualKey.F10, ScanCode.F10,Flags.None)},                            // F10

            // line 3
            { ScanCode.A, new KeySet( VirtualKey.F11, ScanCode.F11,Flags.None)},                            // F11
            { ScanCode.S, new KeySet( VirtualKey.F12, ScanCode.F12,Flags.None)},                            // F12
            { ScanCode.F, new KeySet( VirtualKey.Kanji, ScanCode.Kanji,Flags.None)},                        // 半角/英数
            { ScanCode.J, new KeySet( VirtualKey.Home, ScanCode.Home,Flags.ExtendeKey)},                    // Home
            { ScanCode.K, new KeySet( VirtualKey.End, ScanCode.End,Flags.ExtendeKey)},                      // End
            { ScanCode.L, new KeySet( VirtualKey.Enter, ScanCode.Enter,Flags.None)},                        // Enter
            { ScanCode.SemiColon, new KeySet( VirtualKey.Tab, ScanCode.Tab,Flags.ExtendeKey)},              // Tab
            
            // line 2
            { ScanCode.Z, new KeySet( VirtualKey.PrintScreen, ScanCode.PrintScreen,Flags.None)},            // Print Screen
            { ScanCode.X, new KeySet( VirtualKey.ScrollLock, ScanCode.ScrollLock,Flags.None)},              // Scroll Lock
            { ScanCode.C, new KeySet( VirtualKey.Pause, ScanCode.Pause,Flags.None)},                        // Pause
            { ScanCode.V, new KeySet( VirtualKey.CapsLock, ScanCode.CapsLock,Flags.None)},                  // Caps
            { ScanCode.N, new KeySet( VirtualKey.Delete, ScanCode.Delete,Flags.None)},                      // Delete
            { ScanCode.M, new KeySet( VirtualKey.BackSpace, ScanCode.BackSpace,Flags.None)},                // BackSpace
            { ScanCode.Slash, new KeySet( VirtualKey.Escape, ScanCode.Escape,Flags.None)},                  // Escape
        };

        // User4(@)
        private static Dictionary<ushort, KeySet> _convertMapping4 = new Dictionary<ushort, KeySet> {
            // line 4
            { ScanCode.Q, new KeySet( 0, 0x21, Flags.Unicode)},                                             // !
            { ScanCode.W, new KeySet( 0, 0x22, Flags.Unicode)},                                             // "
            { ScanCode.E, new KeySet( 0, 0x23, Flags.Unicode)},                                             // #
            { ScanCode.R, new KeySet( 0, 0x24, Flags.Unicode)},                                             // $
            { ScanCode.T, new KeySet( 0, 0x25, Flags.Unicode)},                                             // %
            { ScanCode.Y, new KeySet( 0, 0x26, Flags.Unicode)},                                             // &
            { ScanCode.U, new KeySet( 0, 0x27, Flags.Unicode)},                                             // '
            { ScanCode.I, new KeySet( 0, 0x28, Flags.Unicode)},                                             // (
            { ScanCode.O, new KeySet( 0, 0x29, Flags.Unicode)},                                             // )
            { ScanCode.P, new KeySet( 0, 0x5C, Flags.Unicode)},                                             // \

            // line 3
            { ScanCode.A, new KeySet( VirtualKey.SemiColon, ScanCode.SemiColon,Flags.None)},                // + 
            { ScanCode.S, new KeySet( VirtualKey.Minus, ScanCode.Minus,Flags.None)},                        // -
            { ScanCode.D, new KeySet( VirtualKey.Astarsk, ScanCode.Astarsk,Flags.None)},                    // :
            { ScanCode.F, new KeySet( VirtualKey.Caret, ScanCode.Caret,Flags.None)},                        // ^
            { ScanCode.G, new KeySet( VirtualKey.BackSlash, ScanCode.BackSlash,Flags.None)},                // \
            
            // line 2
            { ScanCode.Z, new KeySet( VirtualKey.BracketsL, ScanCode.BracketsL,Flags.None)},                // [
            { ScanCode.X, new KeySet( VirtualKey.BracketsR, ScanCode.BracketsR,Flags.None)},                // ]
            { ScanCode.C, new KeySet( VirtualKey.Yen, ScanCode.Yen,Flags.None)},                            // \
            { ScanCode.V, new KeySet( VirtualKey.AtMark, ScanCode.AtMark,Flags.None)},                      // @
        };

        // single
        private static Dictionary<ushort, KeySet> _normalConvert = new Dictionary<ushort, KeySet> {
            //{ ScanCode.P, new KeySet( VirtualKey.SemiColon, ScanCode.SemiColon,Flags.None)},                // 
            //{ ScanCode.SemiColon, new KeySet( VirtualKey.P, ScanCode.P,Flags.None)},                        // 
            { ScanCode.Astarsk, new KeySet( VirtualKey.Minus, ScanCode.Minus,Flags.None)},                  // 
            { ScanCode.BackSlash, new KeySet( 0, 0x3093, Flags.Unicode)},                                   // ん
        };

        private static Dictionary<int, Dictionary<ushort, KeySet>> _convertMappingList = new Dictionary<int, Dictionary<ushort, KeySet>> {
            { ModifiedKey.User1, _convertMapping1},
            { ModifiedKey.User2, _convertMapping2},
            { ModifiedKey.User3, _convertMapping3},
            { ModifiedKey.User4, _convertMapping4},
        };
        #endregion

        #region Public Property
        public static bool IsHooking {
            get;
            private set;
        }
        #endregion

        #region Public Method
        /// <summary>
        ///  start global hook.
        /// </summary>
        public static void Start() {
            if (IsHooking) {
                return;
            }
            IsHooking = true;

            _hookCallback = HookProcedure;
            IntPtr hinst = System.Runtime.InteropServices.Marshal.GetHINSTANCE(typeof(KeyboardGlobalHook).Assembly.GetModules()[0]);

            _keyEventHandle = NativeMethods.SetWindowsHookEx(Const.HookTypeLL, _hookCallback, hinst, 0);
            if (_keyEventHandle == IntPtr.Zero) {
                IsHooking = false;
                throw new System.ComponentModel.Win32Exception();
            }
        }

        /// <summary>
        /// stop global hook.
        /// </summary>
        public static void Stop() {
            if (!IsHooking) {
                return;
            }

            if (_keyEventHandle != IntPtr.Zero) {
                IsHooking = false;
                NativeMethods.UnhookWindowsHookEx(_keyEventHandle);
                _keyEventHandle = IntPtr.Zero;
                _hookCallback -= HookProcedure;
                _modified = ModifiedKey.None;
            }
        }
        #endregion

        #region Privater Method
        /// <summary>
        /// hook
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="hookData"></param>
        /// <returns></returns>
        private static IntPtr HookProcedure(int code, uint msg, ref KBDLLHOOKSTRUCT hookData) {
            ushort scanCode = (ushort)hookData.scanCode;
            if (Const.Action != code || (IntPtr)ExtraInfo.SendKey == hookData.dwExtraInfo) {
                goto ExitProc;
            }

            if (hookData.vkCode == VirtualKey.ControlL) {
                goto ExitProc;
            }

            var keyStroke = (KeyStroke.KeyDown == msg) ? Flags.KeyDown : Flags.KeyUp;
            if (_modifiedKeys.ContainsKey(scanCode)) {
                if (KeyStroke.KeyDown == msg) {
                    _modified = _modifiedKeys[scanCode];
                } else {
                    _modified = ModifiedKey.None;
                }
                if (_modifiedKeys.ContainsKey(scanCode)) {
                    return (IntPtr)1;
                }
                goto ExitProc;
            }
            if (ModifiedKey.None == _modified) {
                if (_normalConvert.ContainsKey(scanCode)) {
                    SendKey(keyStroke, _normalConvert[scanCode]);
                    return (IntPtr)1;
                }
                goto ExitProc;
            }


            var mappingData = _convertMappingList[_modified];
            if (mappingData.ContainsKey(scanCode)) {
                SendKey(keyStroke, mappingData[scanCode]);
                return (IntPtr)1;
            }

            ExitProc:
            return NativeMethods.CallNextHookEx(_keyEventHandle, code, msg, ref hookData);
        }


        /// <summary>
        /// send key
        /// </summary>
        /// <param name="flag">flag. usually set additional flags</param>
        /// <param name="keyset">key set</param>
        private static void SendKey(int flag, KeySet keyset) {
            Input input = new Input();
            input.Type = InputType.Keyboard;
            input.Keyboard.Flags = flag | keyset.Flag;
            input.Keyboard.VirtualKey = keyset.VirtualKey;
            input.Keyboard.ScanCode = keyset.ScanCode;
            input.Keyboard.Time = 0;
            input.Keyboard.ExtraInfo = ExtraInfo.SendKey;
            Input[] inputs = { input };
            NativeMethods.SendInput(inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
        }


        /// <summary>
        /// send key
        /// </summary>
        /// <param name="flag">flag. usually set additional flags</param>
        /// <param name="keyset">key set</param>
        private static void SendKey(int flag, KeySetInt keyset) {
            var unicodeStr = char.ConvertFromUtf32(keyset.ScanCode);
            Input[] inputs = new Input[unicodeStr.Length];
            for (int i = 0; i < inputs.Length; i++) {
                Input input = new Input();
                input.Type = InputType.Keyboard;
                input.Keyboard.Flags = flag | keyset.Flag;
                input.Keyboard.VirtualKey = keyset.VirtualKey;
                input.Keyboard.ScanCode = (ushort)unicodeStr[i];
                input.Keyboard.Time = 0;
                input.Keyboard.ExtraInfo = ExtraInfo.SendKey;
                inputs[i] = input;
            }
            NativeMethods.SendInput(inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
        }
        #endregion
    }
}
