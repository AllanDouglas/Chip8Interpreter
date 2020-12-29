namespace Chip8Console.CPU
{
    public class OpCodeEXA1 : AOpCodeExecuter
    {
        public OpCodeEXA1(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);

        public override OpCode OpCode => new(0xE0A1);

        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0x0F00) >> 8;
            var vx = cpu.Registers[x];

            if (vx < cpu.Keyboard.Keys.Length && cpu.Keyboard.Keys[vx] == 0)
                cpu.ProgramCounter += 2;

        }
    }
}