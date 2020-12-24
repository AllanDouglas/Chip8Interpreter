namespace Chip8Console.CPU
{
    public class GeneralDecoder : OpcodeDecoder
    {
        private readonly OpcodeDecoder[] opcodeDecoders;

        public GeneralDecoder(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[] {
                new SetIRegister(cpu),
                new RegisterOperations(cpu),
                new CallSubroutine(cpu),
                new SoubroutineDecoder(cpu)
            };
        }

        public override ushort FilterNibble => 0xF000;

        public override void Execute(ushort opcode)
        {
            var subopcode = opcode & FilterNibble;

            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterNibble != subopcode) continue;
                decoder.Execute(opcode);
                break;
            }
        }
    }
}