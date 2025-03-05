using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BusPassengerListCreationTool
{
    public partial class UserRegistration : Form
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void UserRegistration_Load(object sender, EventArgs e)
        {
            // バス停の候補を代入
            Settings settings = new Settings();
            comboBoxBusStop.Items.AddRange(settings.getBusStopName());
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            // 未入力・未選択チェック
            if (textBoxName.Text == String.Empty)
            {
                MessageBox.Show("名前を入力してください。");
                return;
            }
            if (textBoxAddress.Text == String.Empty)
            {
                MessageBox.Show("住所を入力してください。");
                return;
            }
            if (textBoxTEL.Text == String.Empty)
            {
                MessageBox.Show("電話番号を入力してください。");
                return;
            }
            if (comboBoxBusStop.Text == String.Empty)
            {
                MessageBox.Show("バス停を選択してください。");
                return;
            }

            // 電話番号を半角数字に置換
            textBoxTEL.Text = Regex.Replace(textBoxTEL.Text, "[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());
            // 電話番号の全角ハイフンを半角ハイフンに置換
            textBoxTEL.Text = Regex.Replace(textBoxTEL.Text, "－", "-");




            //★データベースに登録
            string name = textBoxName.Text;
            string address = textBoxAddress.Text;
            string TEL = textBoxTEL.Text;
            string busStop = comboBoxBusStop.Text;
            string remarks = textBoxRemarks.Text;

            UserListDatabase users = new UserListDatabase();
            users.addDB(name, address, TEL, busStop, remarks);

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
