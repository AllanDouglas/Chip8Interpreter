namespace Chip8Console.CPU
{
    public class OpCodeFX33 : AOpCodeExecuter
    {
        public OpCodeFX33(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF0FF);
        public override OpCode OpCode => new(0xF033);
        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0x0f00) >> 8;
            var vx = cpu.Registers[x];

            this.cpu.Memory.Store(cpu.RegisterI, (byte)(vx / 100));
            this.cpu.Memory.Store((byte)(cpu.RegisterI + 1), (byte)(vx / 10 % 10));
            this.cpu.Memory.Store((byte)(cpu.RegisterI + 2), (byte)(vx % 100 % 10));

        }
    }
}