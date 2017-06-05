using GameOfLifeKata.Services;
using GameOfLifeKataTests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static GameOfLifeKataTests.Factories.CellGridFactory;

namespace GameOfLifeKataTests.Services
{
    [TestClass]
    public class CellNeighborServiceTests
    {
        private ICellNeighborService service;
        private CellGridFactory cellGridFactory;

        [TestInitialize]
        public void Initialize()
        {
            service = new CellNeighborService();
            cellGridFactory = new CellGridFactory();
        }

        [TestMethod]
        public void GetAliveNeighborsCountOnCellWithNoNeighborsReturnsZero()
        {
            var coordinatesToPopulate = new[] { new Coordinate(1, 1) };
            var testCells = cellGridFactory.CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(testCells, 1, 1);

            Assert.AreEqual(0, aliveNeighbors);
        }

        [TestMethod]
        public void GetAliveNeighborsCountOnCellWithOneNeighborsReturnsOne()
        {
            var coordinatesToPopulate = new[] { new Coordinate(1, 1), new Coordinate(2, 1) };
            var testCells = cellGridFactory.CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(testCells, 1, 1);

            Assert.AreEqual(1, aliveNeighbors);
        }

        [TestMethod]
        public void GetAliveNeighborsCountOnCellWithEightNeighborsReturnsEight()
        {
            var coordinateToPopulate = new[] { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2),
                new Coordinate(1, 0), new Coordinate(1, 2), new Coordinate(2, 0), new Coordinate(2, 1), new Coordinate(2, 2) };
            var cellGrid = cellGridFactory.CreateCellGrid(coordinateToPopulate, 5, 5);

            var aliveNeighbors = service.GetAliveNeighborsCount(cellGrid, 1, 1);

            Assert.AreEqual(8, aliveNeighbors);

        }

        [TestMethod]
        public void GetAliveNeighborsCountOnBottomRightCornerCellWithTwoAliveNeighborsReturnsTwo()
        {
            var coordinatesToPopulate = new[] { new Coordinate(2, 1), new Coordinate(1, 1) };
            var cellGrid = cellGridFactory.CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(cellGrid, 2, 2);

            Assert.AreEqual(2, aliveNeighbors);
        }
        [TestMethod]
        public void GetAliveNeighborsCountOnTopLeftCornerCellWithTwoAliveNeighborsReturnsTwo()
        {
            var coordinatesToPopulate = new[] { new Coordinate(0, 1), new Coordinate(1, 0) };
            var cellGrid = cellGridFactory.CreateCellGrid(coordinatesToPopulate, 3, 3);

            var aliveNeighbors = service.GetAliveNeighborsCount(cellGrid, 0, 0);

            Assert.AreEqual(2, aliveNeighbors);
        }
    }
}
