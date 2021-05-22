﻿using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Forms.HelpersToControls;
using UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper;
using Word = Microsoft.Office.Interop.Word;

namespace UserInterfaceLayer.Forms.Views
{
    internal partial class DetailsEditor : Form
    {
        private readonly IDetailRelationRepositoryClient _detailRelationEntityClient;
        private readonly IPrintClient _printClient;

        public DetailsEditor(IDetailRelationRepositoryClient detailRelationEntityClient, IPrintClient printClient)
        {
            _detailRelationEntityClient = detailRelationEntityClient;
            _printClient = printClient;

            InitializeComponent();
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

        private void UpdateTree()
        {
            try
            {
                AllDetails = _detailRelationEntityClient.GetAll();
            }
            catch(Exception ex)
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

        private void buttonAddRoot_Click(object sender, EventArgs e) => CreateAndAddDetail(isRoot: true);
        private void buttonAddChild_Click(object sender, EventArgs e) => CreateAndAddDetail();
        private void CreateAndAddDetail(bool isRoot = false)
        {
            try
            {
                new MessageBoxHelper().ShowWarningMessage(
                    _detailRelationEntityClient.Add(GetSelectedDetail(), isRoot, textBoxName.Text, maskedTextBoxAmount.Text));
                UpdateTree();
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
                new MessageBoxHelper().ShowWarningMessage(
                    _detailRelationEntityClient.Edit(GetSelectedDetail(), textBoxName.Text, maskedTextBoxAmount.Text));
                UpdateTree();
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
                new MessageBoxHelper().ShowWarningMessage(
                    _detailRelationEntityClient.Delete(GetSelectedDetail()));
                UpdateTree();
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

                byte[] file = _printClient.GetReportOnDetailInMSWord(GetSelectedDetail(), out string warningMessage);

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

        private DetailRelationEntity GetSelectedDetail()
        {
            if(null == treeViewDetails.SelectedNode)
                return null;
            long id = (long)treeViewDetails.SelectedNode.Tag;
            return AllDetails.Where(x => id.Equals(x.Id)).Single();
        }

        private void maskedTextBoxNumber_MouseClick(object sender, MouseEventArgs e) =>
            new MaskedTextBoxHelper().MoveCaretBeforeSpaces(maskedTextBoxAmount);
    }
}