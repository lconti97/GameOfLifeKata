using GameOfLifeDomain;
using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using GameOfLifeDomain.Services;
using GameOfLifeKataTests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using static GameOfLifeKataTests.Factories.CellGridFactory;

namespace GameOfLifeKataTests
{
    [TestClass]
    public class GameOfLifeTests
    {
        private Mock<ICellNeighborService> mockCellNeighborService;
        private GameOfLife gameOfLife;
        private CellGridFactory cellGridFactory;

        public GameOfLifeTests()
        {
            mockCellNeighborService = new Mock<ICellNeighborService>();
            gameOfLife = new GameOfLife(mockCellNeighborService.Object);
            cellGridFactory = new CellGridFactory();
        }

        [TestMethod]
        public void GetNextGenerationWithOneAliveCellWithNoNeighborsKillsTheCell()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1) };
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 0, typeof(DeadLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithOneAliveCellWithOneNeighborKillsTheCell()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(1, 2) };
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 1, typeof(DeadLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithOneAliveCellWithTwoNeighborsKeepsTheCellAlive()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0) };
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 2, typeof(AliveLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithOneAliveCellWithThreeNeighborsKeepsTheCellAlive()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 2) };
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 3, typeof(AliveLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithOneAliveCellWithFourNeighborsKillsTheCell()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 2),
                new Coordinate(1, 2) };
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 4, typeof(DeadLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithOneNotAliveCellWithThreeNeighborsPopulatesTheCell()
        {
            var locationsToPopulate = new[] {new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 2) };
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 3, typeof(AliveLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithNeverAliveCellReturnsNeverAliveCells()
        {
            var locationsToPopulate = new Coordinate[0];
            AssertCellWithNAliveNeighborsWillHaveLifeState(locationsToPopulate, 0, typeof(NeverAliveLifeState));
        }

        [TestMethod]
        public void GetNextGenerationWithAliveCellThatDiesReturnsCellWithOneGenerationsSinceAlive()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(2, 2) };
            AssertCellWithNAliveNeighborsWillHaveGenerationsSinceAlive(locationsToPopulate, 1, 1);
        }

        [TestMethod]
        public void GetNextGenerationWithDeadCellWithOneGenerationsSinceAliveReturnsCellWithTwoGenerationsSinceAlive()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 1), new Coordinate(2, 2) };
            var cellGrid = cellGridFactory.CreateCellGrid(locationsToPopulate, 3, 3);
            cellGrid[1, 1] = new Cell(new DeadLifeState());

            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(1);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.AreEqual(2, (nextGeneration.Cells[1, 1].LifeState as DeadLifeState).GenerationsSinceAlive);
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        [TestMethod]
        public void GetNextGenerationOnHorizontalBlinkerReturnsVerticalBlinker()
        {
            var locationsToPopulate = new[] { new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(1, 2) };
            var cellGrid = cellGridFactory.CreateCellGrid(locationsToPopulate, 3, 3);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 0, 0)).Returns(2);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 0, 1)).Returns(3);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 0, 2)).Returns(2);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 0)).Returns(1);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(2);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 2)).Returns(1);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 2, 0)).Returns(2);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 2, 1)).Returns(3);
            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 2, 2)).Returns(2);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsInstanceOfType(nextGeneration.Cells[0, 0].LifeState, typeof(NeverAliveLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[0, 1].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[0, 2].LifeState, typeof(NeverAliveLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[1, 0].LifeState, typeof(DeadLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[1, 1].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[1, 2].LifeState, typeof(DeadLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[2, 0].LifeState, typeof(NeverAliveLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[2, 1].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(nextGeneration.Cells[2, 2].LifeState, typeof(NeverAliveLifeState));
        }

        private void AssertCellWithNAliveNeighborsWillHaveLifeState(Coordinate[] locationsToPopulate, Int32 numberOfNeighbors, Type TLifeState)
        {
            var cellGrid = cellGridFactory.CreateCellGrid(locationsToPopulate, 3, 3);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(numberOfNeighbors);
            
            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.IsInstanceOfType(nextGeneration.Cells[1, 1].LifeState, TLifeState);
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }

        private void AssertCellWithNAliveNeighborsWillHaveGenerationsSinceAlive(Coordinate[] locationsToPopulate, Int32 numberOfNeighbors, Int32 generationsSinceAlive)
        {
            var cellGrid = cellGridFactory.CreateCellGrid(locationsToPopulate, 3, 3);
            var currentGeneration = new Generation(cellGrid);

            mockCellNeighborService.Setup(s => s.GetAliveNeighborsCount(cellGrid, 1, 1)).Returns(numberOfNeighbors);

            var nextGeneration = gameOfLife.GetNextGeneration(currentGeneration);

            Assert.AreEqual(generationsSinceAlive, (nextGeneration.Cells[1, 1].LifeState as DeadLifeState).GenerationsSinceAlive);
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(cellGrid, 1, 1), Times.Once);
            mockCellNeighborService.Verify(s => s.GetAliveNeighborsCount(It.IsAny<Cell[,]>(), It.IsAny<Int32>(), It.IsAny<Int32>()), Times.Exactly(9));
        }
    }
}
