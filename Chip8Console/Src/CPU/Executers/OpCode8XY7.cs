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
            cpu.StoreIntoRegister(0xF, cpu.GetFromRegister(opcode.Y) > cpu.GetFromRegister(opcode.X) ? 1 : 0);
            
            var VyMinusVx = cpu.GetFromRegister(opcode.Y) - cpu.GetFromRegister(opcode.X);
            cpu.StoreIntoRegister(opcode.X, (byte)VyMinusVx);
        }
    }
}