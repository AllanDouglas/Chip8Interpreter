namespace Chip8Console.CPU
{
    public class Decoder : OpcodeDecoder
    {
        private readonly OpcodeDecoder[] opcodeDecoders;

        public Decoder(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[] {
                new RegisterOperations(cpu),
                new CallSubroutine(cpu),
                new SoubroutineDecoder(cpu)
            };
        }

        public override ushort FilterOpcode => 0xF000;

        public override void Execute(ushort opcode)
        {
            var subopcode = opcode & FilterOpcode;

            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterOpcode != subopcode) continue;
                decoder.Execute(opcode);
                break;
            }
        }
    }
}