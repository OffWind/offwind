using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.NavBar;
using Offwind.Infrastructure;
using Offwind.Projects;
using Offwind.Settings;

namespace Offwind.NewCase
{
    /// <summary>
    /// Interaction logic for NewCaseDialog.xaml
    /// </summary>
    public partial class NewCaseDialog2 : DXWindow
    {
        private readonly VNewCase _model = new VNewCase();
        private readonly FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private readonly NonRoamingUserSettings _settings;

        public VNewCase Model { get { return _model; } }

        public NewCaseDialog2()
        {
            InitializeComponent();
            FillData();
            ScrollingSettings.SetScrollMode(navBarControl1.View, ScrollMode.Buttons);

            _settings = new NonRoamingUserSettings().Read();
            _model.CaseLocation = _settings.ProjectsFolder;
            _model.PropertyChanged += _model_PropertyChanged;
            DataContext = _model;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            navBarControl1.ActiveGroup.SelectedItemIndex = 0;
        }

        private void FillData()
        {
            var descriptors = new List<ProjectDescriptor>();
            foreach (var type in GetTypes<ProjectDescriptor>(typeof(ProjectDescriptor).Assembly))
            {
                Debug.WriteLine("Descriptor: {0}", type);
                var descriptor = (ProjectDescriptor)Activator.CreateInstance(type);
                if (descriptor.SkipStandalone) continue;
                descriptors.Add(descriptor);
            }
            descriptors.Sort((a, b) => a.Order.CompareTo(b.Order));

            var items = new ObservableCollection<ListItem>();
            foreach (var pd in descriptors)
            {
                string groupName = null;
                switch (pd.ProductType)
                {
                    case ProductType.CFD:
                        groupName = "CFD Simulations";
                        break;
                    default:
                        groupName = "Engineering Tools";
                        break;
                }
                items.Add(new ListItem { Group = groupName, Code = pd.Code, Name = pd.Name, Descriptor = pd });
                navBarControl1.ItemsSource = items;
            }
        }

        private static IEnumerable<Type> GetTypes<T>(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsInterface) continue;
                if (type.IsAbstract) continue;
                if (typeof(T).IsAssignableFrom(type))
                    yield return type;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            _folderBrowserDialog.SelectedPath = _model.CaseLocation;
            if (_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _model.CaseLocation = _folderBrowserDialog.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CaseName":
                    _model.CaseDir = Path.Combine(_model.CaseLocation, _model.CaseName);
                    break;
                case "CaseLocation":
                    _model.CaseDir = Path.Combine(_model.CaseLocation, _model.CaseName);
                    break;
            }
        }

        private string GetAvailableName(string folderPath, string desiredFolderName)
        {
            var n = 2;
            var availableName = desiredFolderName;
            var path = Path.Combine(folderPath, availableName);
            while (Directory.Exists(path))
            {
                availableName = String.Format("{0} {1}", desiredFolderName, n++);
                path = Path.Combine(folderPath, availableName);
            }
            return availableName;
        }

        private void SideBarView_ItemSelected(object sender, NavBarItemSelectedEventArgs e)
        {
            var pd = navBarControl1.SelectedItem.Tag as ListItem;
            if (pd == null) return;
            DocumentTextUtils.SetFormattedText(textDescription, pd.Descriptor.Description);
            _model.ProjectDescriptor = pd.Descriptor;
            _model.CaseName = GetAvailableName(_model.CaseLocation, pd.Descriptor.Name);
        }
    }
}
