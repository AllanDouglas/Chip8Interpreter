namespace Chip8Console.CPU
{
    public class OpCode8XY5 : AOpCodeExecuter
    {
        public OpCode8XY5(ICPU cpu) : base(cpu) { }

        public override OpCode OpCode => new(0x8005);

        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var y = (opcode.value & 0x00F0) >> 4;
            var x = (opcode.value & 0x0F00) >> 8;
            cpu.StoreIntoRegister(0xF, (cpu.Registers[x] - cpu.Registers[y] < 0) ? 1 : 0);
            cpu.Registers[x] -= cpu.Registers[y];
            cpu.ProgramCounter += 2;

        }
    }
}