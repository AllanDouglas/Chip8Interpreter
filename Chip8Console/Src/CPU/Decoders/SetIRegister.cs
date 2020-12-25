namespace Chip8Console.CPU
{
    public class SetIRegister : OpcodeDecoder
    {
        public override ushort FilterOpcode => 0xA000;

        public SetIRegister(ICPU cpu) : base(cpu) { }

        public override void Execute(Opcode opcode)
        {
            cpu.RegisterI = (ushort)(opcode.value & 0x0FFF);
            cpu.ProgramCounter += 2;
        }
    }
}