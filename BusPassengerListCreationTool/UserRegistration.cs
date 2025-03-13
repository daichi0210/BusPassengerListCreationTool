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
            Settings s = new Settings();
            comboBoxBusStop.Items.AddRange(s.getBusStopName());

            // _editTargetId が -1 以外の場合、編集対象のデータを読み込む
            if (_editTargetId != -1)
            {
                userEdit();
            }
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            //★★★要修正★★★
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
            textBoxAddress.Text = rt.fullToHalfNumbers(textBoxAddress.Text);
            textBoxTel.Text = rt.fullToHalfNumbers(textBoxTel.Text);
            textBoxMobileNumber.Text = rt.fullToHalfNumbers(textBoxMobileNumber.Text);
            // 全角ハイフンを半角ハイフンに置換
            textBoxAddress.Text = rt.fullToHalfHyphen(textBoxAddress.Text);
            textBoxTel.Text = rt.fullToHalfHyphen(textBoxTel.Text);
            textBoxMobileNumber.Text = rt.fullToHalfHyphen(textBoxMobileNumber.Text);

            // 使用者情報を代入
            User u = new User();
            u.LastName = textBoxLastName.Text;              // 姓
            u.FirstName = textBoxFirstName.Text;            // 名
            u.LastNameKana = textBoxLastNameKana.Text;      // 姓（カナ）
            u.FirstNameKana = textBoxFirstNameKana.Text;    // 名（カナ）
            u.Address = textBoxAddress.Text;                // 住所
            u.Tel = textBoxTel.Text;                        // 電話番号（固定電話）
            u.MobileNumber = textBoxMobileNumber.Text;      // 電話番号（携帯電話）
            u.BusStop = comboBoxBusStop.Text;               // 乗車バス停
            u.Remarks = textBoxRemarks.Text;                // 備考

            //
            UserListDatabase uld = new UserListDatabase();

            //★★★要確認★★★
            // 編集モードの場合
            if (_editTargetId != -1)
            {
                // 上書き保存の確認メッセージ
                DialogResult result = MessageBox.Show("使用者情報を上書き保存しますか？", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //★データを上書き保存
                    //UserListDatabase uld = new UserListDatabase();
                    //uld.editDB(_editTargetId, textBoxLastName.Text, textBoxAddress.Text, textBoxTel.Text, comboBoxBusStop.Text, textBoxRemarks.Text);
                    uld.Update(_editTargetId, u);
                }
                else
                {
                    return;
                }
            }
            else
            {
                /*
                //★データベースに登録
                string name = textBoxLastName.Text;
                string address = textBoxAddress.Text;
                string TEL = textBoxTel.Text;
                string busStop = comboBoxBusStop.Text;
                string remarks = textBoxRemarks.Text;
                */

                //UserListDatabase uld = new UserListDatabase();
                //uld.addDB(name, address, TEL, busStop, remarks);
                uld.Insert(u);
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //★★★要修正★★★
        public void userEdit()
        {
            // 使用者情報を取得
            UserListDatabase uld = new UserListDatabase();
            DataTable dt = uld.getUserData(_editTargetId);

            // 使用者情報を代入
            textBoxLastName.Text = dt.Rows[0]["LastName"].ToString();    // 姓
            textBoxFirstName.Text = dt.Rows[0]["FirstName"].ToString();    // 名
            textBoxLastNameKana.Text = dt.Rows[0]["LastNameKana"].ToString();    // 姓（カナ）
            textBoxFirstNameKana.Text = dt.Rows[0]["FirstNameKana"].ToString();    // 名（カナ）
            textBoxAddress.Text = dt.Rows[0]["Address"].ToString();    // 住所
            textBoxTel.Text = dt.Rows[0]["Tel"].ToString();    // 電話番号（固定電話）
            textBoxMobileNumber.Text = dt.Rows[0]["MobileNumber"].ToString();    // 電話番号（携帯電話）
            comboBoxBusStop.Text = dt.Rows[0]["BusStop"].ToString();    // 乗車バス停
            textBoxRemarks.Text = dt.Rows[0]["Remarks"].ToString();    // 備考

            /*
            // 使用者情報を代入
            textBoxLastName.Text = userData.Rows[0]["Name"].ToString();
            textBoxAddress.Text = userData.Rows[0]["Address"].ToString();
            textBoxTel.Text = userData.Rows[0]["TEL"].ToString();
            comboBoxBusStop.Text = userData.Rows[0]["BusStop"].ToString();
            textBoxRemarks.Text = userData.Rows[0]["Remarks"].ToString();
            */

            // 登録ボタンの Text を変更
            buttonRegistration.Text = "上書き保存";
        }
    }
}
