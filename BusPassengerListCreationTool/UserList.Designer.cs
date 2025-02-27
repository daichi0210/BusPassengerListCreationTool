namespace BusPassengerListCreationTool
{
    partial class UserList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewUsers = new DataGridView();
            buttonEdit = new Button();
            buttonDelete = new Button();
            buttonUserRegistration = new Button();
            buttonClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.AllowUserToDeleteRows = false;
            dataGridViewUsers.AllowUserToResizeColumns = false;
            dataGridViewUsers.AllowUserToResizeRows = false;
            dataGridViewUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridViewUsers.Location = new Point(12, 12);
            dataGridViewUsers.MultiSelect = false;
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.RowHeadersVisible = false;
            dataGridViewUsers.RowTemplate.Height = 35;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsers.Size = new Size(640, 331);
            dataGridViewUsers.TabIndex = 4;
            // 
            // buttonEdit
            // 
            buttonEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonEdit.Location = new Point(93, 349);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 23);
            buttonEdit.TabIndex = 1;
            buttonEdit.Text = "編集";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonDelete.Location = new Point(174, 349);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 2;
            buttonDelete.Text = "削除";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonUserRegistration
            // 
            buttonUserRegistration.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonUserRegistration.Location = new Point(12, 349);
            buttonUserRegistration.Name = "buttonUserRegistration";
            buttonUserRegistration.Size = new Size(75, 23);
            buttonUserRegistration.TabIndex = 0;
            buttonUserRegistration.Text = "新規登録";
            buttonUserRegistration.UseVisualStyleBackColor = true;
            buttonUserRegistration.Click += buttonUserRegistration_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClose.Location = new Point(577, 349);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(75, 23);
            buttonClose.TabIndex = 3;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // UserList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 381);
            Controls.Add(buttonClose);
            Controls.Add(buttonUserRegistration);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(dataGridViewUsers);
            MinimumSize = new Size(680, 420);
            Name = "UserList";
            StartPosition = FormStartPosition.CenterParent;
            Text = "使用者一覧";
            Load += UserList_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewUsers;
        private Button buttonEdit;
        private Button buttonDelete;
        private Button buttonUserRegistration;
        private Button buttonClose;
    }
}