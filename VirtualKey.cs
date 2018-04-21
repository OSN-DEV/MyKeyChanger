using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKeyChanger {
    /// <summary>
    /// virtual key definition(定数名は日本語キーボードベース、値はUSキーボードベース)
    /// </summary>
    public class VirtualKey {
        // modifiy
        public const short Henkan = 0xFF;       // 変換
        public const short Muhenkan = 0xEB;     // 無変換
        public const short ShiftL = 0xA0;       // 左Shift
        public const short ShiftR = 0xA1;       // 右Shift
        public const short ControlL = 0xA2;     // 左Control
        public const short ControlR = 0xA3;     // 右Conrol

        // function
        public const short F1 = 0x70;
        public const short F2 = 0x71;
        public const short F3 = 0x72;
        public const short F4 = 0x73;
        public const short F5 = 0x74;
        public const short F6 = 0x75;
        public const short F7 = 0x76;
        public const short F8 = 0x77;
        public const short F9 = 0x78;
        public const short F10 = 0x79;
        public const short F11 = 0x7A;
        public const short F12 = 0x7B;
        public const short F13 = 0x7C;
        public const short F14 = 0x7D;

        // Number
        public const short Num1 = 0x31;
        public const short Num2 = 0x32;
        public const short Num3 = 0x33;
        public const short Num4 = 0x34;
        public const short Num5 = 0x35;
        public const short Num6 = 0x36;
        public const short Num7 = 0x37;
        public const short Num8 = 0x38;
        public const short Num9 = 0x39;
        public const short Num0 = 0x30;

        // Alpha
        public const short A = 0x41;
        public const short B = 0x42;
        public const short C = 0x43;
        public const short D = 0x44;
        public const short E = 0x45;
        public const short F = 0x46;
        public const short G = 0x47;
        public const short H = 0x48;
        public const short I = 0x49;
        public const short J = 0x4A;
        public const short K = 0x4B;
        public const short L = 0x4C;
        public const short M = 0x4D;
        public const short N = 0x4E;
        public const short O = 0x4F;
        public const short P = 0x50;
        public const short Q = 0x51;
        public const short R = 0x52;
        public const short S = 0x53;
        public const short T = 0x54;
        public const short U = 0x55;
        public const short V = 0x56;
        public const short W = 0x57;
        public const short X = 0x58;
        public const short Y = 0x59;
        public const short Z = 0x5A;

        // Others
        public const short Astarsk = 0xBA;
        public const short AtMark = 0xC0;
        public const short BackSlash = 0xE2;
        public const short BackSpace = 0x08;
        public const short BracketsL = 0xDB;
        public const short BracketsR = 0xDD;
        public const short CapsLock = 0x00;
        public const short Caret = 0xDE;
        public const short Colon = 0xDE;
        public const short Delete = 0x2E;
        public const short Down = 0x28;
        public const short End = 0x23;
        public const short Enter = 0x0D;
        public const short Equal = 0x00;
        public const short Escape = 0x1B;
        public const short GreaterThan = 0xBE;      // >
        public const short Home = 0x24;
        public const short Left = 0x25;
        public const short Kana = 0xF2;
        public const short Kanji = 0xF3;            // 半角/英数・漢字
        public const short LessThan = 0xBC;         // <
        public const short Minus = 0xBD;
        public const short PageDown = 0x22;
        public const short PageUp = 0x21;
        public const short Pause = 0x13;
        public const short PrintScreen = 0x2C;
        public const short Right = 0x27;
        public const short ScrollLock = 0x91;
        public const short SemiColon = 0xBB;
        public const short SingleQuote = 0x00;
        public const short Slash = 0xBF;
        public const short Tab = 0x09;
        public const short Underscore = 0xE2;       // \が２つあるのでShiftとの組み合わせの名称
        public const short Up = 0x26;
        public const short Yen = 0xDC;
    }
}
