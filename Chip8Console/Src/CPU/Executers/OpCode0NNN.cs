namespace Chip8Console.CPU
{
    public class OpCode0NNN : AOpCodeExecuter
    {
        public OpCode0NNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0x0000);

        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            
        }
    }
}