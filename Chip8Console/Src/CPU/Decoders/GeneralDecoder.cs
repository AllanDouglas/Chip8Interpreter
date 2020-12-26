namespace Chip8Console.CPU
{
    public sealed class GeneralDecoder : IOpCodeDecoder
    {

        private readonly Unknown unknownn;
        private readonly IOpCodeDecoder[] decoders;

        public GeneralDecoder(ICPU cpu, OpCode opCode, params IOpCodeDecoder[] decoders)
        {
            unknownn = new Unknown(cpu);
            Filter = opCode;
            this.decoders = decoders;
        }

        public OpCode Filter { get; private set; }

        public IOpCodeExecuter Decode(OpCode opCode)
        {
            var subopcode = new OpCode((ushort)(opCode.value & Filter.value));

            foreach (var decoder in decoders)
            {
                if (decoder.Filter != subopcode) continue;
                return decoder.Decode(opCode);
            }

            return unknownn;
        }

    }
}