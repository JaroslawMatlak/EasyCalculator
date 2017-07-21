using System;
using EasyCalculator.Models.Operations;
using NUnit.Framework;

namespace Testy
{
    [TestFixture]
    public class OperationsFactoryTests
    {
        [TestCase("SUM", typeof(OperationSum))]
        [TestCase("+", typeof(OperationSum))]
        [TestCase("-", typeof(OperationDifference))]
        public void TestFabrykiPrawidlowyArgument(string argument, Type expected)
        {
            var actual = OperationsFactory.ChooseOperation(argument);
            Assert.AreEqual(expected, actual.GetType());
        }
        [Test]
        public void TestFabrykiNieprawidlowyArgument()
        {
            var argument = "sUmakkkkancsaa";
            //IOperation expected = ;
            var actual = OperationsFactory.ChooseOperation(argument);
            Assert.IsNull(actual);
        }
    }

}
