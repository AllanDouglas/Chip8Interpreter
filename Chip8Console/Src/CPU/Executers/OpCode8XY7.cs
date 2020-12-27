namespace Chip8Console.CPU
{
    public class OpCode8XY7 : AOpCodeExecuter
    {
        public OpCode8XY7(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode Filter => new(0xF00F);

        public override OpCode OpCode => new(0x8007);

        public override void Execute(OpCode opcode)
        {
            var x = (opcode.value & 0xF000) >> 8;
            var y = (opcode.value & 0x00F0) >> 4;


            cpu.StoreIntoRegister(0xF, cpu.GetFromRegister((ushort)x) > cpu.GetFromRegister((ushort)y) ? 1 : 0);            
            var VxAndVy = cpu.GetFromRegister((ushort)x) - cpu.GetFromRegister((ushort)y);
            cpu.StoreIntoRegister((ushort)x, (byte)VxAndVy);
        }
    }
}