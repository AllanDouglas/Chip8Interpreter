namespace Chip8Console.CPU
{
    public class SetVxToVy : OpcodeDecoder
    {
        public SetVxToVy(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x0000;

        public override void Execute(Opcode opcode)
        {
            var x = (ushort)((opcode.value & 0xF00) >> 8);
            var y = (ushort)((opcode.value & 0x0F0) >> 4);

            cpu.Registers[x] = cpu.Registers[y];
            cpu.ProgramCounter += 2;

        }
    }
}