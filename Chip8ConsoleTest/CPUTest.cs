using System;
using Chip8Console.CPU;
using Chip8Console.Memory;
using Xunit;

namespace Chip8ConsoleTest
{
    public class CPUTest
    {

        [Fact]
        public void ADD_Vx_To_Vy()
        {
            var cpu = new Chip8CPU(new RAM(4096), null);
            var addVyToVx = new AddVyToVx(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            addVyToVx.Execute(0x8374);

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void ADD_Vx_To_Vy_With_Carry_1()
        {
            var cpu = new Chip8CPU(new RAM(4096), null);
            var addVyToVx = new AddVyToVx(cpu);

            cpu.StoreIntoRegister(0, 0xFF);
            cpu.StoreIntoRegister(1, 0x1);

            addVyToVx.Execute(0x8014);

            Assert.Equal(1, cpu.GetFromRegister(0xF));

        }


        [Fact]
        public void Subtract_Vy_To_Vx()
        {
            var cpu = new Chip8CPU(new RAM(4096), null);
            var addVyToVx = new SubtractVyToVx(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            addVyToVx.Execute(0x8374);

            Assert.Equal(0, cpu.GetFromRegister(3));
        }
        [Fact]
        public void Subtract_Vy_To_Vx_With_Carry_1()
        {
            var cpu = new Chip8CPU(new RAM(4096), null);
            var addVyToVx = new SubtractVyToVx(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x2);

            addVyToVx.Execute(0x8374);

            Assert.Equal(1, cpu.GetFromRegister(0xf));
        }

        [Fact]
        public void Add_Vx_To_Vy_With_Decoder()
        {
            var cpu = new Chip8CPU(new RAM(4096), null);
            var decoder = new Decoder(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            decoder.Execute(0x8374);

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

    }
}
