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

            var leastSignificantBit = (cpu.GetFromRegister(opcode.X) & 0b0001) != 0 ? 1 : 0;
            cpu.StoreIntoRegister(0xF, (byte)leastSignificantBit);

            var VxShitRight = cpu.GetFromRegister(opcode.X) >> 1;
            cpu.StoreIntoRegister(opcode.X, (byte)VxShitRight);
        }
    }
}