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

            var x = (opcode.value & 0x0f00) >> 8;

            for (ushort i = 0; i <= x; i++)
            {
                cpu.StoreIntoRegister(i, cpu.Memory.Read((ushort)(cpu.RegisterI + i)));
            }
            // cpu.RegisterI += (ushort)(x + 1);
        }
    }
}