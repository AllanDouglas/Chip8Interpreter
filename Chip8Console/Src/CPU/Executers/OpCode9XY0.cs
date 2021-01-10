namespace Chip8Console.CPU
{
    public class OpCode9XY0 : AOpCodeExecuter
    {
        public OpCode9XY0(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x9000);
        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var Vx = cpu.Registers[opcode.X];
            var Vy = cpu.Registers[opcode.Y];

            if (Vx != Vy)
            {
                cpu.ProgramCounter += 2;
            }

        }
    }
}