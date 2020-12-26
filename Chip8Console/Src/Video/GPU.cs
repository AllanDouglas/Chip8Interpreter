namespace Chip8Console.Video
{
    public class GPU : IGPU
    {
        public readonly int columns;
        private readonly int rows;
        private readonly byte[] gx;

        public int Columns => columns;
        public int Rows => rows;

        public GPU(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            gx = new byte[columns * rows];
        }
        public void Clear()
        {
            for (int i = 0; i < gx.Length; i++)
            {
                gx[i] = 0;
            }
        }
        public byte[] Dump() => gx;

        public void Store(ushort index, byte value) => gx[index] = value;

        public byte Read(ushort address) => gx[address];
    }
}
