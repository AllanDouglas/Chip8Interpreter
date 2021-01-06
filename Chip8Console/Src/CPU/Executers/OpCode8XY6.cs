namespace Chip8Console.CPU
{
    public class OpCode8XY6 : AOpCodeExecuter
    {
        public OpCode8XY6(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x8006);

        public override void Execute(OpCode opcode)
        {

            var leastSignificantBit = cpu.GetFromRegister(opcode.X) & 0b0001;
            cpu.StoreIntoRegister(0xF, (byte)leastSignificantBit);
            cpu.Registers[opcode.X] >>= 1;
        }
    }
}