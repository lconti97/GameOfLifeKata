using System;

namespace GameOfLifeKata.Models
{
    public class Generation
    {
        public const Int32 Rows = 50;
        public const Int32 Columns = 50;
        public Cell[,] Cells { get; set; }

        public Generation()
        {
            Cells = new Cell[Rows, Columns];
        }

        public Generation(Cell[,] cells)
        {
            Cells = cells;
        }
    }
}