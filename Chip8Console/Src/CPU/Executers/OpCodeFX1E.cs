namespace Chip8Console.CPU
{
    public class OpCodeFX1E : AOpCodeExecuter
    {
        public OpCodeFX1E(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x001E);

        public override OpCode Filter => new(0x00FF);

        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0x0F00) >> 8;
            cpu.RegisterI = cpu.GetFromRegister((ushort)x);
            cpu.ProgramCounter += 2;
        }
    }
}