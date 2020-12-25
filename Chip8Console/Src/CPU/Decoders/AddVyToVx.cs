namespace Chip8Console.CPU
{
    public class AddVyToVx : OpcodeDecoder
    {
        public AddVyToVx(ICPU cpu) : base(cpu) { }
        public override ushort FilterOpcode => 0x0004;
        public override void Execute(Opcode opcode)
        {

            var y = (opcode.value & 0x00F0) >> 4;
            var x = (opcode.value & 0x0F00) >> 8;

            var complementTo255 = 0xFF - cpu.Registers[x];
            cpu.StoreIntoRegister(0xF, (cpu.Registers[y] > complementTo255) ? 1 : 0);

            cpu.Registers[x] += cpu.Registers[y];
            cpu.ProgramCounter += 2;
        }

    }
}