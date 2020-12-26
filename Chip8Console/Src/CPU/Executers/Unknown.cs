using System;

namespace Chip8Console.CPU
{
    public class Unknown : Executer
    {
        public Unknown(ICPU cpu) : base(cpu)
        {
        }
        public override OpCode OpCode => new(0xffff);
        public override OpCode Filter => new(0xffff);
        public override void Execute(OpCode opcode)
        {
            Console.WriteLine($"Not Implemented yet: {opcode}");
            cpu.ProgramCounter += 2;
        }
    }
}