namespace Chip8Console.CPU
{
    public class OpCode8XY7 : AOpCodeExecuter
    {
        public OpCode8XY7(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x8007);

        public override void Execute(OpCode opcode)
        {
            cpu.Registers[0xF] = cpu.Registers[opcode.Y] >= cpu.Registers[opcode.X] ? 1 : 0;
            cpu.Registers[opcode.X] = (byte)(cpu.Registers[opcode.Y] - cpu.Registers[opcode.X]);
        }
    }
}