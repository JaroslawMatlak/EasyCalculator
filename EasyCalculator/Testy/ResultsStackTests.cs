using System;
using EasyCalculator.Models.Operations;
using EasyCalculator.Models.ResultsStack;
using NUnit.Framework;

namespace Testy
{
    [TestFixture]
    class ResultsStackTests
    {
        [TestCase(10,5,3,7)]
        [TestCase(10, 0, 0, 9)]
        [TestCase(10,12,5,5)]
        [TestCase(10, 1, 5, 9)]
        [TestCase(0, 1, 5, -1)]
        [TestCase(1000, 10, 10001, 999)]
        public void DodajWynikiANastepnieWykonajUndoIRedo(int ileWynikow, int ileUndo, int ileRedo, int expected)
        {
            var rs = new ResultsStack();
            for (int i = 0; i < ileWynikow; ++i)
            {
                rs.AppendNewResult(0, "+");
            }
            for (int i = 0; i < ileUndo; ++i)
                rs.Undo();
            for (int i = 0; i < ileRedo; ++i)
                rs.Redo();
            var actual = rs.GetActiveResultId();
            Assert.AreEqual(expected, actual);
        }

    }
}
