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
            var x = (opcode.value & 0x0f00) >> 8;

            for (ushort i = 0; i <= x; i++)
            {
                cpu.Memory.Store((ushort)(cpu.RegisterI + i), cpu.GetFromRegister(i));
            }
        }
    }
}