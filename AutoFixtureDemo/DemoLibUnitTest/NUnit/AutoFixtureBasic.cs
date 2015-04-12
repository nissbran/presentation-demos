using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoLibUnitTest.NUnit
{
    using DemoLib.Model;
    using Ploeh.AutoFixture;

    [TestClass]
    public class AutoFixtureBasic
    {
        [TestMethod]
        public void TestMethod1()
        {
            IFixture fixture = new Fixture();

            var randomInt = fixture.Create<int>();
            var randomString = fixture.Create<string>();
            var customer = fixture.Create<Person>();
        }
    }
}
