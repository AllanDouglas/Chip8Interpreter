using System;

namespace Chip8Console.CPU
{
    public class Unknown : AOpCodeExecuter
    {
        public Unknown(ICPU cpu) : base(cpu)
        {
        }
        public override OpCode OpCode => new(0xffff);
        public override OpCode Filter => new(0xffff);
        public override void Execute(OpCode opcode)
        {
            Console.WriteLine($"Not Implemented yet: {opcode}");

        }
    }
}