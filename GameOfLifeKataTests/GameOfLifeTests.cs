using GameOfLifeDomain.Models;
using GameOfLifeKata.Domain;
using GameOfLifeKata.Models;
using GameOfLifeKata.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GameOfLifeKataTests
{
    [TestClass]
    public class GameOfLifeTests
    {
        private Mock<ICellNeighborService> mockCellNeighborService;
        private GameOfLife gameOfLife;

        public GameOfLifeTests()
        {
            mockCellNeighborService = new Mock<ICellNeighborService>();
            gameOfLife = new GameOfLife(mockCellNeighborService.Object);
        }

        [TestMethod]
        public void GetNextGenerationOnGenerationWithOneAliveCellWithNoNeighborsKillsTheCell()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1) };
            var cellGrid = CreateCellGrid(locationsToPopulate);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(0);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsFalse(nextGeneration.Cells[1, 1].IsAlive());
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            //mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        [TestMethod]
        public void GetNextGenerationOnGenerationWithOneAliveCellWithOneNeighborKillsTheCell()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(1, 2) };
            var cellGrid = CreateCellGrid(locationsToPopulate);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(1);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsFalse(nextGeneration.Cells[1, 1].IsAlive());
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
          //  mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        [TestMethod]
        public void GetNextGenerationOnGenerationWithOneAliveCellWithTwoNeighborsKeepsTheCellAlive()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0) };
            var cellGrid = CreateCellGrid(locationsToPopulate);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(2);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsTrue(nextGeneration.Cells[1, 1].IsAlive());
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
           // mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        [TestMethod]
        public void GetNextGenerationOnGenerationWithOneAliveCellWithThreeNeighborsKeepsTheCellAlive()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 2) };
            var cellGrid = CreateCellGrid(locationsToPopulate);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(3);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsTrue(nextGeneration.Cells[1, 1].IsAlive());
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            //mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        [TestMethod]
        public void GetNextGenerationOnGenerationWithOneAliveCellWithFourNeighborsKillsTheCell()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 2),
                new Coordinate(1, 2) };
            var cellGrid = CreateCellGrid(locationsToPopulate);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(4);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsFalse(nextGeneration.Cells[1, 1].IsAlive());
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            //mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        [TestMethod]
        public void GetNextGenerationOnGenerationWithOneNotAliveCellWithThreeNeighborsPopulatesTheCell()
        {
            var locationsToPopulate = new[] {new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 2) };
            var cellGrid = CreateCellGrid(locationsToPopulate);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(3);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsTrue(nextGeneration.Cells[1, 1].IsAlive());
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            //mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        private Cell[,] CreateCellGrid(Coordinate[] locationsToPopulate)
        {
            var cells = new Cell[Generation.Rows, Generation.Columns];
            for (var row = 0; row < Generation.Rows; row++)
            {
                for (var column = 0; column < Generation.Columns; column++)
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
