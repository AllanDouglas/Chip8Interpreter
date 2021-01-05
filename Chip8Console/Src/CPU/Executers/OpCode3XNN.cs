namespace Chip8Console.CPU
{
    public class OpCode3XNN : AOpCodeExecuter
    {
        public OpCode3XNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x3000);

        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            var Vx = cpu.GetFromRegister(opcode.X);
            if (Vx == opcode.Constant8)
            {
                cpu.ProgramCounter += 2;
            }

        }
    }
}