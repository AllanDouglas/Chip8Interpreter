using System;
using Chip8Console.Keyboard;
using Chip8Console.Memory;
using Chip8Console.Video;

namespace Chip8Console.CPU
{
    public class Chip8CPU : ICPU
    {
        private readonly IMemory memory;
        private readonly IGPU gpu;
        private readonly IKeyboard keyboard;
        private IOpCodeDecoder decoder;

        public Chip8CPU(IMemory memory, IGPU gpu, IKeyboard keyboard)
        {
            this.memory = memory;
            this.gpu = gpu;
            this.keyboard = keyboard;


        }

        public byte StackPointer { get; set; }
        public ushort[] Stack { get; private set; } = new ushort[16];
        public byte[] Registers { get; private set; } = new byte[16];
        public ushort ProgramCounter { get; set; }
        public ushort RegisterI { get; set; }
        public byte DelayTimer { get; set; }
        public byte SoundTimer { get; set; }
        public IGPU Gpu => gpu;
        public IKeyboard Keyboard => keyboard;
        public bool DrawFlag { get; set; }

        public IMemory Memory => memory;

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

            Memory.Flush();

            // load fontset

            decoder = new GeneralDecoder(this, new OpCode(0xF000),
                CreateSubroutineDecoder(),
                CreateRegisterOperationsDecoder(),
                CreateGeneralDecoder(),
                CreateMemDecoder()
            );

        }

        public void Load(byte[] program)
        {
            for (int i = 0; i < program.Length; ++i)
                Memory.Store((ushort)(i + 512), program[i]);
        }

        public void Tick()
        {
            DrawFlag = false;
            // Fetch opcode
            var opcode = GetOpcode();
            // Decode opcode
            var executer = decoder.Decode(opcode);
            // Execute opcode
            executer.Execute(opcode);

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

        private OpCode GetOpcode()
        {
            var first = Memory.Read(ProgramCounter) << 8;
            var secound = Memory.Read((ushort)(ProgramCounter + 1));

            return new((ushort)(first | secound));
        }

        #region Create Decoders
        private OpCodeDecoder CreateMemDecoder()
        {
            return new OpCodeDecoder(new OpCode(0xF000), this)
            {
                new OpCodeFX1E(this),
                new OpCodeFX29(this)
            };
        }
        private OpCodeDecoder CreateGeneralDecoder()
        {
            return new OpCodeDecoder(new OpCode(0xF000), this)
            {
                new OpCode1NNN(this),
                new OpCodeANNN(this),
                new OpCode3XNN(this),
                new OpCode4XNN(this),
                new OpCode6XNN(this),
                new OpCode7XNN(this),
                new OpCode8XY4(this),
                new OpCode5XY0(this),
                new OpCode9XY0(this),
                new OpCode2NNN(this),
                new OpCodeDXYN(this)
            };
        }

        private OpCodeDecoder CreateRegisterOperationsDecoder()
        {
            return new OpCodeDecoder(new OpCode(0x8000), this)
            {
                new OpCode8XY4(this),
                new OpCode8XY5(this),
                new OpCode8XY0(this)
            };
        }

        private OpCodeDecoder CreateSubroutineDecoder()
        {
            return new OpCodeDecoder(new OpCode(0x0000), this)
            {
                new OpCode00EE(this),
                new OpCode00E0(this)
            };
        }

        #endregion
    }
}