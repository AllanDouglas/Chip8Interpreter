namespace Chip8Console.CPU
{
    public class OpCodeFX29 : AOpCodeExecuter
    {
        public OpCodeFX29(ICPU cpu) : base(cpu)
        {
        }
        public override OpCode Filter => new(0xF0FF);
        public override OpCode OpCode => new(0xF029);

        public override void Execute(OpCode opcode)
        {
            var x = (ushort)((opcode.value & 0x0F00) >> 8);

            cpu.RegisterI = cpu.GetFromRegister(x);
            cpu.ProgramCounter += 2;
        }
    }
}