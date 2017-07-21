using System;
using EasyCalculator.Models.Operations;
using NUnit.Framework;
using EasyCalculator.Models;

namespace Testy
{
    [TestFixture]
    class NumberStringAppenderTests
    {
        [TestCase("1", "a", "1")]
        [TestCase("1", "1", "11")]
        [TestCase("0000", "1", "1")]
        [TestCase("1", ",", "1,")]
        [TestCase("1", ",2", "1,2")]
        public void DodawanieDoPrawidlowegoStringa(string cel, string wartoscDoDodania, string result)
        {
            var actual = NumberStringAppender.StartAppending(cel, wartoscDoDodania);
            Assert.AreEqual(actual, result);
        }

        [TestCase("a", "a", "0")]
        [TestCase("a", "1", "1")]
        [TestCase("a", "1", "1")]
        [TestCase("a", ",", "0,")]
        [TestCase("a", ",2", "0,2")]
        public void DodawanieDoNieprawidlowegoStringa(string cel, string wartoscDoDodania, string result)
        {
            var actual = NumberStringAppender.StartAppending(cel, wartoscDoDodania);
            Assert.AreEqual(actual, result);
        }



    }
}
