namespace Chip8Console.CPU
{
    public class OpCode00EE : AOpCodeExecuter
    {
        public OpCode00EE(ICPU cpu) : base(cpu)
        {
            SkipIncrement = true;
        }

        public override OpCode OpCode => new(0x00EE);

        public override OpCode Filter => new(0x00FF);

        public override void Execute(OpCode opcode)
        {
            cpu.ProgramCounter = cpu.Stack[--cpu.StackPointer];
        }
    }
}