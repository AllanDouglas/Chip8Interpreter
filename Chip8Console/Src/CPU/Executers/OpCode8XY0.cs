namespace Chip8Console.CPU
{
    public class OpCode8XY0 : Executer
    {
        public OpCode8XY0(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x8000);

        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var x = (ushort)((opcode.value & 0xF00) >> 8);
            var y = (ushort)((opcode.value & 0x0F0) >> 4);

            cpu.Registers[x] = cpu.Registers[y];
            cpu.ProgramCounter += 2;

        }
    }
}