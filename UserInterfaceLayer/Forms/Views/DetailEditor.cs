using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Forms.HelpersToControls;
using UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper;
using UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper.TreeViewStoresState;
using UserInterfaceLayer.Forms.IViews;
using Word = Microsoft.Office.Interop.Word;

namespace UserInterfaceLayer.Forms.Views
{
    public partial class DetailEditor : Form, IDetailRelationRepositoryView, IPrintView
    {
        private IQueryable<DetailRelationEntity> _allDetails;
        #region Implementation of IDetailRelationRepositoryView
        public IQueryable<DetailRelationEntity> AllDetails
        {
            get { return _allDetails; }
            set
            {
                _allDetails = value.OrderBy(x => x.Name);

                BuildTreeView(treeViewDetails, _allDetails);
            }
        }
        public event SimpleEventHandler LoadData;
        public event AddEventHandler<string, DetailRelationEntity> AddDetail;
        public event EditEventHandler<string, DetailRelationEntity> EditDetail;
        public event ParamReturnDelegate<string, DetailRelationEntity> DeleteDetail;
        #endregion

        #region Implementation of IPrintView
        public event GetReportEventHandler<DetailRelationEntity> GetReportOnDetailInMSWord;
        #endregion

        public DetailEditor() => InitializeComponent();
        private void DetailEditor_Load(object sender, EventArgs e)
        {
            CheckLink(LoadData);
            LoadData();
        }

        private void BuildTreeView<T>(TreeView treeView, IQueryable<T> collection)
            where T : DetailRelationEntity
        {
            TreeViewState treeState = new TreeViewState();
            treeState.SaveState(treeView);

            treeView.Nodes.Clear();

            (new TreeViewBuilder()).BuildTreeView<T, long, long?, long>(treeView, collection);

            treeState.RestoreState(treeView);
        }

        private void buttonAddRoot_Click(object sender, EventArgs e) => CreateAndAddDetail(isRoot: true);
        private void buttonAddChild_Click(object sender, EventArgs e) => CreateAndAddDetail();
        private void CreateAndAddDetail(bool isRoot = false)
        {
            try
            {
                CheckLink(AddDetail);
                ShowWarningMessage(
                    AddDetail(GetSelectedDetail(), isRoot, textBoxName.Text, maskedTextBoxAmount.Text));
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                CheckLink(EditDetail);
                ShowWarningMessage(
                    EditDetail(GetSelectedDetail(), textBoxName.Text, maskedTextBoxAmount.Text));
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CheckLink(DeleteDetail);
                ShowWarningMessage(
                    DeleteDetail(GetSelectedDetail()));
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "C:\\client.doc";

                byte[] file = GetReportOnDetailInMSWord(GetSelectedDetail(), out string warningMessage);

                if( ! ShowWarningMessage(warningMessage))
                {
                    File.WriteAllBytes(fileName, file);

                    _Application word = new Word.Application();
                    _Document doc = word.Documents.Add(fileName);
                    word.Visible = true;
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public DetailRelationEntity GetSelectedDetail()
        {
            if(null == treeViewDetails.SelectedNode)
                return null;
            long id = (long)treeViewDetails.SelectedNode.Tag;
            return AllDetails.Where(x => id.Equals(x.Id)).Single();
        }

        private void CheckLink(object obj, string errorMessage = null)
        {
            if(null == obj)
            {
                if(null == errorMessage)
                    errorMessage = "Ошибка в работе программы. Обратитесь к разработчикам.";

                ShowErrorMessage(errorMessage);
            }
        }

        public bool ShowWarningMessage(string message) => ShowProblemMessage(message, "Предупреждение", MessageBoxIcon.Warning);
        public bool ShowErrorMessage(string message) => ShowProblemMessage(message, "Ошибка", MessageBoxIcon.Error);
        public bool ShowProblemMessage(string message, string caption, MessageBoxIcon icon)
        {
            if( ! string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
                return true;
            }
            return false;
        }

        private void maskedTextBoxNumber_MouseClick(object sender, MouseEventArgs e) =>
            (new MaskedTextBoxHelper()).MoveCaretBeforeSpaces(maskedTextBoxAmount);
    }
}
