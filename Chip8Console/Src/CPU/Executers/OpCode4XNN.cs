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
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            var Vx = cpu.GetFromRegister(x);
            var constant = 0x0FF;

            if (Vx != constant)
            {
                cpu.ProgramCounter += 2;
            }

            cpu.ProgramCounter += 2;
        }
    }
}