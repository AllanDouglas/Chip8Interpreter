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
            var x = cpu.GetFromRegister((ushort)((opcode.value & 0x0F00) >> 8));
            var y = cpu.GetFromRegister((ushort)((opcode.value & 0x00F0) >> 4));
            var height = (ushort)(opcode.value & 0x000F);

            ushort pixel;

            cpu.StoreIntoRegister(0xF, 0);

            for (int line = 0; line < height; line++)
            {
                pixel = cpu.Memory.Read((ushort)(cpu.RegisterI + line));
                for (int column = 0; column < 8; column++)
                {
                    if ((pixel & (0x80 >> column)) != 0)
                    {
                        var index = (ushort)(x + column + ((y + line) * cpu.Gpu.Columns));
                        var currentPixel = cpu.Gpu.Read(index);

                        if (currentPixel == 1)
                            cpu.Registers[0xF] = 1;

                        cpu.Gpu.Store(index, (byte)(currentPixel ^ 1));

                    }
                }
            }

            cpu.DrawFlag = true;
            cpu.ProgramCounter += 2;
        }
    }
}