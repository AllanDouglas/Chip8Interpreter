namespace Chip8Console.CPU
{
    public class OpCode1NNN : AOpCodeExecuter
    {
        public OpCode1NNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x1000);
        public override OpCode Filter => new(0xF000);
        public override void Execute(OpCode opCode)
        {
            var address = opCode.value & 0x0FFF;
            cpu.ProgramCounter = (ushort)address;
        }
    }
}