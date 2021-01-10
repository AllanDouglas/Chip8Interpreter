namespace Chip8Console.CPU
{
    public class OpCode8XYE : AOpCodeExecuter
    {
        public OpCode8XYE(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x800E);

        public override void Execute(OpCode opcode)
        {
            cpu.Registers[opcode.X] = (byte)(cpu.Registers[opcode.Y] << 1);
            cpu.Registers[0xF] = (byte)((cpu.Registers[opcode.Y] >> 7) & 0x1);
        }
    }
}