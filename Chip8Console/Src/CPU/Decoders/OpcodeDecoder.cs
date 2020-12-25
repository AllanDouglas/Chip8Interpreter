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

        public void Execute(ushort opcode) => Execute(new Opcode(opcode));
        public abstract void Execute(Opcode opcode);

        public override string ToString() => string.Format("{0} opcode: {1:X8}", GetType().Name, FilterOpcode);

    }
}