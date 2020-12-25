namespace Chip8Console.CPU
{
    public class AddConstToVx : OpcodeDecoder
    {
        public AddConstToVx(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x7000;

        public override void Execute(Opcode opcode)
        {
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            cpu.Registers[x] += (byte)(opcode.value & 0x00FF);
            cpu.ProgramCounter += 2;
        }
    }
}