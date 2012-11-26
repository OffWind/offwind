using System;
using Offwind.Projects;

namespace Offwind.Infrastructure
{
    public interface IProjectItemController
    {
        Action GetSaveCommand();
        void UpdateFromProject(VProject vProject);
    }
}
