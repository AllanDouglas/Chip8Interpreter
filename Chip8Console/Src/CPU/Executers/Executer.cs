namespace Chip8Console.CPU
{

    public abstract class Executer : IOpCodeExecuter
    {
        public abstract OpCode Filter { get; }
        public abstract OpCode OpCode { get; }
        protected ICPU cpu;
        public Executer(ICPU cpu) => this.cpu = cpu;

        public void Execute(ushort opcode) => Execute(new OpCode(opcode));
        public abstract void Execute(OpCode opcode);

        public override string ToString() => string.Format("{0} opcode: {1:X8}", GetType().Name, OpCode);

    }
}