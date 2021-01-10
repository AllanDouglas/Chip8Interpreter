using System;

namespace Chip8Console.CPU
{
    public class OpCodeCXNN : AOpCodeExecuter
    {
        public OpCodeCXNN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF000);

        public override OpCode OpCode => new(0xC000);

        public override void Execute(OpCode opcode)
        {
            var rand = new Random().Next(0, 255);
            cpu.Registers[opcode.X] = (byte)(rand & opcode.Constant8);
        }
    }
}