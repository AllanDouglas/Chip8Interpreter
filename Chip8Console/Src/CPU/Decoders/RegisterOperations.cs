using System.Threading.Tasks;

namespace Chip8Console.CPU
{
    public class RegisterOperations : OpcodeDecoder
    {
        private readonly OpcodeDecoder[] opcodeDecoders;

        public RegisterOperations(ICPU cpu) : base(cpu)
        {
            opcodeDecoders = new OpcodeDecoder[] {
                new AddVyToVx(cpu),
                new SubtractVyToVx(cpu),
                new SetVxToVy(cpu)
            };
        }

        public override ushort FilterOpcode => 0x8000;

        public override void Execute(Opcode opcode)
        {
            var subopcode = opcode.value & 0x000F;

            foreach (var decoder in opcodeDecoders)
            {
                if (decoder.FilterOpcode != subopcode) continue;
                decoder.Execute(opcode);
                break;
            }
        }
    }
}