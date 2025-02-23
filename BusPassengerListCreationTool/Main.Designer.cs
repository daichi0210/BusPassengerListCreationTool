namespace BusPassengerListCreationTool
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonCreate = new Button();
            buttonUserList = new Button();
            dateTimePickerOperationDays = new DateTimePicker();
            label1 = new Label();
            checkedListBoxUserSelection = new CheckedListBox();
            SuspendLayout();
            // 
            // buttonCreate
            // 
            buttonCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreate.Location = new Point(291, 291);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(86, 23);
            buttonCreate.TabIndex = 0;
            buttonCreate.Text = "作成";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // buttonUserList
            // 
            buttonUserList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonUserList.Location = new Point(291, 41);
            buttonUserList.Name = "buttonUserList";
            buttonUserList.Size = new Size(86, 23);
            buttonUserList.TabIndex = 3;
            buttonUserList.Text = "使用者一覧";
            buttonUserList.UseVisualStyleBackColor = true;
            buttonUserList.Click += buttonUserList_Click;
            // 
            // dateTimePickerOperationDays
            // 
            dateTimePickerOperationDays.Location = new Point(85, 12);
            dateTimePickerOperationDays.Name = "dateTimePickerOperationDays";
            dateTimePickerOperationDays.Size = new Size(200, 23);
            dateTimePickerOperationDays.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 5;
            label1.Text = "次回運行日";
            // 
            // checkedListBoxUserSelection
            // 
            checkedListBoxUserSelection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkedListBoxUserSelection.CheckOnClick = true;
            checkedListBoxUserSelection.FormattingEnabled = true;
            checkedListBoxUserSelection.Location = new Point(12, 41);
            checkedListBoxUserSelection.Name = "checkedListBoxUserSelection";
            checkedListBoxUserSelection.Size = new Size(273, 274);
            checkedListBoxUserSelection.TabIndex = 7;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 326);
            Controls.Add(checkedListBoxUserSelection);
            Controls.Add(label1);
            Controls.Add(dateTimePickerOperationDays);
            Controls.Add(buttonUserList);
            Controls.Add(buttonCreate);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Main";
            Text = "あごころバス乗車名簿　作成ツール";
            Load += Main_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCreate;
        private Button buttonUserList;
        private DateTimePicker dateTimePickerOperationDays;
        private Label label1;
        private CheckedListBox checkedListBoxUserSelection;
    }
}
