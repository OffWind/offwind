using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Offwind.Sowfa.Constant.TurbineArrayPropertiesFAST
{
    public sealed class TurbineArrayPropFASTData
    {
        public TurbineArrayPropFASTGeneral general { set; get; }
        public List<TurbineInstanceFAST>   turbine { set; get; }
    
        public TurbineArrayPropFASTData()
        {
            general = new TurbineArrayPropFASTGeneral();
            turbine = new List<TurbineInstanceFAST>();
        }
    }
}
