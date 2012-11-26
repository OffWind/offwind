using NUnit.Framework;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Tests
{
    [TestFixture]
    public class OtherTests
    {
        [Test]
        [TestCase("tmpi2zNka_0.vtk", "tmpi2zNka_..vtk")]
        [TestCase("tmpi2zNka_0343413413.vtk", "tmpi2zNka_..vtk")]
        [TestCase("tm13443sdgf345pSDri2zNka_4540.vtk", "tm13443sdgf345pSDri2zNka_..vtk")]
        public void GetVtkSeries(string input, string eres)
        {
            var res = Utils.GetVtkSeries(input);
            Assert.AreEqual(eres, res);
        }
    }
}
