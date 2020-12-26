namespace Chip8Console.CPU
{
    public class OpCode2NNN : AOpCodeExecuter
    {
        public OpCode2NNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x2000);
        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            cpu.Stack[cpu.StackPointer] = cpu.ProgramCounter;
            ++cpu.StackPointer;
            cpu.ProgramCounter = (ushort)(opcode.value & 0x0FFF);
        }
    }
}