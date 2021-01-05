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
            var Vx = cpu.GetFromRegister(opcode.X);
            var Vy = cpu.GetFromRegister(opcode.Y);

            if (Vx == Vy)
            {
                cpu.ProgramCounter += 2;
            }

        }
    }
}