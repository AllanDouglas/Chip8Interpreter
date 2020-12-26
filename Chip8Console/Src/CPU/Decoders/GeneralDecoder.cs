namespace Chip8Console.CPU
{
    public class GeneralDecoder : OpcodeDecoder
    {
        private readonly OpcodeDecoder[] opcodeDecoders;
        private readonly Unknown unknownn;
        public GeneralDecoder(ICPU cpu) : base(cpu)
        {
            unknownn = new Unknown(cpu);

            opcodeDecoders = new OpcodeDecoder[] {
                new JumpTo(cpu),
                new SetIRegister(cpu),
                new SkipIfEqualsToConst(cpu),
                new SkipIfNotEqualstoConst(cpu),
                new SetConstToVx(cpu),
                new AddConstToVx(cpu),
                new AddVyToVx(cpu),
                new SkipIfVxEqualsVy(cpu),
                new SkipIfVxNotEqualsVy(cpu),
                new CallSubroutine(cpu),
                new RegisterOperations(cpu),
                new SoubroutineDecoder(cpu),
                new DrawSpriteAtXY(cpu),
                new MemDecoder(cpu)
            };
        }

        public override ushort FilterOpcode => 0xF000;

        public override void Execute(Opcode opcode)
        {
            var subopcode = opcode.value & FilterOpcode;

            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterOpcode != subopcode) continue;
                decoder.Execute(opcode);
                return;
            }

            unknownn.Execute(opcode);
        }
    }
}