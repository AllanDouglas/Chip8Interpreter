using System;
using Chip8Console.CPU;
using Chip8Console.Memory;

namespace Chip8Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var chip8 = new Chip8CPU(new RAM(4096), null);

            var program = new byte[2];

            program[0] = 0xA2;
            program[1] = 0xF0;

            chip8.Start();
            chip8.Load(program);
            chip8.Tick();
        }
    }
}
