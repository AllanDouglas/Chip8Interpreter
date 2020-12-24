namespace Chip8Console.CPU
{
    public class JumpTo : OpcodeDecoder
    {
        public JumpTo(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterNibble => 0x1000;

        public override void Execute(ushort opcode)
        {
            var address = opcode & 0x0FFF;
            cpu.ProgramCounter = (ushort)address;
        }
    }
}