using System;

namespace GameOfLifeKata.Models
{
    public class Generation
    {
        public const Int32 Rows = 50;
        public const Int32 Columns = 50;
        public Int32[,] Cells { get; set; }

        public Generation()
        {
            Cells = new Int32[Rows, Columns];
        }
    }
}