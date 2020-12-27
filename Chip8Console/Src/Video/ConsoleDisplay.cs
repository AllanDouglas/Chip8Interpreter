using System;
using System.Text;

namespace Chip8Console.Video
{
    public class ConsoleDisplay
    {
        private readonly IGPU gpu;
        private readonly StringBuilder str;

        public ConsoleDisplay(IGPU gpu)
        {
            this.gpu = gpu;
            str = new StringBuilder();
        }

        public void Clear()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DEBUG")))
                Console.Clear();

            str.Clear();
        }

        public void Paint()
        {
            Clear();
            for (var y = 0; y < gpu.Rows; y++)
            {
                for (int x = 0; x < gpu.Columns; x++)
                {
                    var index = x + (y * gpu.Columns);
                    byte pixel = gpu.Read((ushort)index);
                    str.Append(pixel > 0 ? "XX" : "..");
                }
                str.AppendLine();
            }
            Console.Write(str.ToString());
        }
    }
}