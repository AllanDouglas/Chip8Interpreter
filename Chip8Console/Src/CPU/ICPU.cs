using Chip8Console.Keyboard;
using Chip8Console.Memory;
using Chip8Console.Video;

namespace Chip8Console.CPU
{
    public interface ICPU
    {
        ushort ProgramCounter { get; set; }
        ushort RegisterI { get; set; }
        byte DelayTimer { get; set; }
        byte SoundTimer { get; set; }
        byte StackPointer { get; set; }
        ushort[] Stack { get; }
        IGPU Gpu { get; }
        IKeyboard Keyboard { get; }
        bool DrawFlag { get; set; }
        IMemory Memory { get; }
        byte[] Registers { get; }
        byte GetFromRegister(ushort address);
        void Load(byte[] program);
        void Start();
        void StoreIntoRegister(ushort address, byte value);
        void Tick();
    }
}