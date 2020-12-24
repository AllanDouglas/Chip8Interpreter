namespace Chip8Console.CPU
{
    public class SoubroutineDecoder : OpcodeDecoder
    {
        private OpcodeDecoder[] opcodeDecoders;

        public SoubroutineDecoder(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[]{
                new ReturnToRoutine(cpu)
            };
        }

        public override ushort FilterNibble => 0x0000;

        public override void Execute(ushort opcode)
        {
            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterNibble != (opcode & 0x000f)) continue;
                decoder.Execute(opcode);
                break;
            }
        }
    }
}