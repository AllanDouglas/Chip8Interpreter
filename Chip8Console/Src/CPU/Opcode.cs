namespace Chip8Console.CPU
{
    public struct Opcode
    {
        public readonly ushort value;

        public Opcode(ushort opcode)
        {
            this.value = opcode;
        }

        public override string ToString() => $"{value:x}";
    }
}