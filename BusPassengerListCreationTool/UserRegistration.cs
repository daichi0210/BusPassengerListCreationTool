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
        // 編集対象
        private int _editTargetId = -1;

        public int editTargetId
        {
            set
            {
                _editTargetId = value;
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

            // _editTargetId が -1 以外の場合、編集対象のデータを読み込む
            if (_editTargetId != -1)
            {
                userEdit();
            }
            /*
            // 新規登録の場合
            else
            {
                userRegistration();
            }
            */
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

            // 使用者情報を取得
            UserListDatabase users = new UserListDatabase();
            //MessageBox.Show(editTargetId.ToString());
            DataTable userData = users.getUserData(_editTargetId);

            // 使用者情報を代入
            textBoxName.Text = userData.Rows[0]["Name"].ToString();
            textBoxAddress.Text = userData.Rows[0]["Address"].ToString();
            textBoxTEL.Text = userData.Rows[0]["TEL"].ToString();
            comboBoxBusStop.Text = userData.Rows[0]["BusStop"].ToString();
            textBoxRemarks.Text = userData.Rows[0]["Remarks"].ToString();

            // 登録ボタンの Text を変更
            buttonRegistration.Text = "上書き保存";
        }
    }
}
