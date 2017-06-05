using GameOfLifeDomain.Models.LifeStates;
using GameOfLifeKata.Models;
using System;

namespace GameOfLifeKataTests.Factories
{
    public class CellGridFactory
    {
        public Cell[,] CreateCellGrid(Coordinate[] locationsToPopulate, Int32 rows, Int32 columns)
        {
            var cells = new Cell[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    cells[row, column] = new Cell(new NeverAliveLifeState());
                }
            }

            foreach (var coordinate in locationsToPopulate)
            {
                cells[coordinate.Row, coordinate.Column] = new Cell(new AliveLifeState());
            }

            return cells;
        }

        public class Coordinate
        {
            public Int32 Row;
            public Int32 Column;

            public Coordinate(Int32 row, Int32 column)
            {
                this.Row = row;
                this.Column = column;
            }
        }
    }
}
