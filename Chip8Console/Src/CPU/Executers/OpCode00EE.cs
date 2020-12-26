namespace Chip8Console.CPU
{
    public class OpCode00EE : AOpCodeExecuter
    {
        public OpCode00EE(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x000E);

        public override OpCode Filter => new(0x00FF);

        public override void Execute(OpCode opcode)
        {
            cpu.ProgramCounter = cpu.Stack[cpu.StackPointer];
            --cpu.StackPointer;
        }
    }
}