using System;

namespace GameOfLifeDomain.Models
{
    public class Generation
    {
        public Cell[,] Cells { get; set; }

        public Generation(Int32 rows, Int32 columns)
        {
            Cells = new Cell[rows, columns];
        }

        public Generation(Cell[,] cells)
        {
            Cells = cells;
        }
    }
}