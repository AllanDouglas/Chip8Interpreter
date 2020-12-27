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
            var x = (ushort)((opcode.value & 0x0f00) >> 8);

            var constant = opcode.value & 0x00FF;
            var rand = new Random().Next(0, 255) & constant;

            cpu.StoreIntoRegister(x, (byte)rand);

        }
    }
}