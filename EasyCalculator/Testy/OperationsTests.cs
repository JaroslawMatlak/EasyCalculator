using System;
using EasyCalculator.Models.Operations;
using NUnit.Framework;

namespace Testy
{
    [TestFixture]
    class OperationsTests
    {
        [TestCase(1, 1, 2)]
        [TestCase(1000 , 0, 1000)]
        [TestCase(90000000000000000000.12332131231231, 100000000, 90000000000100000000.12332131231231)]
        public void TestDodawania(double num1, double num2, double expected)
        {
            var dodawanie = new OperationSum();
            var actual = dodawanie.DoTheOperation(num1, num2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, 0)]
        [TestCase(1000, 0, 1000)]
        [TestCase(90000000000000000000.12332131231231, 100000000, 89999999999900000000.12332131231231)]
        public void TestOdejmowania(double num1, double num2, double expected)
        {
            var odejmowanie = new OperationDifference();
            var actual = odejmowanie.DoTheOperation(num1, num2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1,1,1.0)]
        [TestCase(10,0,0.0)]
        [TestCase(0,19,0.0)]
        [TestCase(-10,-10,100.0)]
        [TestCase(0.5, 4, 2.0)]
        public void TestMnozenia(double num1, double num2, double expected)
        {
            var mnozenie = new OperationProduct();
            var actual = mnozenie.DoTheOperation(num1, num2);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1,1,1.0)]
        [TestCase(10, 2, 5.0)]
        [TestCase(10, 3, 3.3333333333333335d)]
        [TestCase(10, 100, .1)]
        public void TestDzieleniaNiezerowyDzielnik(double num1, double num2, double expected)
        {
            var dzielenie = new OperationDivide();
            var actual = dzielenie.DoTheOperation(num1, num2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DzieleniePrzezZero()
        {
            var dzielenie = new OperationDivide();
            Assert.Throws<DivideByZeroException>(() => dzielenie.DoTheOperation(1, 0));
        }


    }
}
