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
            ICPU cpu = new Chip8CPU(new RAM(4096), null);
            OpcodeDecoder addVyToVx = new AddVyToVx(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            addVyToVx.Execute(0x8374);

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void ADD_Vx_To_Vy_With_Carry_1()
        {
            ICPU cpu = new Chip8CPU(new RAM(4096), null);
            OpcodeDecoder addVyToVx = new AddVyToVx(cpu);

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
            var decoder = new GeneralDecoder(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            decoder.Execute(0x8374);

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void Cpu_Tick()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            memory.Store(0x200, 0x83);
            memory.Store(0x201, 0x74);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Jump_To()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            var jumpTo = new JumpTo(cpu);

            memory.Store(0x201, 0x83);
            memory.Store(0x202, 0x74);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            jumpTo.Execute(0x1201);
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void SkipsIfEqualsToConst_3XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            var skips = new SkipIfEqualsToConst(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new Opcode(0x3A0A));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void SkipsIfEqualsToConst_Fail_3XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            var skips = new SkipIfEqualsToConst(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new Opcode(0x3A01));
            cpu.Tick();

            Assert.NotEqual(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void SkipsIfNotEqualsToConst_4XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            var skips = new SkipIfNotEqualstoConst(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new Opcode(0x4A0A));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void SkipsIfNotEqualsToConst_Fail_4XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            var skips = new SkipIfNotEqualstoConst(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new Opcode(0x4A01));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void SkipsIfVxEqualsVy_5XY0()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null);
            cpu.Start();
            var skips = new SkipIfNotEqualstoConst(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0x1);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new Opcode(0x5A30));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

    }
}
