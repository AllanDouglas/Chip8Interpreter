namespace Chip8Console.Memory
{
    public class RAM : IMemory
    {

        private readonly byte[] memory;

        public RAM(int size)
        {
            memory = new byte[size];
        }

        public short Length => (short)memory.Length;
        public void Flush()
        {
            for (int i = 0; i < this.memory.Length; i++)
            {
                this.memory[i] = default;
            }
        }
        public byte Read(ushort address) => memory[address];
        public void Store(ushort address, byte value) => memory[address] = value;
    }
}