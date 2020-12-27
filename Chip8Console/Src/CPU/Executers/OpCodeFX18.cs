namespace Chip8Console.CPU
{
    public class OpCodeFX18 : AOpCodeExecuter
    {
        public OpCodeFX18(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);
        public override OpCode OpCode => new(0xF018);
        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0x0f00) >> 8;
            cpu.SoundTimer = cpu.Registers[x];

        }
    }
}