using System;
using ILNumerics;

namespace WakeFarmControlR
{
    class EnvMatFileDataStructure
    {
        public double rho;

        public static implicit operator EnvMatFileDataStructure (ILMatFile ILMatFile)
        {
            return new EnvMatFileDataStructure(ILMatFile);
        }

        public EnvMatFileDataStructure(ILMatFile ILMatFile)
        {
            this.rho = (double)(ILMatFile.GetArray<double>("env_rho"));
        }
    }
}
