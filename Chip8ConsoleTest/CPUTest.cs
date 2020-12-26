using System;
using Chip8Console.Memory;
using Chip8Console.Video;
using Xunit;

namespace Chip8Console.CPU
{
    public class CPUTest
    {

        [Fact]
        public void ADD_Vx_To_Vy()
        {
            ICPU cpu = new Chip8CPU(new RAM(4096), null, null);
            AOpCodeExecuter addVyToVx = new OpCode8XY4(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            addVyToVx.Execute(0x8374);

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void ADD_Vx_To_Vy_With_Carry_1()
        {
            ICPU cpu = new Chip8CPU(new RAM(4096), null, null);
            AOpCodeExecuter addVyToVx = new OpCode8XY4(cpu);

            cpu.StoreIntoRegister(0, 0xFF);
            cpu.StoreIntoRegister(1, 0x1);

            addVyToVx.Execute(0x8014);

            Assert.Equal(1, cpu.GetFromRegister(0xF));
        }


        [Fact]
        public void Subtract_Vy_To_Vx()
        {
            var cpu = new Chip8CPU(new RAM(4096), null, null);
            var subtractVyToVx = new OpCode8XY5(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            subtractVyToVx.Execute(0x8374);

            Assert.Equal(0, cpu.GetFromRegister(3));
        }
        [Fact]
        public void Subtract_Vy_To_Vx_With_Carry_1()
        {
            var cpu = new Chip8CPU(new RAM(4096), null, null);

            var subtractVyToVx = new OpCode8XY5(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x2);

            subtractVyToVx.Execute(0x8374);

            Assert.Equal(1, cpu.GetFromRegister(0xf));
        }

        [Fact]
        public void Add_Vx_To_Vy_With_Decoder()
        {
            var cpu = new Chip8CPU(new RAM(4096), null, null);
            var decoder = new OpCodeDecoder(new OpCode(0x000F), cpu);
            var addVyToVx = new OpCode8XY4(cpu);

            decoder.Add(addVyToVx);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            OpCode opCode = new(0x8374);
            decoder.Decode(opCode).Execute(opCode);

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void Cpu_Tick()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
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
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var jumpTo = new OpCode1NNN(cpu);

            memory.Store(0x201, 0x83);
            memory.Store(0x202, 0x74);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            jumpTo.Execute(0x1201);
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Skips_If_Equals_To_Const_3XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var skips = new OpCode3XNN(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new OpCode(0x3A0A));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void SkipsIfEqualsToConst_Fail_3XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var skips = new OpCode3XNN(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new OpCode(0x3A01));
            cpu.Tick();

            Assert.NotEqual(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Skips_If_Not_Equals_To_Const_4XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var skips = new OpCode4XNN(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new OpCode(0x4A0A));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }
        [Fact]
        public void Skips_If_Not_Equals_To_Const_Fail_4XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var skips = new OpCode4XNN(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0xA);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new OpCode(0x4A01));
            cpu.Tick();

            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Skips_If_Vx_Equals_Vy_5XY0()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var skips = new OpCode4XNN(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0x1);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new OpCode(0x5A30));
            cpu.Tick();
            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Skips_If_Vx_Not_Equals_Vy_9XY0()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var skips = new OpCode4XNN(cpu);

            memory.Store(0x200, 0x00);
            memory.Store(0x201, 0x00);
            memory.Store(0x202, 0x00);
            memory.Store(0x203, 0x00);
            memory.Store(0x204, 0x83);
            memory.Store(0x205, 0x74);

            cpu.StoreIntoRegister(0xA, 0x2);
            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(7, 0x1);

            skips.Execute(new OpCode(0x5A30));
            cpu.Tick();
            Assert.Equal(2, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Add_Const_To_Vx_6XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var operation = new OpCode7XNN(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            operation.Execute(new OpCode(0x6305));

            Assert.Equal(6, cpu.GetFromRegister(3));
        }


        [Fact]
        public void Set_Const_To_Vx_6XNN()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var operation = new OpCode6XNN(cpu);

            operation.Execute(new OpCode(0x6A05));

            Assert.Equal(5, cpu.GetFromRegister(0xA));

        }

        [Fact]
        public void Set_Vy_To_Vx_9XY0()
        {
            var memory = new RAM(4096);
            var cpu = new Chip8CPU(memory, null, null);
            cpu.Start();
            var operation = new OpCode8XY0(cpu);

            cpu.StoreIntoRegister(3, 0x1);
            cpu.StoreIntoRegister(4, 0x5);

            operation.Execute(new OpCode(0x8340));

            Assert.Equal(5, cpu.GetFromRegister(3));
        }

        [Fact]
        public void Draw_Sprite_DXYN()
        {
            var memory = new RAM(4096);
            IGPU gpu = new GPU(64, 32);
            var cpu = new Chip8CPU(memory, gpu, null);
            cpu.Start();
            IOpCodeExecuter operation = new OpCodeDXYN(cpu);

            memory.Store(cpu.RegisterI, 0x3C);
            memory.Store((ushort)(cpu.RegisterI + 1), 0xC3);
            memory.Store((ushort)(cpu.RegisterI + 2), 0xFF);

            operation.Execute(new OpCode(0xD003));

            var videoSum = 0;

            foreach (var value in gpu.Dump())
            {
                videoSum += value;
            }

            Assert.Equal(16, videoSum);
        }

    }
}