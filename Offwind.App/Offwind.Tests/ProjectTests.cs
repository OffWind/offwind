using System;
using NUnit.Framework;
using Offwind.Products.Sowfa;
using Offwind.Settings;

namespace Offwind.Tests
{
    [TestFixture]
    public sealed class ProjectTests
    {
        [Test]
        [Ignore]
        public void Generate()
        {
            var cfg = new SowfaProjectConfiguration();
            foreach (var pair in cfg.ProjectItemsMap)
            {
                Console.WriteLine("\tnew ProjectItemDescriptor()");
                Console.WriteLine("\t\t.SetDefaultName(\"{0}\")", pair.Value.DefaultName);
                Console.WriteLine("\t\t.SetForm(typeof({0}))", pair.Value.Form.Name);
                Console.WriteLine("\t\t.SetCommandSave({0})", pair.Value.CommandSave);
                Console.WriteLine("\t\t.SetInitializer({0})", pair.Value.FormInitializer);
                Console.WriteLine("\t\t.SetCode(SowfaProjectItemType.{0}.ToString())", pair.Key);
                Console.WriteLine("\t\t.AddTo(ProjectItemsMap, SowfaProjectItemType.{0});", pair.Key);
                Console.WriteLine();
            }
        }
        
        [Test]
        public void GetXml()
        {
            var cfg = new SowfaProjectConfiguration();
            foreach (var pair in cfg.ProjectItemsMap)
            {
                Console.WriteLine("<item>");
                
                Console.WriteLine("\t<key>{0}</key>", pair.Key);
                Console.WriteLine("\t<code>{0}</code>", pair.Value.Code);
                Console.WriteLine("\t<title>{0}</title>", pair.Value.DefaultName);
                Console.WriteLine("\t<form>{0}</form>", pair.Value.Form);

                Console.WriteLine("</item>");
            }
        }
        
        [Test]
        public void EditorTypesSupported()
        {
            var cfg = new SowfaProjectConfiguration();
            foreach (SowfaProjectItemType et in Enum.GetValues(typeof(SowfaProjectItemType)))
            {
                Assert.Contains(et, cfg.ProjectItemsMap.Keys);
            }
        }

        [Test]
        public void SettingsReadNonExisting()
        {
            var settings = new NonRoamingUserSettings();
            settings.Destroy();
            settings.Read();
        }

        [Test]
        public void SettingsWriteRead()
        {
            var settings = new NonRoamingUserSettings();
            settings.SalomePath = "adsfasd fasdf asdf asdf";
            settings.Destroy();
            settings.Save();

            var settings2 = new NonRoamingUserSettings();
            Assert.IsNull(settings2.SalomePath);
            settings2.Read();
            Assert.AreEqual(settings.SalomePath, settings2.SalomePath);
        }
    }
}
