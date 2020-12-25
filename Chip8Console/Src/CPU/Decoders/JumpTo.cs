namespace Chip8Console.CPU
{
    public class JumpTo : OpcodeDecoder
    {
        public JumpTo(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x1000;

        public override void Execute(Opcode opcode)
        {
            var address = opcode.value & 0x0FFF;
            cpu.ProgramCounter = (ushort)address;
        }
    }
}