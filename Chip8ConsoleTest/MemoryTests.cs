using System;
using Chip8Console.Memory;
using Xunit;

namespace Chip8ConsoleTest
{
    public class MemoryTests
    {

        [Fact]
        public void ReadFromMemory()
        {
            IMemory memory = new RAM(16);
            memory.Store(0x0, 0x10);
            Assert.Equal(0x10, memory.Read(0x0));
        }

        [Fact]
        public void StoreOutOfMemory()
        {
            IMemory memory = new RAM(16);
            Assert.Throws<IndexOutOfRangeException>(() => memory.Store(0x11, 0x10));
        }

    }
}
