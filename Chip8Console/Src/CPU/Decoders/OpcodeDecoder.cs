namespace Chip8Console.CPU
{

    public abstract class OpcodeDecoder : IOpcodeDecoder
    {
        public abstract ushort FilterOpcode { get; }

        protected ICPU cpu;

        public OpcodeDecoder(ICPU cpu)
        {
            this.cpu = cpu;
        }

        public abstract void Execute(ushort opcode);

        public override string ToString() => string.Format("{0} opcode: {1:X8}", this.GetType().Name, FilterOpcode);

    }
}