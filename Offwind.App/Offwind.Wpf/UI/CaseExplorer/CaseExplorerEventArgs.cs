using System;
using Offwind.Projects;

namespace Offwind.UI.CaseExplorer
{
    public sealed class CaseExplorerEventArgs : EventArgs
    {
        public VProject Project { get; private set; }

        public VProjectItem ProjectItem { get; private set; }

        public CaseExplorerEventArgs(VProject project, VProjectItem pItem)
        {
            Project = project;
            ProjectItem = pItem;
        }
    }
}
