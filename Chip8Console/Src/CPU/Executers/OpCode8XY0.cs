namespace Chip8Console.CPU
{
    public class OpCode8XY0 : AOpCodeExecuter
    {
        public OpCode8XY0(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x8000);

        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            cpu.StoreIntoRegister(opcode.X, cpu.GetFromRegister(opcode.Y));
        }
    }
}