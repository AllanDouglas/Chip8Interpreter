namespace Chip8Console.CPU
{
    public class SoubroutineDecoder : OpcodeDecoder
    {
        private OpcodeDecoder[] opcodeDecoders;

        public SoubroutineDecoder(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[]{
                new ReturnToRoutine(cpu),
                new ClearVideo(cpu),
            };
        }

        public override ushort FilterOpcode => 0x0000;

        public override void Execute(Opcode opcode)
        {
            var subopcode = new Opcode((ushort)(opcode.value & 0x00FF));
            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterOpcode != subopcode.value) continue;
                decoder.Execute(opcode);
                break;
            }
        }
    }
}