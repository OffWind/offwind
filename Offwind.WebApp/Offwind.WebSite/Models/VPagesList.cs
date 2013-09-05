using System.Collections.Generic;

namespace Offwind.Web.Models
{
    public class VPagesList
    {
        public string Title { get; set; }
        public string PageType { get; set; }
        public List<VPage> Pages { get; set; }

        public VPagesList()
        {
            Pages = new List<VPage>();
        }
    }
}