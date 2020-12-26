using System;

namespace Chip8Console.CPU
{
    public class Unknown : OpcodeDecoder
    {
        public Unknown(ICPU cpu) : base(cpu)
        {
        }

        public override ushort FilterOpcode => 0xFFFF;

        public override void Execute(Opcode opcode)
        {
            Console.WriteLine($"Not Implemented yet: {opcode}");
            cpu.ProgramCounter += 2;
        }
    }
}