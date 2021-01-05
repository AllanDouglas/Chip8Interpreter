namespace Chip8Console.CPU
{
    public class OpCode8XY4 : AOpCodeExecuter
    {
        public OpCode8XY4(ICPU cpu) : base(cpu) { }

        public override OpCode OpCode => new(0x8004);
        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var currentX = cpu.GetFromRegister(opcode.X);
            var currentY = cpu.GetFromRegister(opcode.Y);

            var complementTo255 = 0xFF - currentX;
            cpu.StoreIntoRegister(0xF, currentY > complementTo255 ? 1 : 0);
            cpu.StoreIntoRegister(opcode.X, (byte)(currentX + currentY));
        }

    }
}