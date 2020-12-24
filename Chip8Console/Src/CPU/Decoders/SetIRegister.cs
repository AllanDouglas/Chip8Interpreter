namespace Chip8Console.CPU
{
    public class SetIRegister : OpcodeDecoder
    {
        public override ushort FilterNibble => 0xA000;

        public SetIRegister(ICPU cpu) : base(cpu) { }

        public override void Execute(ushort opcode)
        {
            cpu.RegisterI = (ushort)(opcode & 0x0FFF);
            cpu.ProgramCounter += 2;
        }
    }
}