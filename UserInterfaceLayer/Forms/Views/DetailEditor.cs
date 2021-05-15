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

                TreeViewState treeState = new TreeViewState();
                treeState.SaveState(treeViewDetails);

                treeViewDetails.Nodes.Clear();

                (new TreeViewBuilder()).BuildTreeView<DetailRelationEntity, long, long?, long>(treeViewDetails, _allDetails);

                treeState.RestoreState(treeViewDetails);
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

        private void buttonAddRoot_Click(object sender, EventArgs e) => OperationExecute(AddDetail, (CreateAddingDetail(isRoot: true)));
        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            DetailRelationEntity сreatedDetail = CreateAddingDetail();
            if(null != сreatedDetail)
                OperationExecute(AddDetail, сreatedDetail);
        }
        private DetailRelationEntity CreateAddingDetail(bool isRoot = false)
        {
            DetailRelationEntity detail = new DetailRelationEntity();

            try
            {
                detail.Name = textBoxName.Text;

                if(isRoot)
                    detail.IsRoot = true;
                else
                {
                    if( ! IsDetailSelected())
                        return null;

                    if( ! SetAmount(detail, "Количество не указано."))
                        return null;

                    detail.ParentId = GetSelectedDetail().TypeId;
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage(ex.Message);
                return null;
            }

            return detail;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if( ! IsDetailSelected())
                return;

            DetailRelationEntity EditableDetail = new DetailRelationEntity();
            DetailRelationEntity SelectedDetail = GetSelectedDetail();

            PropertyInfo[] properties = typeof(DetailRelationEntity).GetProperties();
            foreach(PropertyInfo property in properties)
                property.SetValue(EditableDetail, property.GetValue(SelectedDetail));

            if( ! string.IsNullOrWhiteSpace(textBoxName.Text))
                EditableDetail.Name = textBoxName.Text;

            if( ! EditableDetail.IsRoot)
                if( ! SetAmount(EditableDetail, "Количество не указано."))
                    return;

            OperationExecute(EditDetail, EditableDetail);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if( ! IsDetailSelected())
                return;

            OperationExecute(DeleteDetail, GetSelectedDetail());
        }

        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            if( ! IsDetailSelected())
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

        private void ShowErrorMessage(string message) =>
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private bool IsDetailSelected()
        {
            if(null == treeViewDetails.SelectedNode)
            {
                MessageBox.Show("Ничего не выбрано.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool SetAmount(DetailRelationEntity detail, string message)
        {
            if(short.TryParse(maskedTextBoxNumber.Text, out short value))
            {
                detail.Amount = value;
                return true;
            }
            ShowErrorMessage(message);
            return false;
        }

        private DetailRelationEntity GetSelectedDetail()
        {
            long selectedNodeId = (long)treeViewDetails.SelectedNode.Tag;
            return AllDetails.Where(x => selectedNodeId.Equals(x.Id)).Single();
        }

        private void maskedTextBoxNumber_MouseClick(object sender, MouseEventArgs e) =>
            (new MaskedTextBoxHelper()).MoveCaretBeforeSpaces(maskedTextBoxNumber);
    }
}
