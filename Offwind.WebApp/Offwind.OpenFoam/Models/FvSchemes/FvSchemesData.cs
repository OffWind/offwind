using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Offwind.Sowfa.System.FvSchemes
{
    public sealed class FvSchemesData
    {
        public List<TimeScheme> ddtSchemes { set; get; }
        public List<GradientScheme> gradSchemes { set; get; }
        public List<InterpolationScheme> interpolationSchemes { set; get; }
        public List<LaplacianScheme> laplacianSchemes { set; get; }
        public List<DivergenceScheme> divSchemes { set; get; }
        public List<SurfaceNormalGradientScheme> snGradSchemes { set; get; }
        public List<FluxCalculation> fluxCalculation { set; get; }
        
        public FvSchemesData( bool init_lists = false )
        {
            if (init_lists)
            {
                ddtSchemes = new List<TimeScheme>();
                gradSchemes = new List<GradientScheme>();
                interpolationSchemes = new List<InterpolationScheme>();
                laplacianSchemes = new List<LaplacianScheme>();
                divSchemes = new List<DivergenceScheme>();
                snGradSchemes = new List<SurfaceNormalGradientScheme>();
                fluxCalculation = new List<FluxCalculation>();
            }
        }
        
    }
}
