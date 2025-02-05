namespace BusPassengerListCreationTool
{
    partial class UserRegistration
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBoxName = new TextBox();
            textBoxAddress = new TextBox();
            textBoxTEL = new TextBox();
            textBoxRemarks = new TextBox();
            comboBoxBusStop = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 0;
            label1.Text = "名前";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 1;
            label2.Text = "住所";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 2;
            label3.Text = "電話番号";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 102);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 3;
            label4.Text = "バス停";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 131);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 4;
            label5.Text = "備考";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(90, 12);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(100, 23);
            textBoxName.TabIndex = 5;
            // 
            // textBoxAddress
            // 
            textBoxAddress.Location = new Point(90, 41);
            textBoxAddress.Name = "textBoxAddress";
            textBoxAddress.Size = new Size(100, 23);
            textBoxAddress.TabIndex = 6;
            // 
            // textBoxTEL
            // 
            textBoxTEL.Location = new Point(90, 70);
            textBoxTEL.Name = "textBoxTEL";
            textBoxTEL.Size = new Size(100, 23);
            textBoxTEL.TabIndex = 7;
            // 
            // textBoxRemarks
            // 
            textBoxRemarks.Location = new Point(90, 128);
            textBoxRemarks.Name = "textBoxRemarks";
            textBoxRemarks.Size = new Size(100, 23);
            textBoxRemarks.TabIndex = 8;
            // 
            // comboBoxBusStop
            // 
            comboBoxBusStop.FormattingEnabled = true;
            comboBoxBusStop.Items.AddRange(new object[] { "", "末", "更毛", "桜ヶ丘", "本堂", "ヒロミストアー", "羽坂団地", "北堀", "恐神" });
            comboBoxBusStop.Location = new Point(90, 99);
            comboBoxBusStop.Name = "comboBoxBusStop";
            comboBoxBusStop.Size = new Size(100, 23);
            comboBoxBusStop.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(248, 11);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "登録";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(248, 40);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 11;
            button2.Text = "キャンセル";
            button2.UseVisualStyleBackColor = true;
            // 
            // UserRegistration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 161);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBoxBusStop);
            Controls.Add(textBoxRemarks);
            Controls.Add(textBoxTEL);
            Controls.Add(textBoxAddress);
            Controls.Add(textBoxName);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UserRegistration";
            Text = "使用者登録";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxName;
        private TextBox textBoxAddress;
        private TextBox textBoxTEL;
        private TextBox textBoxRemarks;
        private ComboBox comboBoxBusStop;
        private Button button1;
        private Button button2;
    }
}