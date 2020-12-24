namespace Chip8Console.CPU
{
    public class SubtractVyToVx : OpcodeDecoder
    {
        public SubtractVyToVx(ICPU cpu) : base(cpu) { }
        public override ushort FilterOpcode => 0x0005;
        public override void Execute(ushort opcode)
        {
            var y = (opcode & 0x00F0) >> 4;
            var x = (opcode & 0x0F00) >> 8;
            cpu.StoreIntoRegister(0xF, (cpu.Registers[x] - cpu.Registers[y] < 0) ? 1 : 0);
            cpu.Registers[x] -= cpu.Registers[y];
            cpu.ProgramCounter += 2;

        }
    }
}