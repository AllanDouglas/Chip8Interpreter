namespace Chip8Console.CPU
{
    public class OpCode8XY5 : AOpCodeExecuter
    {
        public OpCode8XY5(ICPU cpu) : base(cpu) { }

        public override OpCode OpCode => new(0x8005);

        public override OpCode Filter => new(0xF00F);

        public override void Execute(OpCode opcode)
        {
            var y = (ushort)((opcode.value & 0x00F0) >> 4);
            var x = (ushort)((opcode.value & 0x0F00) >> 8);
            var currentX = cpu.GetFromRegister(x);
            var currentY = cpu.GetFromRegister(y);

            cpu.StoreIntoRegister(0xF, (currentX > currentY) ? 1 : 0);
            cpu.Registers[x] -= currentY;
        }
    }
}