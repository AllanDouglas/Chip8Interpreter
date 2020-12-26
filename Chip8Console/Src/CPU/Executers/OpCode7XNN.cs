namespace Chip8Console.CPU
{
    public class OpCode7XNN : Executer
    {
        public OpCode7XNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x7000);
        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            cpu.Registers[x] += (byte)(opcode.value & 0x00FF);
            cpu.ProgramCounter += 2;
        }
    }
}