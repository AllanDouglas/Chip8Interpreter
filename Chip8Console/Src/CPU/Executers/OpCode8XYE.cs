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
            var mostSignificantBit = (byte)((cpu.GetFromRegister(opcode.X) & 0x80) >> 7);
            cpu.Registers[0xF] = mostSignificantBit;
            cpu.Registers[opcode.X] <<= 1;
        }
    }
}