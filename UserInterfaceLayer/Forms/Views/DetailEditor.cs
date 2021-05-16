using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UserInterfaceLayer.Forms.HelpersToControls;
using UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper;
using UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper.TreeViewStoresState;
using Word = Microsoft.Office.Interop.Word;

namespace UserInterfaceLayer.Forms.IViews
{
    public partial class DetailEditor : Form, IDetailRelationRepositoryView, IPrintView
    {
        public DetailEditor() => InitializeComponent();

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
        public event SimpleEventHandler GetAllDetails;
        public event GenericEventHandler<DetailRelationEntity> AddDetail;
        public event GenericEventHandler<DetailRelationEntity> EditDetail;
        public event GenericEventHandler<DetailRelationEntity> DeleteDetail;
        #endregion

        #region Implementation of IPrintView
        public event ParamReturnDelegate<byte[], long> GetMSWord;
        #endregion

        private void DetailEditor_Load(object sender, EventArgs e) => UpdateTree();
        private void UpdateTree()
        {
            if(null != GetAllDetails)
                try
                {
                    GetAllDetails();
                }
                catch(Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
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
                DetailRelationEntity detail = new DetailRelationEntity();

                detail.Name = textBoxName.Text;

                if(isRoot)
                    detail.IsRoot = true;
                else
                {
                    if( ! ThereIsSelectedNode())
                        return;

                    if( ! SetAmount(detail))
                        return;

                    detail.ParentId = GetSelectedDetail().TypeId;
                }

                OperationExecute(AddDetail, detail);
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
                if( ! ThereIsSelectedNode())
                    return;

                DetailRelationEntity editableDetail = new DetailRelationEntity();
                DetailRelationEntity selectedDetail = GetSelectedDetail();

                PropertyInfo[] properties = typeof(DetailRelationEntity).GetProperties();
                foreach(PropertyInfo property in properties)
                    property.SetValue(editableDetail, property.GetValue(selectedDetail));

                bool nameIsEmpty = string.IsNullOrWhiteSpace(textBoxName.Text);

                if(editableDetail.IsRoot)
                {
                    if(nameIsEmpty)
                    {
                        ShowWarningMessage("Наименование не указано.");
                        return;
                    }
                }
                else
                    if( ! SetAmount(editableDetail))
                        return;

                if( ! nameIsEmpty)
                    editableDetail.Name = textBoxName.Text;

                OperationExecute(EditDetail, editableDetail);

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
                if( ! ThereIsSelectedNode())
                    return;

                OperationExecute(DeleteDetail, GetSelectedDetail());
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            if( ! ThereIsSelectedNode())
                return;

            if(null != GetMSWord)
                try
                {
                    string fileName = "C:\\client.doc";

                    byte[] file = GetMSWord((long)treeViewDetails.SelectedNode.Tag);
                    File.WriteAllBytes(fileName, file);

                    _Application word = new Word.Application();
                    _Document doc = word.Documents.Add(fileName);
                    word.Visible = true;
                }
                catch(Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
        }

        private void OperationExecute<T>(GenericEventHandler<T> operation, T operand)
        {
            try
            {
                if(null != operation)
                {
                    operation(operand);
                    UpdateTree();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private bool SetAmount(DetailRelationEntity detail)
        {
            if(short.TryParse(maskedTextBoxAmount.Text, out short value))
            {
                detail.Amount = value;
                return true;
            }

            if(string.IsNullOrWhiteSpace(maskedTextBoxAmount.Text))
                ShowErrorMessage("Количество не указано.");
            else
                ShowErrorMessage("Количество указано не верно.");

            return false;
        }

        public bool ThereIsSelectedNode()
        {
            if(null == treeViewDetails.SelectedNode)
            {
                ShowWarningMessage("Деталь не выбрана.");
                return false;
            }
            return true;
        }

        public object GetTagSelectedNode() => treeViewDetails.SelectedNode.Tag;
        public long GetIdSelectedNode()
        {
            object tag = GetTagSelectedNode();
            if(tag is long)
                return (long)tag;
            throw new Exception("Ошибка в работе программы. Обратитесь к разработчикам.");
        }
        public DetailRelationEntity GetSelectedDetail()
        {
            long id = GetIdSelectedNode();
            return AllDetails.Where(x => id.Equals(x.Id)).Single();
        }

        public void ShowWarningMessage(string message)
        {
            if( ! string.IsNullOrEmpty(message))
                MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void ShowErrorMessage(string message)
        {
            if( ! string.IsNullOrEmpty(message))
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void maskedTextBoxNumber_MouseClick(object sender, MouseEventArgs e) =>
            (new MaskedTextBoxHelper()).MoveCaretBeforeSpaces(maskedTextBoxAmount);
    }
}
