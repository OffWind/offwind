using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Offwind.Projects;

namespace Offwind.UI.CaseExplorer
{
    /// <summary>
    /// Interaction logic for CaseExplorer.xaml
    /// </summary>
    public partial class CaseExplorerTree : UserControl
    {
        public event EventHandler<CaseExplorerEventArgs> NodeClicked;
        private VCase _case;
        private readonly ObservableCollection<CETI> _items = new ObservableCollection<CETI>();

        public CaseExplorerTree()
        {
            InitializeComponent();

            treeListControl1.ItemsSource = _items;
        }

        public void SetCaseModel(VCase vCase)
        {
            _case = vCase;
        }

        public void Reset()
        {
            _case = null;
            _items.Clear();
        }

        public void Initialize()
        {
            if (_case == null) return;

            foreach (var caseItem in _case.Items)
            {
                AddCaseItem(caseItem);
            }
        }

        public void AddCaseItem(VCaseItem caseItem)
        {
            var vProject = caseItem as VProject;
            if (vProject == null) return;

            var projectNode = new CETI(caseItem.DisplayName, caseItem, null, Guid.NewGuid(), Guid.Empty, null);
            _items.Add(projectNode);

            if (vProject.ProjectDescriptor.ProductType != ProductType.CFD)
            {
                foreach (var item in vProject.Items)
                {
                    var d = item.Descriptor;
                    _items.Add(new CETI(d.NodeName, caseItem, item, Guid.NewGuid(), projectNode.Id, null));
                }
            }
            else
            {
                //var parentNodesMap = new Dictionary<CaseItemType, CETI>();
                //parentNodesMap[CaseItemType.Project] = projectNode;
                //if (vProject.Items.Any(i => i.Descriptor.CaseItemType == CaseItemType.Preprocessor))
                //{
                //    parentNodesMap[CaseItemType.Preprocessor] = new CETI("preprocessing", caseItem, null, Guid.NewGuid(),
                //                                                         projectNode.Id, null);
                //    _items.Add(parentNodesMap[CaseItemType.Preprocessor]);
                //}
                //if (vProject.Items.Any(i => i.Descriptor.CaseItemType == CaseItemType.SolverTimeZero))
                //{
                //    parentNodesMap[CaseItemType.SolverTimeZero] = new CETI("0.original", caseItem, null, Guid.NewGuid(),
                //                                                           projectNode.Id, null);
                //    _items.Add(parentNodesMap[CaseItemType.SolverTimeZero]);
                //}
                //if (vProject.Items.Any(i => i.Descriptor.CaseItemType == CaseItemType.SolverConstant))
                //{
                //    parentNodesMap[CaseItemType.SolverConstant] = new CETI("constant", caseItem, null, Guid.NewGuid(),
                //                                                           projectNode.Id, null);
                //    _items.Add(parentNodesMap[CaseItemType.SolverConstant]);
                //}
                //if (vProject.Items.Any(i => i.Descriptor.CaseItemType == CaseItemType.SolverSystem))
                //{
                //    parentNodesMap[CaseItemType.SolverSystem] = new CETI("system", caseItem, null, Guid.NewGuid(),
                //                                                         projectNode.Id, null);
                //    _items.Add(parentNodesMap[CaseItemType.SolverSystem]);
                //}

                foreach (var item in vProject.Items)
                {
                    var d = item.Descriptor;
                    if (d.NodeInvisible) continue;
                    //if (!parentNodesMap.ContainsKey(d.CaseItemType))
                    //{
                    //    Debug.Fail("Unexpected value: " + d.CaseItemType);
                    //    continue;
                    //}
                    //var targetNode = parentNodesMap[d.CaseItemType];
                    _items.Add(new CETI(d.NodeName, caseItem, item, Guid.NewGuid(), projectNode.Id, null));
                }
            }
        }

        private void HandleItem()
        {
            if (treeListView1.SelectedRows.Count == 0) return;
            var node = treeListView1.SelectedRows[0];

            var ceti = node as CETI;
            if (ceti == null) return;
            if (ceti.CaseItem == null) return;

            if (NodeClicked != null)
            {
                try
                {
                    NodeClicked(this, new CaseExplorerEventArgs((VProject)ceti.CaseItem, ceti.ProjectItem));
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void treeListControl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleItem();
        }
    }
}
