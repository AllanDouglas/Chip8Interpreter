namespace Chip8Console.CPU
{
    public class SetVxToRegisterI : OpcodeDecoder
    {
        public SetVxToRegisterI(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x001E;

        public override void Execute(Opcode opcode)
        {
            var x = (opcode.value & 0x0F00) >> 8;
            cpu.RegisterI = cpu.GetFromRegister((ushort)x);
            cpu.ProgramCounter += 2;
        }
    }
}