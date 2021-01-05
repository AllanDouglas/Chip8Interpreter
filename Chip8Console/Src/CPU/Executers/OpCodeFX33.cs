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
            var vx = cpu.Registers[opcode.X];

            var value = (byte)(vx / 100 % 10);
            var value1 = (byte)(vx / 10 % 10);
            var value2 = (byte)(vx % 10);

            cpu.Memory.Store(cpu.RegisterI, value);
            cpu.Memory.Store((ushort)(cpu.RegisterI + 1), value1);
            cpu.Memory.Store((ushort)(cpu.RegisterI + 2), value2);

        }
    }
}