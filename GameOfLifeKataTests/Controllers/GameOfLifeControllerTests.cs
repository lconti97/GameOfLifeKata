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

namespace GameOfLifeKataTests.Controllers
{
    [TestClass]
    public class GameOfLifeControllerTests
    {
        private Mock<IGameOfLife> mockGameOfLife;
        private Mock<IGenerationEncoderService> mockGenerationEncoderService;
        private GameOfLifeController controller;
        private CellGridFactory cellGridFactory;

        public GameOfLifeControllerTests()
        {
            mockGameOfLife = new Mock<IGameOfLife>();
            mockGenerationEncoderService = new Mock<IGenerationEncoderService>();
            controller = new GameOfLifeController(mockGameOfLife.Object, mockGenerationEncoderService.Object);
            cellGridFactory = new CellGridFactory();
        }

        [TestMethod]
        public void PostInitialGenerationReturnsOkResultsOnSuccess()
        {
            var encodedGenerationInput = new Int32[2][];
            encodedGenerationInput[0] = new Int32[2];
            encodedGenerationInput[1] = new Int32[2];

            encodedGenerationInput[0][0] = 0;
            encodedGenerationInput[0][1] = 0;
            encodedGenerationInput[1][0] = 0;
            encodedGenerationInput[1][1] = 0;

            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new AliveLifeState());
            cellGrid[0, 1] = new Cell(new AliveLifeState());
            cellGrid[1, 0] = new Cell(new AliveLifeState());
            cellGrid[1, 1] = new Cell(new AliveLifeState());

            var decodedGenerationToPost = new Generation(cellGrid);

            mockGenerationEncoderService.Setup(e => e.Decode(encodedGenerationInput)).Returns(decodedGenerationToPost);

            var result = controller.PostInitialGeneration(encodedGenerationInput) as OkNegotiatedContentResult<Int32>;

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Int32>));

            mockGenerationEncoderService.Verify(e => e.Decode(encodedGenerationInput), Times.Once);
            mockGenerationEncoderService.Verify(e => e.Decode(It.IsAny<Int32[][]>()), Times.Once);
        }

        [TestMethod]
        public void PostInitialGenerationUpdatesStoredCurrentGeneration()
        {
            var encodedGenerationInput = new Int32[2][];
            encodedGenerationInput[0] = new Int32[2];
            encodedGenerationInput[1] = new Int32[2];

            encodedGenerationInput[0][0] = 0;
            encodedGenerationInput[0][1] = 0;
            encodedGenerationInput[1][0] = 0;
            encodedGenerationInput[1][1] = 0;

            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new AliveLifeState());
            cellGrid[0, 1] = new Cell(new AliveLifeState());
            cellGrid[1, 0] = new Cell(new AliveLifeState());
            cellGrid[1, 1] = new Cell(new AliveLifeState());

            var decodedGenerationToPost = new Generation(cellGrid);

            mockGenerationEncoderService.Setup(e => e.Decode(encodedGenerationInput)).Returns(decodedGenerationToPost);

            controller.PostInitialGeneration(encodedGenerationInput);

            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[0, 0].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[0, 1].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[1, 0].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[1, 1].LifeState, typeof(AliveLifeState));

            mockGenerationEncoderService.Verify(e => e.Decode(encodedGenerationInput), Times.Once);
            mockGenerationEncoderService.Verify(e => e.Decode(It.IsAny<Int32[][]>()), Times.Once);
        }

        [TestMethod]
        public void GetNextGenerationReturnsBadRequestWhenGridHasNotBeenInitialized()
        {
            var result = controller.GetNextGeneration();

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        private Generation CreateInitialGeneration()
        {
            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new NeverAliveLifeState());
            cellGrid[0, 1] = new Cell(new AliveLifeState());
            cellGrid[1, 0] = new Cell(new AliveLifeState());
            cellGrid[1, 1] = new Cell(new AliveLifeState());

            return new Generation(cellGrid);
        }

        [TestMethod]
        public void GetNextGenerationReturnsOkResultsOnSuccess()
        {
            CreateInitialGeneration();

            var result = controller.GetNextGeneration() as OkNegotiatedContentResult<Int32>;

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Int32>));
        }

        [TestMethod]
        public void GetNextGenerationReturnsEncodedGenerationOnSuccess()
        {
            var initialGeneration = CreateInitialGeneration();
            GameOfLifeData.CurrentGeneration = initialGeneration;

            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new AliveLifeState());
            cellGrid[0, 1] = new Cell(new DeadLifeState());
            cellGrid[1, 0] = new Cell(new DeadLifeState());
            cellGrid[1, 1] = new Cell(new DeadLifeState());

            var generationToReturn = new Generation(cellGrid);

            mockGameOfLife.Setup(g => g.GetNextGeneration(initialGeneration)).Returns(generationToReturn);

            var encodedCellsToReturn = new Int32[2, 2];
            encodedCellsToReturn[0, 0] = 0;
            encodedCellsToReturn[0, 1] = 1;
            encodedCellsToReturn[1, 0] = 1;
            encodedCellsToReturn[1, 1] = 1;

            mockGenerationEncoderService.Setup(e => e.Encode(generationToReturn)).Returns(encodedCellsToReturn);

            var result = controller.GetNextGeneration() as OkNegotiatedContentResult<Int32[,]>;

            mockGameOfLife.Verify(g => g.GetNextGeneration(initialGeneration), Times.Once);
            mockGameOfLife.Verify(g => g.GetNextGeneration(It.IsAny<Generation>()), Times.Once);

            mockGenerationEncoderService.Verify(g => g.Encode(generationToReturn), Times.Once);
            mockGenerationEncoderService.Verify(g => g.Encode(It.IsAny<Generation>()), Times.Once);

            Assert.IsNotNull(result.Content);

            var returnedEncodedCells = result.Content as Int32[,];
            Assert.AreEqual(0, returnedEncodedCells[0, 0]);
            Assert.AreEqual(1, returnedEncodedCells[0, 1]);
            Assert.AreEqual(1, returnedEncodedCells[1, 0]);
            Assert.AreEqual(1, returnedEncodedCells[1, 1]);

        }

        [TestMethod]
        public void GetNextGenerationUpdatesStoredCurrentGeneration()
        {
            var initialGeneration = CreateInitialGeneration();
            GameOfLifeData.CurrentGeneration = initialGeneration;

            var cellGrid = new Cell[2, 2];
            cellGrid[0, 0] = new Cell(new AliveLifeState());
            cellGrid[0, 1] = new Cell(new DeadLifeState());
            cellGrid[1, 0] = new Cell(new DeadLifeState());
            cellGrid[1, 1] = new Cell(new DeadLifeState());

            var generationToReturn = new Generation(cellGrid);

            mockGameOfLife.Setup(g => g.GetNextGeneration(initialGeneration)).Returns(generationToReturn);

            controller.GetNextGeneration();
            
            mockGameOfLife.Verify(g => g.GetNextGeneration(initialGeneration), Times.Once);
            mockGameOfLife.Verify(g => g.GetNextGeneration(It.IsAny<Generation>()), Times.Once);

            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[0, 0].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[0, 1].LifeState, typeof(DeadLifeState));
            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[1, 0].LifeState, typeof(DeadLifeState));
            Assert.IsInstanceOfType(GameOfLifeData.CurrentGeneration.Cells[1, 1].LifeState, typeof(DeadLifeState));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var controller = new GameOfLifeController(new GameOfLife(new CellNeighborService()), new GenerationEncoderService());

            var inputGrid = new Int32[10][];
            for (int i = 0; i < inputGrid.Length; i++)
            {
                inputGrid[i] = new Int32[10];
                for (int j = 0; j < inputGrid[0].Length; j++)
                {
                    inputGrid[i][j] = -1;
                }
            }

            inputGrid[1][0] = 0;
            inputGrid[1][1] = 0;
            inputGrid[1][2] = 0;

            controller.PostInitialGeneration(inputGrid);
            var nextGen = (controller.GetNextGeneration() as OkNegotiatedContentResult<Int32[,]>).Content;

            Assert.AreEqual(-1, nextGen[0, 0]);
            Assert.AreEqual(0, nextGen[0, 1]);
            Assert.AreEqual(-1, nextGen[0, 2]);
            Assert.AreEqual(1, nextGen[1, 0]);
            Assert.AreEqual(0, nextGen[1, 1]);
            Assert.AreEqual(1, nextGen[1, 2]);
            Assert.AreEqual(-1, nextGen[2, 0]);
            Assert.AreEqual(0, nextGen[2, 1]);
            Assert.AreEqual(-1, nextGen[2, 2]);
        }
    }

    
}
