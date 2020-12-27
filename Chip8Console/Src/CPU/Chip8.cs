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
        public readonly byte[] registers = new byte[16];

        public Chip8CPU(IMemory memory, IGPU gpu, IKeyboard keyboard)
        {
            this.memory = memory;
            this.gpu = gpu;
            this.keyboard = keyboard;
        }

        public byte StackPointer { get; set; }
        public ushort[] Stack { get; private set; } = new ushort[16];
        public ushort ProgramCounter { get; set; }
        public ushort RegisterI { get; set; }
        public byte DelayTimer { get; set; }
        public byte SoundTimer { get; set; }
        public IGPU Gpu => gpu;
        public IKeyboard Keyboard => keyboard;
        public bool DrawFlag { get; set; }

        public IMemory Memory => memory;

        public byte GetFromRegister(ushort address) => registers[address];
        public void StoreIntoRegister(ushort address, byte value)
        {
            if (IsDebug())
                Console.WriteLine($"Changing {address} from {registers[address]}, to {value}");
            registers[address] = value;
        }

        public void Start()
        {
            ProgramCounter = 0x200;
            RegisterI = 0x0;
            SoundTimer = 0x0;
            DelayTimer = 0x0;
            StackPointer = 0x0;

            for (var i = 0; i < registers.Length; i++)
            {
                registers[i] = default;
            }

            for (var i = 0; i < Stack.Length; i++)
            {
                Stack[i] = default;
            }

            Memory.Flush();

            // load fontset
            for (byte i = 0; i < chip8Fontset.Length; i++)
            {
                memory.Store(i, chip8Fontset[i]);
            }

            // load opcode decoders
            decoder = new GeneralDecoder(this, new OpCode(0xF000),
                CreateSubroutineDecoder(),
                CreateRegisterOperationsDecoder(),
                CreateMemDecoder(),
                OpCodeDecoder.CreateDecoderFor(new OpCode1NNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode2NNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode3XNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode4XNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode5XY0(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode6XNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode7XNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCode9XY0(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCodeANNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCodeDXYN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCodeBNNN(this), this),
                OpCodeDecoder.CreateDecoderFor(new OpCodeCXNN(this), this)
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
            if (IsDebug())
            {
                Console.WriteLine($"Executer {executer}, opcode {opcode}");
            }
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
            // go to next instructiuon
            ProgramCounter += 2;
        }

        private static bool IsDebug()
        {
            return false; //Environment.GetEnvironmentVariable("DEBUG") == "true";
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
                new OpCodeFX29(this),
                new OpCodeFX65(this),
                new OpCodeFX07(this),
                new OpCodeFX15(this)
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
        static readonly byte[] chip8Fontset =
        {
                0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
                0x20, 0x60, 0x20, 0x20, 0x70, // 1
                0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
                0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
                0x90, 0x90, 0xF0, 0x10, 0x10, // 4
                0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
                0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
                0xF0, 0x10, 0x20, 0x40, 0x40, // 7
                0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
                0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
                0xF0, 0x90, 0xF0, 0x90, 0x90, // A
                0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
                0xF0, 0x80, 0x80, 0x80, 0xF0, // C
                0xE0, 0x90, 0x90, 0x90, 0xE0, // D
                0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
                0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };
        #endregion
    }
}