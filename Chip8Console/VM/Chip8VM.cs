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

        public static void Run(string programPath)
        {
            if (string.IsNullOrEmpty(programPath))
                throw new ArgumentNullException($"{nameof(programPath)} can't be null");

            var cpu = new Chip8CPU(new RAM(4096), new GPU(64, 32), new Joystick());

            var video = new WindowsVideo(cpu.Gpu);
            var program = ReadProgram(programPath);

            cpu.Start();
            cpu.Load(program);

            var cpuClock = new TimeSpan(TimeSpan.TicksPerSecond / 540);

            RunCPU(cpu, video, cpuClock);
            Application.Run(video);
        }

        private static void RunCPU(Chip8CPU cpu, WindowsVideo video, TimeSpan cpuClock)
        {
            Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();
                while (true)
                {
                    if (sw.ElapsedTicks < cpuClock.Ticks) continue;
                    sw.Restart();
                    cpu.Tick();
                    if (cpu.DrawFlag)
                    {
                        cpu.DrawFlag = false;
                        video.Draw();
                    }
                    cpu.Keyboard.Update();
                }
            });
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