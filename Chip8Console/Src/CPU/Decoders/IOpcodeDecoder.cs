namespace Chip8Console.CPU
{
    public interface IOpcodeDecoder
    {
        ushort FilterNibble { get; }

        void Execute(ushort opcode);
    }
}