using GameOfLifeDomain;
using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using GameOfLifeDomain.Services;
using GameOfLifeKata.Controllers;
using GameOfLifeKataTests.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http.Results;
using static GameOfLifeKataTests.Factories.CellGridFactory;

namespace GameOfLifeKataTests.Controllers
{
    [TestClass]
    public class GameOfLifeControllerTests
    {
        private Mock<IGameOfLife> mockGameOfLife;
        private Mock<IGenerationConverterService> mockGenerationConverterService;
        private GameOfLifeController controller;
        private Generation currentGeneration;
        private CellGridFactory cellGridFactory;
        private LifeStateGeneratorService lifeStateGeneratorService;

        public GameOfLifeControllerTests()
        {
            mockGameOfLife = new Mock<IGameOfLife>();
            mockGenerationConverterService = new Mock<IGenerationConverterService>();
            controller = new GameOfLifeController(mockGameOfLife.Object, mockGenerationConverterService.Object);
            currentGeneration = new Generation(50, 50);
            cellGridFactory = new CellGridFactory();
            lifeStateGeneratorService = new LifeStateGeneratorService();
        }

        [TestMethod]
        public void PostInitialGenerationReturnsOkResultsOnSuccess()
        {
            var initialGenerationsSinceAliveGrid = new Int32[2, 2];
            initialGenerationsSinceAliveGrid[0, 0] = 0;
            initialGenerationsSinceAliveGrid[0, 1] = 0;
            initialGenerationsSinceAliveGrid[1, 0] = 0;
            initialGenerationsSinceAliveGrid[1, 1] = 0;

            var result = controller.PostInitialGeneration(initialGenerationsSinceAliveGrid) as OkNegotiatedContentResult<Int32>;

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Int32>));
        }

        [TestMethod]
        public void PostInitialGenerationUpdatesStoredCurrentGeneration()
        {
            var initialGenerationsSinceAliveGrid = new Int32[2, 2];
            initialGenerationsSinceAliveGrid[0, 0] = 0;
            initialGenerationsSinceAliveGrid[0, 1] = -1;
            initialGenerationsSinceAliveGrid[1, 0] = 0;
            initialGenerationsSinceAliveGrid[1, 1] = -1;

            controller.PostInitialGeneration(initialGenerationsSinceAliveGrid);

            var updatedStoredCellGrid = GameOfLifeData.CurrentGeneration.Cells;

            Assert.AreEqual(new Cell(lifeStateGeneratorService.Generate(0)).LifeState.GenerationsSinceAlive, updatedStoredCellGrid[0, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(new Cell(lifeStateGeneratorService.Generate(- 1)).LifeState.GenerationsSinceAlive, updatedStoredCellGrid[0, 1].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(new Cell(lifeStateGeneratorService.Generate(0)).LifeState.GenerationsSinceAlive, updatedStoredCellGrid[1, 0].LifeState.GenerationsSinceAlive);
            Assert.AreEqual(new Cell(lifeStateGeneratorService.Generate(- 1)).LifeState.GenerationsSinceAlive, updatedStoredCellGrid[1, 1].LifeState.GenerationsSinceAlive);
        }

        [TestMethod]
        public void GetNextGenerationReturnsBadRequestWhenGridHasNotBeenInitialized()
        {
            var result = controller.GetNextGeneration();

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void GetNextGenerationReturnsOkResultsOnSuccess()
        {
            var locationsToPopulate = new[] { new Coordinate(0, 0), new Coordinate(1, 0) };
            GameOfLifeData.CurrentGeneration = new Generation(cellGridFactory.CreateCellGrid(locationsToPopulate, 2, 2));

            var result = controller.GetNextGeneration() as OkNegotiatedContentResult<Int32>;

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Int32>));
        }

        [TestMethod]
        public void GetNextGenerationReturnsContentOnSuccess()
        {
            var locationsToPopulate = new[] { new Coordinate(0, 0), new Coordinate(1, 0) };
            var currentGeneration = new Generation(cellGridFactory.CreateCellGrid(locationsToPopulate, 2, 2));
            GameOfLifeData.CurrentGeneration = currentGeneration;
            var generationToReturn = new Generation(cellGridFactory.CreateCellGrid(new Coordinate[0], 2, 2));

            mockGameOfLife.Setup(g => g.GetNextGeneration(currentGeneration)).Returns(generationToReturn);

            var result = controller.GetNextGeneration() as OkNegotiatedContentResult<Int32[,]>;

            mockGameOfLife.Verify(g => g.GetNextGeneration(currentGeneration), Times.Once);
            mockGameOfLife.Verify(g => g.GetNextGeneration(It.IsAny<Generation>()), Times.Once);

            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void GetNextGenerationUpdatesStoredCurrentGeneration()
        {
            var locationsToPopulate = new[] { new Coordinate(0, 0), new Coordinate(1, 0) };
            var currentGeneration = new Generation(cellGridFactory.CreateCellGrid(locationsToPopulate, 2, 2));
            GameOfLifeData.CurrentGeneration = currentGeneration;
            var generationToReturn = new Generation(cellGridFactory.CreateCellGrid(new Coordinate[0], 2, 2));

            mockGameOfLife.Setup(g => g.GetNextGeneration(currentGeneration)).Returns(generationToReturn);

            var result = controller.GetNextGeneration() as OkNegotiatedContentResult<Int32[,]>;
            var resultCellGrid = GameOfLifeData.CurrentGeneration.Cells;

            mockGameOfLife.Verify(g => g.GetNextGeneration(currentGeneration), Times.Once);
            mockGameOfLife.Verify(g => g.GetNextGeneration(It.IsAny<Generation>()), Times.Once);


            Assert.AreEqual(new NeverAliveLifeState().GenerationsSinceAlive, resultCellGrid[0, 0]);
            Assert.AreEqual(new NeverAliveLifeState().GenerationsSinceAlive, resultCellGrid[0, 1]);
            Assert.AreEqual(new NeverAliveLifeState().GenerationsSinceAlive, resultCellGrid[1, 0]);
            Assert.AreEqual(new NeverAliveLifeState().GenerationsSinceAlive, resultCellGrid[1, 1]);
        }
    }
}
