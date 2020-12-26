using System.Collections;
using System.Collections.Generic;

namespace Chip8Console.CPU
{
    public class OpCodeDecoder : IOpCodeDecoder, IEnumerable<IOpCodeExecuter>
    {

        private readonly List<IOpCodeExecuter> executers = new();
        public OpCodeDecoder(OpCode opCode, ICPU cpu)
        {
            Filter = opCode;
            Unknown = new Unknown(cpu);
        }

        private readonly IOpCodeExecuter Unknown;

        public virtual OpCode Filter { get; protected set; }

        public void Add(IOpCodeExecuter executer)
        {
            executers.Add(executer);
        }

        public IOpCodeExecuter Decode(OpCode opCode)
        {

            foreach (var executer in executers)
            {
                var filter = new OpCode((ushort)(opCode.value & executer.Filter.value));

                if (executer.OpCode != filter) continue;
                return executer;
            }

            return Unknown;
        }

        public IEnumerator<IOpCodeExecuter> GetEnumerator() => executers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => executers.GetEnumerator();
    }

}