namespace Chip8Console.CPU
{
    public class SkipIfVxNotEqualsVy : OpcodeDecoder
    {
        public SkipIfVxNotEqualsVy(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x5000;

        public override void Execute(Opcode opcode)
        {
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            var y = (ushort)((opcode.value & 0x00F0) >> 4);
            var Vx = cpu.GetFromRegister(x);
            var Vy = cpu.GetFromRegister(y);

            if (Vx != Vy)
            {
                cpu.ProgramCounter += 2;
            }

            cpu.ProgramCounter += 2;
        }
    }
}