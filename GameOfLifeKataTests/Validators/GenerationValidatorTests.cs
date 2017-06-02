using GameOfLifeKata.Models;
using GameOfLifeKata.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameOfLifeKataTests.Validators
{
    [TestClass]
    public class GenerationValidatorTests
    {
        private GenerationValidator validator;
        private Generation generation;

        public GenerationValidatorTests()
        {
            validator = new GenerationValidator();
            generation = new Generation();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateThrownArgumentNullExceptionWhenGenerationIsNull()
        {
            generation = null;
            validator.Validate(generation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateThrownArgumentExceptionWhenCellsIsNull()
        {
            generation.Cells = null;
            validator.Validate(generation);
        }

        [TestMethod]
        public void ValidateDoesNotThrowExceptionsWhenGenerationIsNotNullAndContainsCells()
        {
            validator.Validate(generation);
        }
    }
}
