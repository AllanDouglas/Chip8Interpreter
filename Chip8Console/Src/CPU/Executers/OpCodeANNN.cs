namespace Chip8Console.CPU
{
    public class OpCodeANNN : AOpCodeExecuter
    {

        public override OpCode OpCode => new(0xA000);
        public override OpCode Filter => new(0xF000);
        public OpCodeANNN(ICPU cpu) : base(cpu) { }
        public override void Execute(OpCode opcode)
        {
            cpu.RegisterI = (ushort)(opcode.value & 0x0FFF);
            cpu.ProgramCounter += 2;
        }
    }
}