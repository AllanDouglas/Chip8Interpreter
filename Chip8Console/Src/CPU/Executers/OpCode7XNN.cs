namespace Chip8Console.CPU
{
    public class OpCode7XNN : AOpCodeExecuter
    {
        public OpCode7XNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x7000);
        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            cpu.Registers[opcode.X] += opcode.Constant8;
        }
    }
}