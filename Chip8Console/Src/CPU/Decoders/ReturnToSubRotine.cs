namespace Chip8Console.CPU
{
    public class ReturnToRoutine : OpcodeDecoder
    {
        public ReturnToRoutine(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x000E;

        public override void Execute(ushort opcode)
        {
            cpu.ProgramCounter = cpu.Stack[cpu.StackPointer];
            --cpu.StackPointer;
        }
    }
}