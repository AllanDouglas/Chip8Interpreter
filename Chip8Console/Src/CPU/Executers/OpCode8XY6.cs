namespace Chip8Console.CPU
{
    public class OpCode8XY6 : AOpCodeExecuter
    {
        public OpCode8XY6(ICPU cpu) : base(cpu) { }
        public override OpCode Filter => new(0xF00F);
        public override OpCode OpCode => new(0x8006);
        public override void Execute(OpCode opcode)
        {
            cpu.Registers[0xf] = (byte)(cpu.Registers[opcode.Y] & 0x1);
            cpu.Registers[opcode.X] = (byte)(cpu.Registers[opcode.Y] >> 1);
        }
    }
}