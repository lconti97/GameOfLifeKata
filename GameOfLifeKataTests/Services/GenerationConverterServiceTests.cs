using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using GameOfLifeDomain.Services;
using GameOfLifeKataTests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static GameOfLifeKataTests.Factories.CellGridFactory;

namespace GameOfLifeKataTests.Services
{
    [TestClass]
    public class GenerationConverterServiceTests
    {
        private CellGridFactory cellGridFactory;
        private GenerationConverterService generationConverter;
        private LifeStateGeneratorService lifeStateGeneratorService;

        public GenerationConverterServiceTests()
        {
            cellGridFactory = new CellGridFactory();
            lifeStateGeneratorService = new LifeStateGeneratorService();
            generationConverter = new GenerationConverterService(lifeStateGeneratorService);
        }

        [TestMethod]
        public void CreateGenerationOnGridOfNegativeOnesReturnsGenerationOfNeverAliveCells()
        {
            var generationsSinceAliveGrid = new Int32[2, 2];
            generationsSinceAliveGrid[0, 0] = -1;
            generationsSinceAliveGrid[0, 1] = -1;
            generationsSinceAliveGrid[1, 0] = -1;
            generationsSinceAliveGrid[1, 1] = -1;

            var generation = generationConverter.CreateGeneration(generationsSinceAliveGrid);

            var expectedGenerationsSinceAlive = -1;
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 1].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 1].LifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void CreateGenerationOnGridOfZeroesReturnsGenerationOfAliveCells()
        {
            var generationsSinceAliveGrid = new Int32[2, 2];
            generationsSinceAliveGrid[0, 0] = 0;
            generationsSinceAliveGrid[0, 1] = 0;
            generationsSinceAliveGrid[1, 0] = 0;
            generationsSinceAliveGrid[1, 1] = 0;

            var generation = generationConverter.CreateGeneration(generationsSinceAliveGrid);

            var expectedGenerationsSinceAlive = 0;
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 1].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 1].LifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void CreateGenerationOnGridOfOnesReturnsGenerationOfNewDeadCells()
        {
            var generationsSinceAliveGrid = new Int32[2, 2];
            generationsSinceAliveGrid[0, 0] = 1;
            generationsSinceAliveGrid[0, 1] = 1;
            generationsSinceAliveGrid[1, 0] = 1;
            generationsSinceAliveGrid[1, 1] = 1;

            var generation = generationConverter.CreateGeneration(generationsSinceAliveGrid);

            var expectedGenerationsSinceAlive = 1;
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 1].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 1].LifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void CreateGenerationOnGridOfFoursReturnsGenerationOfDeadCellsWithGenerationsSinceAliveFour()
        {
            var generationsSinceAliveGrid = new Int32[2, 2];
            generationsSinceAliveGrid[0, 0] = 4;
            generationsSinceAliveGrid[0, 1] = 4;
            generationsSinceAliveGrid[1, 0] = 4;
            generationsSinceAliveGrid[1, 1] = 4;

            var generation = generationConverter.CreateGeneration(generationsSinceAliveGrid);

            var expectedGenerationsSinceAlive = 4;
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[0, 1].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(expectedGenerationsSinceAlive, generation.Cells[1, 1].LifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void CreateGenerationOnGridWithMixedGenerationsSinceAliveReturnsGenerationOfCorrectCells()
        {
            var generationsSinceAliveGrid = new Int32[2, 2];
            generationsSinceAliveGrid[0, 0] = -1;
            generationsSinceAliveGrid[0, 1] = 0;
            generationsSinceAliveGrid[1, 0] = 1;
            generationsSinceAliveGrid[1, 1] = 3;

            var generation = generationConverter.CreateGeneration(generationsSinceAliveGrid);

            Assert.AreEqual(-1, generation.Cells[0, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(0, generation.Cells[0, 1].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(1, generation.Cells[1, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(3, generation.Cells[1, 1].LifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void CreateGenerationsSinceAliveGridOnGenerationOfNeverAliveCellsReturnsGridOfNegativeOnes()
        {
            var locationsToPopulate = new Coordinate[0];
            var cellGrid = cellGridFactory.CreateCellGrid(locationsToPopulate, 2, 2);
            var generation = new Generation(cellGrid);

            var generationsSinceAliveGrid = generationConverter.CreateGenerationsSinceAliveGrid(generation);

            var expectedGenerationsSinceAlive = new NeverAliveLifeState().GenerationsSinceAlive;
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 1]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 1]);
        }

        [TestMethod]
        public void CreateGenerationsSinceAliveGridOnGenerationOfAliveCellsReturnsGridOfZeroes()
        {
            var locationsToPopulate = new[] { new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(0, 1), new Coordinate(1, 1) };
            var cellGrid = cellGridFactory.CreateCellGrid(locationsToPopulate, 2, 2);
            var generation = new Generation(cellGrid);

            var generationsSinceAliveGrid = generationConverter.CreateGenerationsSinceAliveGrid(generation);

            var expectedGenerationsSinceAlive = new AliveLifeState().GenerationsSinceAlive;
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 1]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 1]);
        }

        [TestMethod]
        public void CreateGenerationsSinceAliveGridOnGenerationOfNewDeadCellsReturnsGridOfOnes()
        {
            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new DeadLifeState());
            cellGrid[0, 1] = new Cell(new DeadLifeState());
            cellGrid[1, 0] = new Cell(new DeadLifeState());
            cellGrid[1, 1] = new Cell(new DeadLifeState());
            var generation = new Generation(cellGrid);

            var generationsSinceAliveGrid = generationConverter.CreateGenerationsSinceAliveGrid(generation);

            var expectedGenerationsSinceAlive = new DeadLifeState().GenerationsSinceAlive;
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 1]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 1]);
        }

        [TestMethod]
        public void CreateGenerationsSinceAliveGridOnGenerationOfDeadCellsWithGenerationsSinceAliveFourReturnsGridOfFours()
        {
            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new DeadLifeState(4));
            cellGrid[0, 1] = new Cell(new DeadLifeState(4));
            cellGrid[1, 0] = new Cell(new DeadLifeState(4));
            cellGrid[1, 1] = new Cell(new DeadLifeState(4));
            var generation = new Generation(cellGrid);

            var generationsSinceAliveGrid = generationConverter.CreateGenerationsSinceAliveGrid(generation);

            var expectedGenerationsSinceAlive = new DeadLifeState(4).GenerationsSinceAlive;
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[0, 1]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 0]);
            Assert.AreEqual(expectedGenerationsSinceAlive, generationsSinceAliveGrid[1, 1]);
        }

        [TestMethod]
        public void CreateGenerationsSinceAliveGridOnGenerationOfMixedCellsReturnsGridWithCorrectGenerationsSinceAlive()
        {
            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new NeverAliveLifeState());
            cellGrid[0, 1] = new Cell(new AliveLifeState());
            cellGrid[1, 0] = new Cell(new DeadLifeState());
            cellGrid[1, 1] = new Cell(new DeadLifeState(3));
            var generation = new Generation(cellGrid);

            var generationsSinceAliveGrid = generationConverter.CreateGenerationsSinceAliveGrid(generation);

            Assert.AreEqual(new NeverAliveLifeState().GenerationsSinceAlive, generationsSinceAliveGrid[0, 0]);
            Assert.AreEqual(new AliveLifeState().GenerationsSinceAlive, generationsSinceAliveGrid[0, 1]);
            Assert.AreEqual(new DeadLifeState().GenerationsSinceAlive, generationsSinceAliveGrid[1, 0]);
            Assert.AreEqual(new DeadLifeState(3).GenerationsSinceAlive, generationsSinceAliveGrid[1, 1]);
        }
    }
}
