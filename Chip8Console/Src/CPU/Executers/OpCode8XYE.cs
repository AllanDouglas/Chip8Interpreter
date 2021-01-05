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

            var mostSignificantBit = (byte)((cpu.GetFromRegister(opcode.X) & 0b1000) != 0 ? 1 : 0);
            cpu.StoreIntoRegister(0xF, mostSignificantBit);

            var VxShitLeft = cpu.GetFromRegister(opcode.X) << 1;
            cpu.StoreIntoRegister(opcode.X, (byte)VxShitLeft);
        }
    }
}