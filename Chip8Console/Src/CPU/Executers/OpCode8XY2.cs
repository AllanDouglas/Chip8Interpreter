namespace Chip8Console.CPU
{
    public class OpCode8XY2 : AOpCodeExecuter
    {
        public OpCode8XY2(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x8002);

        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0x0F00) >> 8;
            var y = (opcode.value & 0x00F0) >> 4;

            var VxAndVy = cpu.GetFromRegister((ushort)x) & cpu.GetFromRegister((ushort)y);
            cpu.StoreIntoRegister((ushort)x, (byte)VxAndVy);
        }
    }
}