namespace Chip8Console.CPU
{
    public class OpCode4XNN : AOpCodeExecuter
    {
        public OpCode4XNN(ICPU cpu) : base(cpu)
        {
        }
        public override OpCode OpCode => new(0x4000);

        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            var Vx = cpu.Registers[opcode.X];

            if (Vx != opcode.Constant8)
            {
                cpu.ProgramCounter += 2;
            }

        }
    }
}