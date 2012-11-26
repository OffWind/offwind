using System;
using Offwind.Projects;

namespace Offwind.UI.CaseExplorer
{
    /// <summary>
    /// Abbreviation of "Case Explorer Tree Item"
    /// </summary>
    public sealed class CETI
    {
        public string Image { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public VCaseItem CaseItem { get; set; }
        public VProjectItem ProjectItem { get; set; }

        public CETI(string name, VCaseItem caseItem, VProjectItem projectItem, Guid id, Guid parentId, string image)
        {
            Image = image;
            Id = id;
            ParentId = parentId;
            Name = name;
            CaseItem = caseItem;
            ProjectItem = projectItem;
        }
    }
}
