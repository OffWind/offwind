using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmitMapper;
using NUnit.Framework;
using Offwind.Sowfa.System.FvSchemes;
using Offwind.WebApp.Areas.CFD.Models.SystemControls;
using System.Collections;


namespace Offwind.WebApp.Tests
{
    [TestFixture]
    public sealed class EmitMapper
    {
        [Test]
         public void MapList()
        {
            var x = new FvSchemesData(true);
            var y = new VSchemes();

            x.ddtSchemes.Add(new TimeScheme()
                                 {
                                     function = "default1",
                                     isDefault = false,
                                     psi = 128,
                                     scheme = "abcd",
                                     type = TimeSchemeType.CrankNicholson
                                 });
            x.ddtSchemes.Add(new TimeScheme()
            {
                function = "default2",
                isDefault = false,
                psi = 256,
                scheme = "efgh",
                type = TimeSchemeType.Euler
            });
            /*
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TimeScheme, VTimeScheme>();
            //y.ddtSchemes = x.ddtSchemes.Select(mapper.Map);
            foreach (var a in x.ddtSchemes)
            {
                y.ddtSchemes.Add(mapper.Map(a));
            }
             */ 
            ListMapper<TimeScheme, VTimeScheme>(x.ddtSchemes, y.ddtSchemes);
            x.divSchemes.Clear();
        }

        public void ListMapper<TFrom, TTo> (List<TFrom> from, List<TTo> to)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>();
            foreach (var x in from)
            {
                to.Add(mapper.Map(x));
            }
        }
    }
}