namespace Chip8Console.CPU
{
    public class OpCodeFX55 : AOpCodeExecuter
    {
        public OpCodeFX55(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);
        public override OpCode OpCode => new(0xF055);
        public override void Execute(OpCode opcode)
        {
            for (ushort i = 0; i <= opcode.X; i++)
            {
                cpu.Memory.Store((ushort)(cpu.RegisterI + i), cpu.GetFromRegister(i));
            }
            cpu.RegisterI += (ushort)(opcode.X + 1);
        }
    }
}