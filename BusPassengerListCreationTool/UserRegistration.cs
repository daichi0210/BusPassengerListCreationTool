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
            if (cme.NoInput(textBoxLastName.Text, "姓"))
            {
                return;
            }
            if (cme.NoInput(textBoxFirstName.Text, "名"))
            {
                return;
            }
            if (cme.NoInput(textBoxLastNameKana.Text, "セイ"))
            {
                return;
            }
            if (cme.NoInput(textBoxFirstNameKana.Text, "メイ"))
            {
                return;
            }
            if (cme.NoInput(textBoxAddress.Text, "住所"))
            {
                return;
            }
            //★固定電話または携帯電話番号は必須
            if (cme.NoInput(textBoxTel.Text, "固定電話番号"))
            {
                return;
            }
            if (cme.NoInput(textBoxMobileNumber.Text, "携帯電話番号"))
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
            textBoxTel.Text = rt.fullToHalfNumbers(textBoxTel.Text);
            textBoxAddress.Text = rt.fullToHalfNumbers(textBoxAddress.Text);
            // 全角ハイフンを半角ハイフンに置換
            textBoxTel.Text = rt.fullToHalfHyphen(textBoxTel.Text);
            textBoxAddress.Text = rt.fullToHalfHyphen(textBoxAddress.Text);


            // 編集モードの場合
            if (_editTargetId != -1)
            {
                // 上書き保存の確認メッセージ
                DialogResult result = MessageBox.Show("使用者情報を上書き保存しますか？", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //★データを上書き保存
                    UserListDatabase users = new UserListDatabase();
                    users.editDB(_editTargetId, textBoxLastName.Text, textBoxAddress.Text, textBoxTel.Text, comboBoxBusStop.Text, textBoxRemarks.Text);
                }
                else
                {
                    return;
                }
            }
            else
            {
                //★データベースに登録
                string name = textBoxLastName.Text;
                string address = textBoxAddress.Text;
                string TEL = textBoxTel.Text;
                string busStop = comboBoxBusStop.Text;
                string remarks = textBoxRemarks.Text;

                UserListDatabase users = new UserListDatabase();
                users.addDB(name, address, TEL, busStop, remarks);

            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
        public void userRegistration()
        {
            MessageBox.Show("新規モード");
        }
        */

        public void userEdit()
        {
            MessageBox.Show("編集モード");

            // 使用者情報を取得
            UserListDatabase users = new UserListDatabase();
            //MessageBox.Show(editTargetId.ToString());
            DataTable userData = users.getUserData(_editTargetId);

            // 使用者情報を代入
            textBoxLastName.Text = userData.Rows[0]["Name"].ToString();
            textBoxAddress.Text = userData.Rows[0]["Address"].ToString();
            textBoxTel.Text = userData.Rows[0]["TEL"].ToString();
            comboBoxBusStop.Text = userData.Rows[0]["BusStop"].ToString();
            textBoxRemarks.Text = userData.Rows[0]["Remarks"].ToString();

            // 登録ボタンの Text を変更
            buttonRegistration.Text = "上書き保存";
        }
    }
}
