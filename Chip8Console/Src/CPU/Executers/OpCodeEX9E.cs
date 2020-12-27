namespace Chip8Console.CPU
{
    public class OpCodeEX9E : AOpCodeExecuter
    {
        public OpCodeEX9E(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);

        public override OpCode OpCode => new(0xE09E);

        public override void Execute(OpCode opcode)
        {
            var x = opcode.value & 0x0F00;
            var vx = cpu.Registers[x];

            if (cpu.Keyboard.Keys[vx] != 0)
                cpu.ProgramCounter += 2;

        }
    }
}