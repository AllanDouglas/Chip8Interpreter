namespace Chip8Console.CPU
{
    public class OpCodeDXYN : AOpCodeExecuter
    {
        public OpCodeDXYN(ICPU cpu) : base(cpu)
        {
        }

        public override OpCode OpCode => new(0xD000);

        public override OpCode Filter => new(0xF000);

        public override void Execute(OpCode opcode)
        {
            var posX = cpu.GetFromRegister(opcode.X);
            var posY = cpu.GetFromRegister(opcode.Y);
            var height = opcode.Constant4;
            cpu.DrawFlag = true;

            cpu.StoreIntoRegister(0xF, 0);

            for (int line = 0; line < height; line++)
            {
                var pixel = cpu.Memory.Read((ushort)(cpu.RegisterI + line));

                for (int column = 0; column < 8; column++)
                {
                    if ((pixel & (0x80 >> column)) == 0) continue;

                    var x = posX + column;
                    var y = posY + line;

                    var index = (ushort)(x + (y * cpu.Gpu.Columns));

                    var currentPixel = cpu.Gpu.Read(index);

                    if (currentPixel == 1)
                        cpu.StoreIntoRegister(0xf, 1);

                    currentPixel ^= 1;
                    cpu.Gpu.Store(index, currentPixel);
                }
            }
        }
    }
}