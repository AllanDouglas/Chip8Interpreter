using System;
using System.Collections.Generic;

namespace Chip8Console.Keyboard
{
    public class Joystick : IKeyboard
    {
        Dictionary<ConsoleKey, byte> binds;
        public byte[] Keys { get; } = new byte[16];
        public bool HasKeyPressed { get; private set; }
        public byte LastPressedKey { get; private set; }

        public Joystick()
        {
            binds = new()
            {
                { ConsoleKey.NumPad1, 0x1 },
                { ConsoleKey.NumPad2, 0x2 },
                { ConsoleKey.NumPad3, 0x3 },
                { ConsoleKey.NumPad4, 0xC },

                { ConsoleKey.Q, 0x4 },
                { ConsoleKey.W, 0x5 },
                { ConsoleKey.E, 0x6 },
                { ConsoleKey.R, 0xD },

                { ConsoleKey.A, 0x7 },
                { ConsoleKey.S, 0x8 },
                { ConsoleKey.D, 0x9 },
                { ConsoleKey.F, 0xE },

                { ConsoleKey.Z, 0xA },
                { ConsoleKey.X, 0x0 },
                { ConsoleKey.C, 0xB },
                { ConsoleKey.V, 0xF }
            };

        }

        public void Update()
        {
            if (Console.KeyAvailable == false)
            {

                Clear();
                return;
            }

            var keyinfo = Console.ReadKey();
            if (binds.TryGetValue(keyinfo.Key, out var bind) == false) return;

            Keys[bind] = 1;
            LastPressedKey = bind;
            Console.WriteLine($"{keyinfo.KeyChar}, {bind}");
        }

        private void Clear()
        {
            HasKeyPressed = false;
            LastPressedKey = 0xFF;
            for (int i = 0; i < Keys.Length; i++)
            {
                Keys[i] = 0xFF;
            }
        }
    }
}