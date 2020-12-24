namespace Chip8Console.CPU
{
    public interface IOpcodeDecoder
    {
        ushort FilterOpcode { get; }

        void Execute(ushort opcode);
    }
}