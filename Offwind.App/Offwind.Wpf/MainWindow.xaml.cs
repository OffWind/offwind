using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Layout.Core;
using Offwind.Infrastructure;
using Offwind.NewCase;
using Offwind.Projects;
using Offwind.UI.CaseExplorer;

namespace Offwind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        public class PanelLayoutInfo
        {
            public bool IsFloating { get; set; }
            public Point FloatLocation { get; set; }
            public Size FloatSize { get; set; }
        }

        private bool _startPageActivated = false;
        private readonly Dictionary<string, DocumentPanel> _openDocuments = new Dictionary<string, DocumentPanel>();
        private readonly Dictionary<string, PanelLayoutInfo> _panelInfos = new Dictionary<string, PanelLayoutInfo>();
        private VCase _case;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ThemeManager.ApplicationThemeName = "VS2010";
            //ThemeManager.ApplicationThemeName = "Azure";
            //ThemeManager.ApplicationThemeName = "Seven";
            //ThemeManager.ApplicationThemeName = "Office2010Black";
            //ThemeManager.ApplicationThemeName = "Office2007Silver";
            //ThemeManager.ApplicationThemeName = "Office2007Blue";
            //ThemeManager.ApplicationThemeName = "Office2007Black";
            //ThemeManager.ApplicationThemeName = "LightGray";
            //ThemeManager.ApplicationThemeName = "DeepBlue";
            //ThemeManager.ApplicationThemeName = "DXStyle";

            //ThemeManager.ApplicationThemeName = "DXStyle";

            dockLayoutManager1.DockItemClosed += DockItemClosed;
            dockLayoutManager1.DockItemClosing += DockItemClosing;
            dockLayoutManager1.DockItemStartDocking += DockItemStartDocking;
            dockLayoutManager1.DockItemDocking += DockItemDocking;
            dockLayoutManager1.DockItemEndDocking += DockItemEndDocking;
            dockLayoutManager1.DockItemDragging += DockItemDragging;

            caseExplorerTree.NodeClicked += caseExplorerTree_NodeClicked;
            DemountEnvironment();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (!_startPageActivated)
            {
                _startPageActivated = true;
                StartNewDialog();
            }
        }

        private void StartNewDialog()
        {
            var dlg = new NewCaseDialog2();
            dlg.Owner = this;
            var result = dlg.ShowDialog();
            if (result == null) return;
            if (!result.Value) return;
            try
            {
                DemountEnvironment();

                _case = dlg.Model
                    .ProjectDescriptor
                    .CaseInitializer
                    .Initialize(dlg.Model);

                MountEnvironment();
            }
            catch (Exception ex)
            {
                DemountEnvironment();
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MountEnvironment()
        {
            caseExplorerTree.Reset();
            caseExplorerTree.SetCaseModel(_case);
            caseExplorerTree.Initialize();
            dockLayoutManager1.DockController.Activate(panelLeft);

            bbFileClose.IsEnabled = true;
            bbFileSave.IsEnabled = true;
            bbFileSaveAll.IsEnabled = true;
            bbToolsSettings.IsEnabled = true;
        }

        private void DemountEnvironment()
        {
            caseExplorerTree.Reset();

            foreach (var documentPanel in _openDocuments.Values)
            {
                dockLayoutManager1.DockController.RemoveItem(documentPanel);
            }
            _openDocuments.Clear();
            _panelInfos.Clear();

            bbFileClose.IsEnabled = false;
            bbFileSave.IsEnabled = false;
            bbFileSaveAll.IsEnabled = false;
            bbToolsSettings.IsEnabled = false;

            _case = null;
        }

        private void OpenOrActivateForm(string key, string caption, Control content, string toolTip, bool noScroll)
        {
            if (_openDocuments.ContainsKey(key))
            {
                dockLayoutManager1.DockController.Activate(_openDocuments[key]);
                return;
            }

            content.MaxHeight = 5000;
            content.MaxWidth = 10000;

            var panel = new DocumentPanel();
            panel.AllowClose = true;
            panel.Caption = caption;
            panel.ToolTip = toolTip ?? caption ?? "";

            panel.SizeChanged += (o, e) =>
            {
                Debug.WriteLine("Panel: W: {0}, H: {1}", panel.ActualWidth, panel.ActualHeight);
            };
            _openDocuments[key] = panel;

            if (noScroll)
            {
                panel.Content = content;
            }
            else
            {
                var scrollView = new ScrollViewer();
                scrollView.Content = content;
                content.HorizontalAlignment = HorizontalAlignment.Stretch;
                //content.VerticalAlignment = VerticalAlignment.Stretch;
                scrollView.SizeChanged += (o, e) =>
                {
                    Debug.WriteLine("Scroll: W: {0}, H: {1}", scrollView.ActualWidth, scrollView.ActualHeight);
                };

                panel.Content = scrollView;
            }

            if (_panelInfos.ContainsKey(key))
            {
                var i = _panelInfos[key];
                if (i.IsFloating)
                {
                    var g = new FloatGroup();
                    g.FloatLocation = i.FloatLocation;
                    g.FloatSize = i.FloatSize;
                    g.Add(panel);
                    dockLayoutManager1.FloatGroups.Add(g);
                }
                else
                {
                    dockLayoutManager1.DockController.Dock(panel, baseDocumentGroup, DockType.Fill);
                    //baseDocumentGroup.Add(panel);
                }
            }
            else
            {
                dockLayoutManager1.DockController.Dock(panel, baseDocumentGroup, DockType.Fill);
                //baseDocumentGroup.Add(panel);
            }
            dockLayoutManager1.DockController.Activate(panel);
        }

        void DockItemStartDocking(object sender, ItemCancelEventArgs e)
        {
            Debug.WriteLine("DockItemStartDocking: {0}", e.Item);
        }

        private void DockItemDocking(object sender, DockItemDockingEventArgs e)
        {
            Debug.WriteLine("DockItemDocking: {0}", e.Item);
            Debug.WriteLine("\tDockTarget: {0} / {1}", e.DockTarget, e.DockTarget != null ? e.DockTarget.Name : "");
            Debug.WriteLine("\tDockType: {0}", e.DockType);
            Debug.WriteLine("\tDragPoint: {0}", e.DragPoint);

            var dg = e.DockTarget as DocumentGroup;
            if (dg == null)
            {
                e.Cancel = true;
                return;
            }
        }

        void DockItemEndDocking(object sender, DockItemDockingEventArgs e)
        {
            Debug.WriteLine("DockItemEndDocking: {0}", e.Item);
            var dg = e.DockTarget as DocumentGroup;
            if (dg == null)
            {
                e.Cancel = true;
                return;
            }
        }

        private void DockItemDragging(object sender, DockItemDraggingEventArgs e)
        {
            Debug.WriteLine("DockItemDragging: {0} / {1}", e.Item, e.Item.Name);
        }

        private void DockItemClosing(object sender, ItemCancelEventArgs e)
        {
            Debug.WriteLine("DockItemClosing: {0}", e.Item);

            var info = new PanelLayoutInfo();
            LayoutPanel panel = null;
            if (e.Item is LayoutPanel)
            {
                panel = (LayoutPanel) e.Item;
                info.IsFloating = false;
            }
            else if (e.Item is FloatGroup)
            {
                var g = (FloatGroup) e.Item;
                panel = (LayoutPanel)g.Items[0];
                info.IsFloating = true;
                info.FloatLocation = g.FloatLocation;
                info.FloatSize = g.FloatSize;
            }

            var key = FindKeyByObject(panel);
            if (key != "")
            {
                _panelInfos[key] = info;
                _openDocuments.Remove(key);
            }
        }

        void DockItemClosed(object sender, DockItemClosedEventArgs e)
        {
            Debug.WriteLine("DockItemClosed: {0}", e.Item);
            dockLayoutManager1.ClosedPanels.Clear(); // Don't leave panels in semi-closed state
        }

        string FindKeyByObject(object panel)
        {
            var key = "";
            foreach (var pair in _openDocuments)
            {
                if (pair.Value != panel) continue;
                key = pair.Key;
                break;
            }
            return key;
        }

        void caseExplorerTree_NodeClicked(object sender, CaseExplorerEventArgs e)
        {
            var key = e.ProjectItem.Descriptor.Code;
            var tip = e.Project.DisplayName + " - " + e.ProjectItem.Descriptor.DefaultName;
            if (e.ProjectItem.Descriptor.Form == null)
            {
                OpenOrActivateForm(key, e.ProjectItem.Descriptor.DefaultName, null, tip, e.ProjectItem.Descriptor.NoScroll);
                return;
            }

            //var content = (Control) Activator.CreateInstance(e.ProjectItem.Descriptor.Form);
            var content = e.ProjectItem.Descriptor.CreateContentControl();
            var projectItemView = content as IProjectItemView;
            if (projectItemView != null)
            {
                projectItemView.UpdateFromProject(_case.FindByProjectItem(e.ProjectItem));
            }
            OpenOrActivateForm(key, e.ProjectItem.Descriptor.DefaultName, content, tip, e.ProjectItem.Descriptor.NoScroll);
        }

        private void bbFileNew_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            StartNewDialog();
        }

        private void bbFileOpen_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
        }

        private void bbFileClose_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            DemountEnvironment();
        }

        private void bbFileSave_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            var panel = dockLayoutManager1.DockController.ActiveItem;
            var key = FindKeyByObject(panel);
            if (key == "") return;

            var documentPanel = _openDocuments[key];
            var scrollView = (ScrollViewer)documentPanel.Content;
            var content = (Control)scrollView.Content;

            var editor = content as IProjectItemView;
            if (editor == null) return;
            var save = editor.GetSaveCommand();
            if (save == null) return;
            save();
        }

        private void bbFileSaveAll_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            foreach (var documentPanel in _openDocuments.Values)
            {
                var scrollView = (ScrollViewer)documentPanel.Content;
                var content = (Control)scrollView.Content;

                var editor = content as IProjectItemView;
                if (editor == null) return;
                var save = editor.GetSaveCommand();
                if (save == null) return;
                save();
            }
        }

        private void bbRunRun_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void bbToolsSettings_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }
    }
}
