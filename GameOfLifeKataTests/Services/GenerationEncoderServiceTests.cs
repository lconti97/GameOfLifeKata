using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using GameOfLifeDomain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameOfLifeKataTests.Services
{
    [TestClass]
    public class GenerationEncoderServiceTests
    {
        private const Int32 NeverAliveCode = GenerationEncoderService.NeverAliveCode;
        private const Int32 AliveCode = GenerationEncoderService.AliveCode;

        private GenerationEncoderService generationConverter;

        public GenerationEncoderServiceTests()
        {

            generationConverter = new GenerationEncoderService();
        }

        [TestMethod]
        public void DecodeOnGridOfNeverAliveCodesReturnsGenerationOfNeverAliveCells()
        {
            var encodedGrid = new Int32[2][];
            encodedGrid[0] = new Int32[2];
            encodedGrid[1] = new Int32[2];

            encodedGrid[0][0] = NeverAliveCode;
            encodedGrid[0][1] = NeverAliveCode;
            encodedGrid[1][0] = NeverAliveCode;
            encodedGrid[1][1] = NeverAliveCode;

            var decodedGeneration = generationConverter.Decode(encodedGrid);

            var expectedType = typeof(NeverAliveLifeState);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 1].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 1].LifeState, expectedType);

        }

        [TestMethod]
        public void DecodeOnGridOfAliveCodesReturnsGenerationOfAliveCells()
        {
            var encodedGrid = new Int32[2][];
            encodedGrid[0] = new Int32[2];
            encodedGrid[1] = new Int32[2];

            encodedGrid[0][0] = AliveCode;
            encodedGrid[0][1] = AliveCode;
            encodedGrid[1][0] = AliveCode;
            encodedGrid[1][1] = AliveCode;

            var decodedGeneration = generationConverter.Decode(encodedGrid);

            var expectedType = typeof(AliveLifeState);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 1].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 1].LifeState, expectedType);
        }

        [TestMethod]
        public void DecodeOnGridOfOnesReturnsGenerationOfDeadCellsWithGenerationsSinceAliveOne()
        {
            var encodedGrid = new Int32[2][];
            encodedGrid[0] = new Int32[2];
            encodedGrid[1] = new Int32[2];

            encodedGrid[0][0] = 1;
            encodedGrid[0][1] = 1;
            encodedGrid[1][0] = 1;
            encodedGrid[1][1] = 1;

            var decodedGeneration = generationConverter.Decode(encodedGrid);

            var expectedType = typeof(DeadLifeState);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 1].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 1].LifeState, expectedType);

            Assert.AreEqual((decodedGeneration.Cells[0, 0].LifeState as DeadLifeState).GenerationsSinceAlive, 1);
            Assert.AreEqual((decodedGeneration.Cells[0, 1].LifeState as DeadLifeState).GenerationsSinceAlive, 1);
            Assert.AreEqual((decodedGeneration.Cells[1, 0].LifeState as DeadLifeState).GenerationsSinceAlive, 1);
            Assert.AreEqual((decodedGeneration.Cells[1, 1].LifeState as DeadLifeState).GenerationsSinceAlive, 1);
        }

        [TestMethod]
        public void DecodeOnGridOfFoursReturnsGenerationOfDeadCellsWithGenerationsSinceAliveFour()
        {
            var encodedGrid = new Int32[2][];
            encodedGrid[0] = new Int32[2];
            encodedGrid[1] = new Int32[2];

            encodedGrid[0][0] = 4;
            encodedGrid[0][1] = 4;
            encodedGrid[1][0] = 4;
            encodedGrid[1][1] = 4;

            var decodedGeneration = generationConverter.Decode(encodedGrid);

            var expectedType = typeof(DeadLifeState);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 1].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 0].LifeState, expectedType);
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 1].LifeState, expectedType);

            Assert.AreEqual((decodedGeneration.Cells[0, 0].LifeState as DeadLifeState).GenerationsSinceAlive, 4);
            Assert.AreEqual((decodedGeneration.Cells[0, 1].LifeState as DeadLifeState).GenerationsSinceAlive, 4);
            Assert.AreEqual((decodedGeneration.Cells[1, 0].LifeState as DeadLifeState).GenerationsSinceAlive, 4);
            Assert.AreEqual((decodedGeneration.Cells[1, 1].LifeState as DeadLifeState).GenerationsSinceAlive, 4);
        }

        [TestMethod]
        public void DecodeOnGridWithMixedValuesReturnsGenerationOfCorrectCells()
        {
            var encodedGrid = new Int32[2][];
            encodedGrid[0] = new Int32[2];
            encodedGrid[1] = new Int32[2];

            encodedGrid[0][0] = NeverAliveCode;
            encodedGrid[0][1] = AliveCode;
            encodedGrid[1][0] = 1;
            encodedGrid[1][1] = 4;

            var decodedGeneration = generationConverter.Decode(encodedGrid);

            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 0].LifeState, typeof(NeverAliveLifeState));
            Assert.IsInstanceOfType(decodedGeneration.Cells[0, 1].LifeState, typeof(AliveLifeState));
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 0].LifeState, typeof(DeadLifeState));
            Assert.IsInstanceOfType(decodedGeneration.Cells[1, 1].LifeState, typeof(DeadLifeState));

            Assert.AreEqual((decodedGeneration.Cells[1, 0].LifeState as DeadLifeState).GenerationsSinceAlive, 1);
            Assert.AreEqual((decodedGeneration.Cells[1, 1].LifeState as DeadLifeState).GenerationsSinceAlive, 4);
        }

        [TestMethod]
        public void EncodeOnGenerationOfNeverAliveCellsReturnsGridOfNeverAliveCodes()
        {
            var cellGrid = new Cell[2, 2];

            cellGrid[0, 0] = new Cell(new NeverAliveLifeState());
            cellGrid[0, 1] = new Cell(new NeverAliveLifeState());
            cellGrid[1, 0] = new Cell(new NeverAliveLifeState());
            cellGrid[1, 1] = new Cell(new NeverAliveLifeState());

            var generation = new Generation(cellGrid);

            var encodedGeneration = generationConverter.Encode(generation);

            var expectedValue = NeverAliveCode;
            Assert.AreEqual(expectedValue, encodedGeneration[0, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[0, 1]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 1]);
        }

        [TestMethod]
        public void EncodeOnGenerationOfAliveCellsReturnsGridOfAliveCodes()
        {
            var cellGrid = new Cell[2, 2];

            cellGrid[0, 0] = new Cell(new AliveLifeState());
            cellGrid[0, 1] = new Cell(new AliveLifeState());
            cellGrid[1, 0] = new Cell(new AliveLifeState());
            cellGrid[1, 1] = new Cell(new AliveLifeState());

            var generation = new Generation(cellGrid);

            var encodedGeneration = generationConverter.Encode(generation);

            var expectedValue = AliveCode;
            Assert.AreEqual(expectedValue, encodedGeneration[0, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[0, 1]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 1]);
        }

        [TestMethod]
        public void EncodeOnGenerationOfNewDeadCellsReturnsGridOfOnes()
        {
            var cellGrid = new Cell[2, 2];

            cellGrid[0, 0] = new Cell(new DeadLifeState());
            cellGrid[0, 1] = new Cell(new DeadLifeState());
            cellGrid[1, 0] = new Cell(new DeadLifeState());
            cellGrid[1, 1] = new Cell(new DeadLifeState());

            var generation = new Generation(cellGrid);

            var encodedGeneration = generationConverter.Encode(generation);

            var expectedValue = 1;
            Assert.AreEqual(expectedValue, encodedGeneration[0, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[0, 1]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 1]);
        }

        [TestMethod]
        public void EncodeOnGenerationOfDeadCellsWithGenerationsSinceAliveFourReturnsGridOfFours()
        {
            var cellGrid = new Cell[2, 2];

            cellGrid[0, 0] = new Cell(new DeadLifeState(4));
            cellGrid[0, 1] = new Cell(new DeadLifeState(4));
            cellGrid[1, 0] = new Cell(new DeadLifeState(4));
            cellGrid[1, 1] = new Cell(new DeadLifeState(4));

            var generation = new Generation(cellGrid);

            var encodedGeneration = generationConverter.Encode(generation);

            var expectedValue = 4;
            Assert.AreEqual(expectedValue, encodedGeneration[0, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[0, 1]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 0]);
            Assert.AreEqual(expectedValue, encodedGeneration[1, 1]);
        }

        [TestMethod]
        public void EncodeOnGenerationOfMixedCellsReturnsGridWithCorrectGenerationsSinceAlive()
        {
            var cellGrid = new Cell[2, 2];

            cellGrid[0, 0] = new Cell(new NeverAliveLifeState());
            cellGrid[0, 1] = new Cell(new AliveLifeState());
            cellGrid[1, 0] = new Cell(new DeadLifeState());
            cellGrid[1, 1] = new Cell(new DeadLifeState(4));

            var generation = new Generation(cellGrid);

            var encodedGeneration = generationConverter.Encode(generation);

            Assert.AreEqual(NeverAliveCode, encodedGeneration[0, 0]);
            Assert.AreEqual(AliveCode, encodedGeneration[0, 1]);
            Assert.AreEqual(1, encodedGeneration[1, 0]);
            Assert.AreEqual(4, encodedGeneration[1, 1]);
        }
    }
}