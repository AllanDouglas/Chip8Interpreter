using System;

namespace Chip8Console.CPU
{
    public struct OpCode : IEquatable<OpCode>, IEquatable<ushort>
    {
        public readonly ushort value;

        public OpCode(ushort opcode)
        {
            value = opcode;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

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