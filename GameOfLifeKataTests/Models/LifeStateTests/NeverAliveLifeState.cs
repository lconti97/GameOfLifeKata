using GameOfLifeDomain.Models.LifeStates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeKataTests.Models.LifeStateTests
{
    [TestClass]
    public class NeverAliveLifeStateTests
    {
        [TestMethod]
        public void NewNeverAliveLifeStateHasGenerationsSinceAliveNegativeOne()
        {
            var neverAliveLifeState = new NeverAliveLifeState();

            Assert.AreEqual(-1, neverAliveLifeState.GenerationsSinceAlive);
        }
    }
}