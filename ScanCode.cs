using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKeyChanger {
    /// <summary>
    /// Scancode definition(定数名は日本語キーボードベース、値はUSキーボードベース)
    /// </summary>
    public class ScanCode {
        // modifiy
        public const ushort Henkan = 0x79;        // 変換
        public const ushort Muhenkan = 0x7B;      // 無変換
        public const ushort ShiftL = 0x2A;        // 左Shift
        public const ushort ShiftR = 0x36;        // 右Shift

        // function 1
        public const ushort F1 = 0x3B;
        public const ushort F2 = 0x3C;
        public const ushort F3 = 0x3D;
        public const ushort F4 = 0x3E;
        public const ushort F5 = 0x3F;
        public const ushort F6 = 0x40;
        public const ushort F7 = 0x41;
        public const ushort F8 = 0x42;
        public const ushort F9 = 0x43;
        public const ushort F10 = 0x44;
        public const ushort F11 = 0x57;
        public const ushort F12 = 0x58;
        public const ushort F13 = 0x64;
        public const ushort F14 = 0x65;

        // Number
        public const ushort Num1 = 0x02;
        public const ushort Num2 = 0x03;
        public const ushort Num3 = 0x04;
        public const ushort Num4 = 0x05;
        public const ushort Num5 = 0x06;
        public const ushort Num6 = 0x07;
        public const ushort Num7 = 0x08;
        public const ushort Num8 = 0x09;
        public const ushort Num9 = 0x0A;
        public const ushort Num0 = 0x0B;

        // Alpha
        public const ushort A = 0x1E;
        public const ushort B = 0x30;
        public const ushort C = 0x2E;
        public const ushort D = 0x20;
        public const ushort E = 0x12;
        public const ushort F = 0x21;
        public const ushort G = 0x22;
        public const ushort H = 0x23;
        public const ushort I = 0x17;
        public const ushort J = 0x24;
        public const ushort K = 0x25;
        public const ushort L = 0x26;
        public const ushort M = 0x32;
        public const ushort N = 0x31;
        public const ushort O = 0x18;
        public const ushort P = 0x19;
        public const ushort Q = 0x10;
        public const ushort R = 0x13;
        public const ushort S = 0x1F;
        public const ushort T = 0x14;
        public const ushort U = 0x16;
        public const ushort V = 0x2F;
        public const ushort W = 0x11;
        public const ushort X = 0x2D;
        public const ushort Y = 0x15;
        public const ushort Z = 0x2C;

        // Others
        public const ushort Astarsk = 0x28;
        public const ushort AtMark = 0x1A;
        public const ushort BackSlash = 0x73;
        public const ushort BackSpace = 0x0E;
        public const ushort BracketsL = 0x1B;
        public const ushort BracketsR = 0x2B;
        public const ushort CapsLock = 0x3A;
        public const ushort Caret = 0x0D;
        public const ushort Colon = 0x28;
        public const ushort Control = 0x1D;
        public const ushort Delete = 0x53;
        public const ushort Down = 0x50;
        public const ushort End = 0x4F;
        public const ushort Enter = 0x1C;
        public const ushort Equal = 0x00;
        public const ushort Escape = 0x01;
        public const ushort GreaterThan = 0x34;      // >
        public const ushort Home = 0x47;
        public const ushort Kana = 0x70;
        public const ushort Left = 0x4B;
        public const ushort Kanji = 0x29;            // 半角/英数・漢字
        public const ushort LessThan = 0x33;         // <
        public const ushort Minus = 0x0C;
        public const ushort PageDown = 0x51;
        public const ushort PageUp = 0x21;
        public const ushort Pause = 0x45;
        public const ushort PrintScreen = 0x37;
        public const ushort Right = 0x4D;
        public const ushort ScrollLock = 0x46;
        public const ushort SemiColon = 0x27;
        public const ushort SingleQuote = 0x00;
        public const ushort Slash = 0x35;
        public const ushort Tab = 0x0F;
        public const ushort Underscore = 0x73;
        public const ushort Up = 0x48;
        public const ushort Yen = 0x7D;
    }
}
