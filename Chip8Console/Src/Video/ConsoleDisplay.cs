using System;

namespace Chip8Console.Video
{
    public class ConsoleDisplay
    {
        private readonly IGPU gpu;

        public ConsoleDisplay(IGPU gpu) => this.gpu = gpu;

        public void Clear() => Console.Clear();

        public void Paint()
        {
            for (int y = gpu.Rows - 1; y > -1; y--)
            {
                for (int x = 0; x < gpu.Columns; x++)
                {
                    var index = x + (y * gpu.Columns);
                    Console.Write(gpu.Read((ushort)index) > 0 ? "X" : string.Empty);
                }
                Console.WriteLine();
            }
        }
    }
}