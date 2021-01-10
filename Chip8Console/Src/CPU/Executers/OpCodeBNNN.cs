namespace Chip8Console.CPU
{
    public class OpCodeBNNN : AOpCodeExecuter
    {
        public OpCodeBNNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF000);

        public override OpCode OpCode => new(0xB000);

        public override void Execute(OpCode opcode)
        {
            cpu.ProgramCounter = (ushort)(opcode.Address + cpu.Registers[0]);
        }
    }
}