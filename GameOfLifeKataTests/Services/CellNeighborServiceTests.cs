using GameOfLifeDomain.Models;
using GameOfLifeKata.Models;
using GameOfLifeKata.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameOfLifeKataTests.Services
{
    [TestClass]
    public class CellNeighborServiceTests
    {
        private ICellNeighborService service;

        [TestInitialize]
        public void Initialize()
        {
            service = new CellNeighborService();
        }

        [TestMethod]
        public void GetAliveNeighborsCountOnCellWithNoNeighborsReturnsZero()
        {
            var coordinatesToPopulate = new[] { new Coordinate(1, 1) };
            var testCells = CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(testCells, 1, 1);

            Assert.AreEqual(0, aliveNeighbors);
        }

        [TestMethod]
        public void GetAliveNeighborsCountOnCellWithOneNeighborsReturnsOne()
        {
            var coordinatesToPopulate = new[] { new Coordinate(1, 1), new Coordinate(2, 1) };
            var testCells = CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(testCells, 1, 1);

            Assert.AreEqual(1, aliveNeighbors);
        }

        [TestMethod]
        public void GetAliveNeighborsCountOnCellWithEightNeighborsReturnsEight()
        {
            var coordinateToPopulate = new[] { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2),
                new Coordinate(1, 0), new Coordinate(1, 2), new Coordinate(2, 0), new Coordinate(2, 1), new Coordinate(2, 2) };
            var cellGrid = CreateCellGrid(coordinateToPopulate, 5, 5);

            var aliveNeighbors = service.GetAliveNeighborsCount(cellGrid, 1, 1);

            Assert.AreEqual(8, aliveNeighbors);

        }

        [TestMethod]
        public void GetAliveNeighborsCountOnBottomRightCornerCellWithTwoAliveNeighborsReturnsTwo()
        {
            var coordinatesToPopulate = new[] { new Coordinate(2, 1), new Coordinate(1, 1) };
            var cellGrid = CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(cellGrid, 2, 2);

            Assert.AreEqual(2, aliveNeighbors);
        }
        [TestMethod]
        public void GetAliveNeighborsCountOnTopLeftCornerCellWithTwoAliveNeighborsReturnsTwo()
        {
            var coordinatesToPopulate = new[] { new Coordinate(0, 1), new Coordinate(1, 0) };
            var cellGrid = CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(cellGrid, 0, 0);

            Assert.AreEqual(2, aliveNeighbors);
        }

        private Cell[,] CreateCellGrid(Coordinate[] locationsToPopulate, Int32 rows, Int32 columns)
        {
            var cells = new Cell[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    cells[row, column] = new Cell(LifeState.NeverAlive);
                }
            }

            foreach (var coordinate in locationsToPopulate)
            {
                cells[coordinate.Row, coordinate.Column] = new Cell(LifeState.Alive);
            }

            return cells;
        }

        private class Coordinate
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
