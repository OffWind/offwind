using System.Linq;
using Offwind.Web.Core;

namespace Offwind.WebApp.Models
{
    public class BlockModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public BlockModel()
        {
            Name = "";
            Title = "";
            Content = "";
        }

        public static BlockModel GetBlock(string name,ContentType type)
        {
            using (var ctx = new OffwindEntities())
            {
                var ctBlock = type.ToString();
                var content = ctx.DContents.FirstOrDefault(c => c.TypeId == ctBlock && c.Name == name);
                var block = new BlockModel();
                if (content != null)
                {
                    block.Name = content.Name;
                    block.Title = content.Title;
                    block.Content = content.Content;
                    block.Image = content.Announce;
                }
                return block;
            }
        }
    }
}