using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserInterfaceLayer.Clients.Authorization;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.HelpersToControls;
using UserInterfaceLayer.HelpersToControls.TreeViewHelper;

namespace UserInterfaceLayer.Forms.Views
{
    internal partial class DetailsEditor : FormHelper
    {
        private readonly IDetailRelationRepositoryClient _detailRelationEntityClient;
        private readonly IPrintClient _printClient;
        private readonly IAuthorizationClient _authorizationClient;

        public DetailsEditor(IDetailRelationRepositoryClient detailRelationEntityClient, IPrintClient printClient, IAuthorizationClient authorizationClient)
        {
            _detailRelationEntityClient = detailRelationEntityClient;
            _printClient = printClient;
            _authorizationClient = authorizationClient;

            InitializeComponent();

#if WCF
            buttonAuthorization.Visible = false;
#endif
        }

        private IQueryable<DetailRelationEntity> _allDetails;
        public IQueryable<DetailRelationEntity> AllDetails
        {
            get { return _allDetails; }
            set
            {
                _allDetails = value.OrderBy(x => x.Name);

                BuildTreeView(treeViewDetails, _allDetails);
            }
        }

        internal DetailsEditor() => InitializeComponent();
        private void DetailEditor_Load(object sender, EventArgs e) => UpdateTree();

#if WCF
        private void UpdateTree()
#elif REST
        private async void UpdateTree()
#endif
        {
            try
            {
#if REST
                await Task.Run(() => _authorizationClient.GetAuthorization());
#endif

                AllDetails = _detailRelationEntityClient.GetAll();
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
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

        private void ButtonAddRoot_Click(object sender, EventArgs e) => CreateAndAddDetail(isRoot: true);
        private void ButtonAddChild_Click(object sender, EventArgs e) => CreateAndAddDetail();
        private async void CreateAndAddDetail(bool isRoot = false)
        {
            Button button = isRoot ? buttonAddRoot : buttonCreateReport;
            try
            {
                DetailRelationEntity detailRelationEntity = GetSelectedDetail();
                ButtonSuspend(button);
                await Task.Run(() => new MessageBoxHelper().ShowWarningMessage(_detailRelationEntityClient.Add(detailRelationEntity, isRoot, textBoxName.Text, maskedTextBoxAmount.Text)));
                UpdateTree();
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
            finally
            {
                ButtonResume(button);
            }
        }

        private async void ButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonSuspend(buttonEdit);
                DetailRelationEntity detailRelationEntity = GetSelectedDetail();
                await Task.Run(() => new MessageBoxHelper().ShowWarningMessage(_detailRelationEntityClient.Edit(detailRelationEntity, textBoxName.Text, maskedTextBoxAmount.Text)));
                UpdateTree();
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
            finally
            {
                ButtonResume(buttonEdit);
            }
        }

        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonSuspend(buttonDelete);
                DetailRelationEntity detailRelationEntity = GetSelectedDetail();
                await Task.Run(() => new MessageBoxHelper().ShowWarningMessage(_detailRelationEntityClient.Delete(detailRelationEntity)));
                UpdateTree();
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
            finally
            {
                ButtonResume(buttonDelete);
            }
        }

        private async void ButtonCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonSuspend(buttonCreateReport);
                DetailRelationEntity detailRelationEntity = GetSelectedDetail();
                await Task.Run(() =>
                {
                    string filePath = "C:\\client.doc";
                    string warningMessage = null;

                    byte[] fileBytes = _printClient.GetReportOnDetailInMSWord(detailRelationEntity, out warningMessage);

                    if ( ! new MessageBoxHelper().ShowWarningMessage(warningMessage))
                    {
                        File.WriteAllBytes(filePath, fileBytes);

                        new MSDocHelper().ShowMSWord(filePath);
                    }
                });
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
            finally
            {
                ButtonResume(buttonCreateReport);
            }
        }

        private DetailRelationEntity GetSelectedDetail()
        {
            if (null == treeViewDetails.SelectedNode)
                return null;
            long id = (long)treeViewDetails.SelectedNode.Tag;
            return AllDetails.Where(x => id.Equals(x.Id)).Single();
        }

        private void MaskedTextBoxNumber_MouseClick(object sender, MouseEventArgs e) =>
            new MaskedTextBoxHelper().MoveCaretBeforeSpaces(maskedTextBoxAmount);

        private async void ButtonAuthorization_Click(object sender, EventArgs e)
        {
            try
            {
                ButtonSuspend(buttonAuthorization);
                await Task.Run(() => _authorizationClient.GetAuthorization());
            }
            catch (Exception ex)
            {
                new MessageBoxHelper().ShowErrorMessage(ex.Message);
            }
            finally
            {
                ButtonResume(buttonAuthorization);
            }
        }
    }
}
