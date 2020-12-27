namespace Chip8Console.CPU
{
    public class OpCode8XY6 : AOpCodeExecuter
    {
        public OpCode8XY6(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x8002);

        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0xF000) >> 8;

            cpu.StoreIntoRegister(0xF, (byte)(cpu.GetFromRegister((ushort)x) & 0b0001));

            var VxShitRight = cpu.GetFromRegister((ushort)x) >> 1;
            cpu.StoreIntoRegister((ushort)x, (byte)VxShitRight);
        }
    }
}