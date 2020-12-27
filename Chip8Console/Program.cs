using System;
using System.IO;
using System.Threading;
using Chip8Console.CPU;
using Chip8Console.Memory;
using Chip8Console.Video;

namespace Chip8Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpu = new Chip8CPU(
                new RAM(4096),
                new GPU(64, 32),
                null
            );

            var video = new ConsoleDisplay(cpu.Gpu);
            var program = ReadProgram(args[0]);

            cpu.Start();
            cpu.Load(program);

            bool debug = Environment.GetEnvironmentVariable("DEBUG") == "true";
            while (true)
            {
                cpu.Tick();

                if (cpu.DrawFlag)
                {
                    if (debug == false)
                    {
                        video.Clear();
                    }

                    video.Paint();
                }

                Thread.Sleep(32);
            }

        }

        private static byte[] ReadProgram(string path)
        {
            var reader = new BinaryReader(File.OpenRead(path));
            var program = new byte[reader.BaseStream.Length];
            var index = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                program[index] = reader.ReadByte();
                index++;
            }

            return program;
        }
    }
}
