namespace Chip8Console.CPU
{
    public class OpCodeFX65 : AOpCodeExecuter
    {
        public OpCodeFX65(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);
        public override OpCode OpCode => new(0xF065);
        public override void Execute(OpCode opcode)
        {
            for (ushort i = 0; i <= opcode.X; i++)
            {
                cpu.StoreIntoRegister(i, cpu.Memory.Read((ushort)(cpu.RegisterI + i)));
            }
            cpu.RegisterI += (ushort)(opcode.X + 1);
        }
    }
}