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
            var x = (opcode.value & 0xF000) >> 8;

            cpu.StoreIntoRegister(0xF, (byte)(cpu.GetFromRegister((ushort)x) & 0b1000));

            var VxShitLeft = cpu.GetFromRegister((ushort)x) << 1;
            cpu.StoreIntoRegister((ushort)x, (byte)VxShitLeft);
        }
    }
}