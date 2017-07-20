using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyCalculator.Models.Operations;

namespace Testy
{
    [TestClass]
    public class TestFabrykiOperacji
    {
        [TestMethod]
        public void TestFabrykiPrawidlowyArgument()
        {
            var argument = "sUm";
            var expected = new OperationSum();
            var actual = OperationsFactory.ChooseOperation(argument);
            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
        [TestMethod]
        public void TestFabrykiNieprawidlowyArgument()
        {
            var argument = "sUmakkkkancsaa";
            IOperation expected = null;
            var actual = OperationsFactory.ChooseOperation(argument);
            Assert.IsNull(actual);
        }
    }
}
