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
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.AllowUserToDeleteRows = false;
            dataGridViewUsers.AllowUserToResizeColumns = false;
            dataGridViewUsers.AllowUserToResizeRows = false;
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridViewUsers.Location = new Point(12, 12);
            dataGridViewUsers.MultiSelect = false;
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.RowHeadersVisible = false;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsers.Size = new Size(776, 265);
            dataGridViewUsers.TabIndex = 0;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(12, 299);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 23);
            buttonEdit.TabIndex = 1;
            buttonEdit.Text = "編集";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(93, 299);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "削除";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 325);
            label1.Name = "label1";
            label1.Size = new Size(281, 15);
            label1.TabIndex = 5;
            label1.Text = "※編集・削除したい行を選択してからボタンを押してください";
            // 
            // UserList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(dataGridViewUsers);
            Name = "UserList";
            Text = "使用者一覧";
            Load += UserList_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewUsers;
        private Button buttonEdit;
        private Button buttonDelete;
        private Label label1;
    }
}