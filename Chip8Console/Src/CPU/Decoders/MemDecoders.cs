namespace Chip8Console.CPU
{
    public class MemDecoder : OpcodeDecoder
    {
        private OpcodeDecoder[] opcodeDecoders;

        public MemDecoder(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[]{
              new SetVxToRegisterI(cpu)
            };
        }

        public override ushort FilterOpcode => 0xF000;

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