namespace Chip8Console.CPU
{
    public class OpCode00E0 : AOpCodeExecuter
    {
        public OpCode00E0(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x00E0);

        public override OpCode Filter => new(0x00FF);

        public override void Execute(OpCode opcode)
        {
            cpu.Gpu.Clear();
            cpu.DrawFlag = true;
        }
    }
}