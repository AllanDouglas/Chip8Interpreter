using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Chip8Console.Keyboard
{
    public class Joystick : IKeyboard
    {
        public const int KEY_PRESSED = 0x8000;
        private const int KEY_KEYDOWN = 0x0100;
        private const int KEY_UP = 0x0101;
        readonly Dictionary<Keys, byte> binds;
        public byte[] KeyBinds { get; } = new byte[16];
        public bool HasKeyPressed
        {
            get
            {
                foreach (var keys in KeyBinds)
                {
                    if (keys > 0) return true;
                }
                return false;
            }
        }
        public byte LastPressedKey { get; private set; }

        public Joystick()
        {

            binds = new()
            {
                { Keys.Up, 0x1 },
                { Keys.Down, 0x2 },
                { Keys.Right, 0x3 },
                { Keys.Left, 0xC },

                { Keys.Q, 0x4 },
                { Keys.W, 0x5 },
                { Keys.E, 0x6 },
                { Keys.R, 0xD },

                { Keys.A, 0x7 },
                { Keys.S, 0x8 },
                { Keys.D, 0x9 },
                { Keys.F, 0xE },

                { Keys.Z, 0xA },
                { Keys.X, 0x0 },
                { Keys.C, 0xB },
                { Keys.V, 0xF }
            };

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
        private static bool IsKeyDown(Keys key)
        {
            return Convert.ToBoolean(GetKeyState((int)key) & (KEY_KEYDOWN));
        }

        private static bool IsKeyUp(Keys key)
        {
            return (GetKeyState((int)key) & KEY_UP) == KEY_UP;
        }

        public void Update()
        {
            foreach (var bind in binds)
            {
                KeyBinds[bind.Value] = IsKeyDown(bind.Key) ? 1 : 0;

                if (KeyBinds[bind.Value] > 0)
                {
                    LastPressedKey = bind.Value;
                }
            }
        }

        public override string ToString()
        {
            var str = string.Empty;
            for (int i = 0; i < KeyBinds.Length; i++)
            {
                byte value = KeyBinds[i];
                str += $"{i:x2} = {value} ";
            }

            return str;
        }

    }
}