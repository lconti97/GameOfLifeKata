using GameOfLifeDomain.Models;
using GameOfLifeDomain.Models.LifeStates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeKataTests.Models
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CellCreatedAliveIsAliveReturnsTrue()
        {
            var cell = new Cell(new AliveLifeState());

            Assert.IsTrue(cell.IsAlive());
        }

        [TestMethod]
        public void CellCreatedNotAliveIsAliveReturnsFalse()
        {
            var cell = new Cell(new NeverAliveLifeState());

            Assert.IsFalse(cell.IsAlive());
        }


    }
}
