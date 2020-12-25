namespace Chip8Console.CPU
{
    public class CallSubroutine : OpcodeDecoder
    {
        public CallSubroutine(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x2000;

        public override void Execute(Opcode opcode)
        {
            cpu.Stack[cpu.StackPointer] = cpu.ProgramCounter;
            ++cpu.StackPointer;
            cpu.ProgramCounter = (ushort)(opcode.value & 0x0FFF);
        }
    }
}