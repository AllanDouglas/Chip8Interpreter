namespace Chip8Console.Video
{
    public interface IGPU
    {
        int Columns { get; }
        int Rows { get; }

        void Clear();
        byte[] Dump();
        void Store(ushort index, byte value);
        byte Read(ushort address);
    }
}