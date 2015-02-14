using ILNumerics;
using NUnit.Framework;

namespace MatlabInterpreter.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        [TestCase(0, 0.1, 100, 1000)]
        [TestCase(0, 0.1, 200, 2000)]
        [TestCase(0, 0.1, 299, 2990)]
        [TestCase(0, 0.1, 300, 3000)]
        [TestCase(0, 0.1, 400, 4000)]
        public void TestC(decimal tStart, decimal timeStep, decimal tEnd, int result)
        {
            var r = TestMock.TestC(tStart, timeStep, tEnd - timeStep);
            Assert.AreEqual(result, r.Size[1]);
        }

        [Test]
        [TestCase(0, 0.1, 100, 1000)]
        [TestCase(0, 0.1, 200, 2000)]
        [TestCase(0, 0.1, 299, 2990)]
        [TestCase(0, 0.1, 300, 3000)]
        [TestCase(0, 0.1, 400, 4000)]
        public void TestCSize(decimal tStart, decimal timeStep, decimal tEnd, int result)
        {
            var r = MatlabCode._csize(tStart, timeStep, tEnd - timeStep);
            Assert.AreEqual(result, r);
        }
    }

    public class TestMock : MatlabCode
    {
        public static ILArray<double> TestC(decimal start, decimal incrementation, decimal limit)
        {
            return _c(start, incrementation, limit);
        }
    }
}