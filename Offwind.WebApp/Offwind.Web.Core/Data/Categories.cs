using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Offwind.Web.Core.Data
{
    public class CategoryNames
    {
        public const string Home = "home";
        public const string About = "info-about";
        public const string Help = "help";
    }

    public static class Categories
    {
        public static Guid Home = Guid.Parse("b882c640-5537-4c38-8501-02dfa2086472");
        public static Guid About = Guid.Parse("9ec610bf-4f5b-4e02-a3da-da6c853f9b64");
    }

    public class PageTypes
    {
        public const string News = "News";
        public const string Help = "Help";
    }
    public class ContentTypes
    {
        public const string Block = "Block";
        public const string Carousel = "Carousel";
        public const string Blog = "Blog";
        public const string Page = "Page";
    }
}
