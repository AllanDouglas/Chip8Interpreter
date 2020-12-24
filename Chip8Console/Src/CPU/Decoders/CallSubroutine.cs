namespace Chip8Console.CPU
{
    public class CallSubroutine : OpcodeDecoder
    {
        public CallSubroutine(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterNibble => 0x2000;

        public override void Execute(ushort opcode)
        {
            cpu.Stack[cpu.StackPointer] = cpu.ProgramCounter;
            ++cpu.StackPointer;
            cpu.ProgramCounter = (ushort)(opcode & 0x0FFF);
        }
    }
}