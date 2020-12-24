namespace Chip8Console.Memory
{
    public interface IMemory
    {
        short Length { get; }
        void Store(ushort address, byte value);
        byte Read(ushort address);
        void Flush();
    }
}