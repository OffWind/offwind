using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Offwind.OpenFoam.Models.DecomposeParDict;
using System.Collections;
using Offwind.Sowfa.Constant.AirfoilProperties;

namespace Offwind.WebApp.Tests
{
    [TestFixture]
    public class SolverTests
    {
        [Test]
        public void DecomposeParDictTest()
        {
            var hdr = new DecomposeParDictHandler();
            var o = hdr.Read(null);
            hdr.Write(null, o);
        }
        [Test]
        public void AirfoilHandlerTest()
        {
            var a = new AirfoilPropertiesHandler(null);
            var t = a.ReadDefault();
        }
    }
}