using System;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Infrastructure
{
    public interface IProjectItemView
    {
        void SetFileHandler(FoamFileHandler handler);
        Action GetSaveCommand();
        void UpdateFromProject(VProject vProject);
    }
}
