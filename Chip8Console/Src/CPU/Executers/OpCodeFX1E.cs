namespace Chip8Console.CPU
{
    public class OpCodeFX1E : AOpCodeExecuter
    {
        public OpCodeFX1E(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x001E);

        public override OpCode Filter => new(0x00FF);

        public override void Execute(OpCode opcode)
        {
            var vx = cpu.Registers[opcode.X];
            
            if (cpu.RegisterI + vx > cpu.Memory.Length)
            {
                cpu.Registers[0xf] = 1;
            }
            else
            {
                cpu.Registers[0xf] = 0;
            }

            cpu.RegisterI += vx;
        }
    }
}