namespace Chip8Console.CPU
{
    public class OpCodeFX07 : AOpCodeExecuter
    {
        public OpCodeFX07(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);

        public override OpCode OpCode => new(0xF007);

        public override void Execute(OpCode opcode)
        {
            cpu.StoreIntoRegister(opcode.X, cpu.DelayTimer);
        }
    }
}