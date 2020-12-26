namespace Chip8Console.CPU
{
    public class OpCode8XY4 : AOpCodeExecuter
    {
        public OpCode8XY4(ICPU cpu) : base(cpu) { }

        public override OpCode OpCode => new(0x8004);
        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
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