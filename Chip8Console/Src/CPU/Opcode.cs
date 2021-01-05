using System;

namespace Chip8Console.CPU
{
    public struct OpCode : IEquatable<OpCode>, IEquatable<ushort>
    {
        public readonly ushort value;
        public ushort X => (ushort)((value & 0x0F00) >> 8);
        public ushort Y => (ushort)((value & 0x00F0) >> 4);
        public ushort Address => (ushort)(value & 0x0FFF);
        public byte Constant4 => (byte)(value & 0x000F);
        public byte Constant8 => (byte)(value & 0x00FF);

        public OpCode(ushort opcode) => value = opcode;

        public override bool Equals(object obj) => base.Equals(obj);

        public bool Equals(OpCode other) => this == other;

        public bool Equals(ushort other) => value == other;

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => $"{value:x4}";

        public static bool operator !=(OpCode a, OpCode b) => !(a == b);
        public static bool operator ==(OpCode a, OpCode b) => a.value == b.value;
        public static bool operator ==(OpCode a, ushort b) => a.value == b;
        public static bool operator !=(OpCode a, ushort b) => !(a.value == b);
    }
}