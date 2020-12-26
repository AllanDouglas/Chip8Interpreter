namespace Chip8Console.CPU
{
    public interface IOpCodeDecoder
    {
        OpCode Filter { get; }
        IOpCodeExecuter Decode(OpCode opCode);
    }
}