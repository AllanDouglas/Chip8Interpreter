namespace Chip8Console.CPU
{
    public class OpCode8XY3 : AOpCodeExecuter
    {
        public OpCode8XY3(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x8003);

        public override void Execute(OpCode opcode)
        {
            cpu.Registers[opcode.X] ^= cpu.Registers[opcode.Y];
        }
    }
}