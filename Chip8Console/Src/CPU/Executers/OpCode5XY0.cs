namespace Chip8Console.CPU
{
    public class OpCode5XY0 : AOpCodeExecuter
    {
        public OpCode5XY0(ICPU cpu) : base(cpu)
        {
        }
        public override OpCode OpCode => new(0x5000);

        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            var y = (ushort)((opcode.value & 0x00F0) >> 4);
            var Vx = cpu.GetFromRegister(x);
            var Vy = cpu.GetFromRegister(y);

            if (Vx == Vy)
            {
                cpu.ProgramCounter += 2;
            }

        }
    }
}