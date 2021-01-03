using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chip8Console.CPU;
using Chip8Console.Keyboard;
using Chip8Console.Memory;
using Chip8Console.Video;

namespace Chip8Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var cpu = new Chip8CPU(
                new RAM(4096),
                new GPU(64, 32),
                new Joystick()
            );

            var video = new WindowsVideo(cpu.Gpu);
            var program = ReadProgram(args[0]);

            cpu.Start();
            cpu.Load(program);

            var cpuClock = new TimeSpan(TimeSpan.TicksPerSecond / 500);
            
            Task.Run(async () =>
            {
                while (true)
                {
                    cpu.Tick();

                    if (cpu.DrawFlag)
                    {
                        cpu.DrawFlag = false;
                        video.Draw();
                    }

                    cpu.Keyboard.Update();
                    await Task.Delay(cpuClock.Milliseconds);
                }
            });

            Application.Run(video);

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
