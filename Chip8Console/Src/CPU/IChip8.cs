namespace Chip8Console.CPU
{
    public interface ICPU
    {
        byte[] Registers { get; }
        ushort ProgramCounter { get; set; }
        ushort RegisterI { get; set; }
        byte DelayTimer { get; set; }
        byte SoundTimer { get; set; }
        byte StackPointer { get; set; }
        ushort[] Stack { get; }

        byte GetFromRegister(ushort address);
        void Load(byte[] program);
        void Start();
        void StoreIntoRegister(ushort address, byte value);
        void Tick();

    }
}