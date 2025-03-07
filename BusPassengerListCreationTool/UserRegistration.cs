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
        private string _mode; // フォームのモード（新規／編集）を指定する
        
        public string mode
        {
            set
            {
                _mode = value;
            }
        }



        public UserRegistration()
        {
            InitializeComponent();
        }

        private void UserRegistration_Load(object sender, EventArgs e)
        {
            // バス停の候補を代入
            Settings settings = new Settings();
            comboBoxBusStop.Items.AddRange(settings.getBusStopName());

            MessageBox.Show("Load");

            // 新規登録の場合
            if (_mode == "new")
            {
                userRegistration();
            }
            // 編集の場合
            else if (_mode == "edit")
            {
                userEdit();
            }
            else
            {
                MessageBox.Show("呼び出しエラー");
            }

        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            //// 未入力・未選択チェック
            CheckMissingEntries cme = new CheckMissingEntries();
            if (cme.NoInput(textBoxName.Text, "名前"))
            {
                return;
            }
            if (cme.NoInput(textBoxAddress.Text, "住所"))
            {
                return;
            }
            if (cme.NoInput(textBoxTEL.Text, "電話番号"))
            {
                return;
            }
            if (cme.NoChoice(comboBoxBusStop.Text, "バス停"))
            {
                return;
            }

            //// テキストを置換
            ReplaceText rt = new ReplaceText();
            // 全角数字を半角数字に置換
            textBoxTEL.Text = rt.fullToHalfNumbers(textBoxTEL.Text);
            textBoxAddress.Text = rt.fullToHalfNumbers(textBoxAddress.Text);
            // 全角ハイフンを半角ハイフンに置換
            textBoxTEL.Text = rt.fullToHalfHyphen(textBoxTEL.Text);
            textBoxAddress.Text = rt.fullToHalfHyphen(textBoxAddress.Text);



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

        public void userRegistration()
        {
            MessageBox.Show("新規モード");
        }

        public void userEdit()
        {
            MessageBox.Show("編集モード");
        }
    }
}
