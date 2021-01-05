using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chip8Console.CPU;
using Chip8Console.Keyboard;
using Chip8Console.Memory;
using Chip8Console.Video;

namespace Chip8Console.VM
{
    public class Chip8VM
    {

        public static void Run(string programPath, int frameTarget = 120)
        {
            if (string.IsNullOrEmpty(programPath))
                throw new ArgumentNullException($"{nameof(programPath)} can't be null");

            var cpu = new Chip8CPU(new RAM(4096), new GPU(64, 32), new Joystick());

            var video = new WindowsVideo(cpu.Gpu);
            var program = ReadProgram(programPath);

            cpu.Start();
            cpu.Load(program);

            var cpuClock = TimeSpan.FromMilliseconds(1000 / frameTarget);

            RunCPU(cpu, video, cpuClock);
            Application.Run(video);
        }

        private static void RunCPU(Chip8CPU cpu, WindowsVideo video, TimeSpan cpuClock)
        {
            Task.Run(() =>
            {
                var lastUpdate = DateTime.Now;
                var accumulator = 0d;
                while (true)
                {
                    var now = DateTime.Now;
                    var dt = now - lastUpdate;
                    lastUpdate = DateTime.Now;
                    accumulator += dt.TotalSeconds;

                    cpu.Keyboard.Update();
                    while (accumulator > cpuClock.TotalSeconds)
                    {
                        cpu.Tick();
                        accumulator -= cpuClock.TotalSeconds;
                    }
                    if (cpu.DrawFlag)
                    {
                        cpu.DrawFlag = false;
                        video.Draw();
                    }

                }
            });
        }

        private static byte[] ReadProgram(string path)
        {
            using var reader = new BinaryReader(File.OpenRead(path));
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