using System;
using Chip8Console.Keyboard;
using Chip8Console.Memory;

namespace Chip8Console.CPU
{
    public class Chip8CPU : ICPU
    {
        private readonly IMemory memory;
        private readonly IKeyboard keyboard;
        private Decoder decoder;

        public Chip8CPU(IMemory memory, IKeyboard keyboard)
        {
            this.memory = memory;
            this.keyboard = keyboard;
        }

        public byte StackPointer { get; set; }
        public ushort[] Stack { get; private set; } = new ushort[16];
        public byte[] Registers { get; private set; } = new byte[16];
        public ushort ProgramCounter { get; set; }
        public ushort RegisterI { get; set; }
        public byte DelayTimer { get; set; }
        public byte SoundTimer { get; set; }

        public byte GetFromRegister(ushort address) => Registers[address];
        public void StoreIntoRegister(ushort address, byte value) => Registers[address] = value;

        public void Start()
        {
            ProgramCounter = 0x200;
            RegisterI = 0x0;
            SoundTimer = 0x0;
            DelayTimer = 0x0;
            StackPointer = 0x0;

            for (var i = 0; i < Registers.Length; i++)
            {
                Registers[i] = default;
            }

            for (var i = 0; i < Stack.Length; i++)
            {
                Stack[i] = default;
            }

            memory.Flush();

            // load fontset
            decoder = new Decoder(this);

        }

        public void Load(byte[] program)
        {
            for (int i = 0; i < program.Length; ++i)
                memory.Store((ushort)(i + 512), program[i]);
        }

        public void Tick()
        {
            // Fetch opcode
            var opcode = Getopcode();
            // Decode opcode
            // Execute opcode
            decoder.Execute(opcode);

            // Update timers
            if (DelayTimer > 0)
                --DelayTimer;

            if (SoundTimer > 0)
            {
                if (SoundTimer == 1)
                {
                    Console.WriteLine("BEEP!");
                }
                --SoundTimer;
            }
        }

        private ushort Getopcode() => (ushort)(memory.Read(ProgramCounter) << 8 | memory.Read((ushort)(ProgramCounter + 1)));
    }
}