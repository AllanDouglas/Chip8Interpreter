namespace Chip8Console.CPU
{
    public class OpCodeFX0A : AOpCodeExecuter
    {
        public OpCodeFX0A(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);

        public override OpCode OpCode => new(0xF00A);

        public override void Execute(OpCode opcode)
        {

            if (cpu.Keyboard.HasKeyPressed)
            {
                cpu.Registers[opcode.X] = cpu.Keyboard.LastPressedKey;
            }
            else
                cpu.ProgramCounter -= 2;

        }
    }
}