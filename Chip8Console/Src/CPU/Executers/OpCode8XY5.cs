namespace Chip8Console.CPU
{
    public class OpCode8XY5 : AOpCodeExecuter
    {
        public OpCode8XY5(ICPU cpu) : base(cpu) { }

        public override OpCode OpCode => new(0x8005);

        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var currentX = cpu.GetFromRegister(opcode.X);
            var currentY = cpu.GetFromRegister(opcode.Y);

            cpu.StoreIntoRegister(0xF, currentX >= currentY ? 1 : 0);
            cpu.Registers[opcode.X] -= currentY;
        }
    }
}