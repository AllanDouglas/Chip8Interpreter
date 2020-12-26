namespace Chip8Console.CPU
{
    public class ClearVideo : OpcodeDecoder
    {
        public ClearVideo(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0x00E0;

        public override void Execute(Opcode opcode)
        {
            cpu.Gpu.Clear();
            cpu.DrawFlag = true;
            cpu.ProgramCounter += 2;
        }
    }
}