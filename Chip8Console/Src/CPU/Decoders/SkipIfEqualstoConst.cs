namespace Chip8Console.CPU
{
    public class SkipIfEqualsToConst : OpcodeDecoder
    {
        public SkipIfEqualsToConst(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x3000;

        public override void Execute(Opcode opcode)
        {
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            var Vx = cpu.GetFromRegister(x);
            var constant = opcode.value & 0x0FF;

            if (Vx == constant)
            {
                cpu.ProgramCounter += 2;
            }

            cpu.ProgramCounter += 2;
        }
    }
}