using System;
using System.Collections.Generic;
using System.Linq;
using EmitMapper;

namespace Offwind.WebApp.Infrastructure
{
    public sealed class EmitMapperExtensions
    {
        public static void MapList<TFrom, TTo>(List<TFrom> from, List<TTo> to)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>();
            to.AddRange(@from.Select(x => mapper.Map(x)));
        }
    }
}
