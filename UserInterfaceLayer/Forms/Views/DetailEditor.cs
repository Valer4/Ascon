using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UserInterfaceLayer.Forms.HelpersToControls;
using UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper;
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
        public event ParamReturnDelegate<string, DetailRelationEntity, bool, string, string> AddDetail;
        public event ParamReturnDelegate<string, DetailRelationEntity, string, string> EditDetail;
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

            new TreeViewBuilder().BuildTreeView<T, long, long?, long>(treeView, collection);

            treeState.RestoreState(treeView);
        }

        private void buttonAddRoot_Click(object sender, EventArgs e) => CreateAndAddDetail(isRoot: true);
        private void buttonAddChild_Click(object sender, EventArgs e) => CreateAndAddDetail();
        private void CreateAndAddDetail(bool isRoot = false)
        {
            try
            {
                CheckLink(AddDetail);
                new MessageBoxHelper().ShowWarningMessage(
                    AddDetail(GetSelectedDetail(), isRoot, textBoxName.Text, maskedTextBoxAmount.Text));
            }
            catch(Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                CheckLink(EditDetail);
                new MessageBoxHelper().ShowWarningMessage(
                    EditDetail(GetSelectedDetail(), textBoxName.Text, maskedTextBoxAmount.Text));
            }
            catch(Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CheckLink(DeleteDetail);
                new MessageBoxHelper().ShowWarningMessage(
                    DeleteDetail(GetSelectedDetail()));
            }
            catch(Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
        }

        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "C:\\client.doc";

                byte[] file = GetReportOnDetailInMSWord(GetSelectedDetail(), out string warningMessage);

                if( ! new MessageBoxHelper().ShowWarningMessage(warningMessage))
                {
                    File.WriteAllBytes(fileName, file);

                    _Application word = new Word.Application();
                    _Document doc = word.Documents.Add(fileName);
                    word.Visible = true;
                }
            }
            catch(Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
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

                new MessageBoxHelper().ShowErrorMessage(errorMessage);
            }
        }

        private void maskedTextBoxNumber_MouseClick(object sender, MouseEventArgs e) =>
            new MaskedTextBoxHelper().MoveCaretBeforeSpaces(maskedTextBoxAmount);
    }
}
