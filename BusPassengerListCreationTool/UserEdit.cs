﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusPassengerListCreationTool
{
    public partial class UserEdit : Form
    {
        public int editTargetId { get; set; }

        public UserEdit()
        {
            InitializeComponent();
        }

        private void UserEdit_Load(object sender, EventArgs e)
        {
            // バス停の候補を代入
            Settings settings = new Settings();
            comboBoxBusStop.Items.AddRange(settings.getBusStopName());

            // 使用者情報を取得
            UserListDatabase users = new UserListDatabase();
            //MessageBox.Show(editTargetId.ToString());
            DataTable userData = users.getUserData(editTargetId);


            // 使用者情報を代入
            textBoxName.Text = userData.Rows[0]["Name"].ToString();
            textBoxAddress.Text = userData.Rows[0]["Address"].ToString();
            textBoxTEL.Text = userData.Rows[0]["TEL"].ToString();
            comboBoxBusStop.Text = userData.Rows[0]["BusStop"].ToString();
            textBoxRemarks.Text = userData.Rows[0]["Remarks"].ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // 上書き保存の確認メッセージ
            DialogResult result = MessageBox.Show("使用者情報を上書き保存しますか？", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //★データを上書き保存
                UserListDatabase users = new UserListDatabase();
                users.editDB(editTargetId, textBoxName.Text, textBoxAddress.Text, textBoxTEL.Text, comboBoxBusStop.Text, textBoxRemarks.Text);

                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
