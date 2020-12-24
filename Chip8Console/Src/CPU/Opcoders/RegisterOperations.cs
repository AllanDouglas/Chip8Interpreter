namespace Chip8Console.CPU
{
    public class RegisterOperations : OpcodeDecoder
    {
        private readonly OpcodeDecoder[] opcodeDecoders;

        public RegisterOperations(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[] {
                new SetIRegister(cpu),
                new AddVyToVx(cpu),
                new SubtractVyToVx(cpu)
            };
        }

        public override ushort FilterOpcode => 0x8000;

        public override void Execute(ushort opcode)
        {
            var subopcode = opcode & 0x000F;

            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterOpcode != subopcode) continue;
                decoder.Execute(opcode);
                break;
            }
        }
    }
}