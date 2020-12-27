namespace Chip8Console.CPU
{
    public interface IOpCodeExecuter
    {
        OpCode OpCode { get; }
        OpCode Filter { get; }
        bool SkipIncrement { get; }

        void Execute(ushort opcode);
        void Execute(OpCode opcode);
    }
}