namespace Chip8Console.CPU
{
    public class GeneralDecoder : OpcodeDecoder
    {
        private readonly OpcodeDecoder[] opcodeDecoders;

        public GeneralDecoder(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[] {
                new SetIRegister(cpu),
                new SkipIfEqualsToConst(cpu),
                new SkipIfNotEqualstoConst(cpu),
                new SetConstToVx(cpu),
                new SkipIfVxEualsVy(cpu),
                new RegisterOperations(cpu),
                new CallSubroutine(cpu),
                new SoubroutineDecoder(cpu)
            };
        }

        public override ushort FilterOpcode => 0xF000;

        public override void Execute(Opcode opcode)
        {
            var subopcode = opcode.value & FilterOpcode;

            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterOpcode != subopcode) continue;
                decoder.Execute(opcode.value);
                break;
            }
        }
    }
}