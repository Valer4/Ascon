
namespace UserInterfaceLayer.Forms.Views
{
    partial class DetailsEditor
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewDetails = new System.Windows.Forms.TreeView();
            this.buttonAddRoot = new System.Windows.Forms.Button();
            this.buttonAddChild = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.buttonCreateReport = new System.Windows.Forms.Button();
            this.maskedTextBoxAmount = new System.Windows.Forms.MaskedTextBox();
            this.buttonAuthorization = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewDetails
            // 
            this.treeViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDetails.HideSelection = false;
            this.treeViewDetails.Location = new System.Drawing.Point(12, 12);
            this.treeViewDetails.Name = "treeViewDetails";
            this.treeViewDetails.Size = new System.Drawing.Size(400, 275);
            this.treeViewDetails.TabIndex = 0;
            // 
            // buttonAddRoot
            // 
            this.buttonAddRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRoot.Location = new System.Drawing.Point(418, 119);
            this.buttonAddRoot.Name = "buttonAddRoot";
            this.buttonAddRoot.Size = new System.Drawing.Size(150, 23);
            this.buttonAddRoot.TabIndex = 3;
            this.buttonAddRoot.Text = "Добавить двигатель";
            this.buttonAddRoot.UseVisualStyleBackColor = true;
            this.buttonAddRoot.Click += new System.EventHandler(this.ButtonAddRoot_Click);
            // 
            // buttonAddChild
            // 
            this.buttonAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddChild.Location = new System.Drawing.Point(418, 148);
            this.buttonAddChild.Name = "buttonAddChild";
            this.buttonAddChild.Size = new System.Drawing.Size(150, 23);
            this.buttonAddChild.TabIndex = 4;
            this.buttonAddChild.Text = "Добавить компонент";
            this.buttonAddChild.UseVisualStyleBackColor = true;
            this.buttonAddChild.Click += new System.EventHandler(this.ButtonAddChild_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(418, 177);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(150, 23);
            this.buttonEdit.TabIndex = 5;
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(418, 206);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(150, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(418, 38);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(150, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.Location = new System.Drawing.Point(418, 12);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(150, 23);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Наименование:";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelNumber
            // 
            this.labelNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNumber.Location = new System.Drawing.Point(418, 64);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(150, 23);
            this.labelNumber.TabIndex = 8;
            this.labelNumber.Text = "Количество";
            this.labelNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // buttonCreateReport
            // 
            this.buttonCreateReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateReport.Location = new System.Drawing.Point(418, 235);
            this.buttonCreateReport.Name = "buttonCreateReport";
            this.buttonCreateReport.Size = new System.Drawing.Size(150, 23);
            this.buttonCreateReport.TabIndex = 7;
            this.buttonCreateReport.Text = "Вывести отчёт";
            this.buttonCreateReport.UseVisualStyleBackColor = true;
            this.buttonCreateReport.Click += new System.EventHandler(this.ButtonCreateReport_Click);
            // 
            // maskedTextBoxAmount
            // 
            this.maskedTextBoxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBoxAmount.Location = new System.Drawing.Point(418, 90);
            this.maskedTextBoxAmount.Mask = "0000";
            this.maskedTextBoxAmount.Name = "maskedTextBoxAmount";
            this.maskedTextBoxAmount.ResetOnSpace = false;
            this.maskedTextBoxAmount.Size = new System.Drawing.Size(150, 20);
            this.maskedTextBoxAmount.TabIndex = 2;
            this.maskedTextBoxAmount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskedTextBoxNumber_MouseClick);
            // 
            // buttonAuthorization
            // 
            this.buttonAuthorization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAuthorization.Location = new System.Drawing.Point(418, 264);
            this.buttonAuthorization.Name = "buttonAuthorization";
            this.buttonAuthorization.Size = new System.Drawing.Size(150, 23);
            this.buttonAuthorization.TabIndex = 9;
            this.buttonAuthorization.Text = "Авторизоваться";
            this.buttonAuthorization.UseVisualStyleBackColor = true;
            this.buttonAuthorization.Click += new System.EventHandler(this.ButtonAuthorization_Click);
            // 
            // DetailsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 299);
            this.Controls.Add(this.buttonAuthorization);
            this.Controls.Add(this.maskedTextBoxAmount);
            this.Controls.Add(this.buttonCreateReport);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAddChild);
            this.Controls.Add(this.buttonAddRoot);
            this.Controls.Add(this.treeViewDetails);
            this.Name = "DetailsEditor";
            this.Text = "Редактор деталей";
            this.Load += new System.EventHandler(this.DetailEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDetails;
        private System.Windows.Forms.Button buttonAddRoot;
        private System.Windows.Forms.Button buttonAddChild;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.Button buttonCreateReport;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxAmount;
        private System.Windows.Forms.Button buttonAuthorization;
    }
}

