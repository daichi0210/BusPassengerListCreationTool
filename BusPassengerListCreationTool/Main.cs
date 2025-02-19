using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace BusPassengerListCreationTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // データを読み込む
            databaseLoad();

            // 設定情報を読み込み
            Settings settings = new Settings();

            // 本日の日付を取得
            DateTime dateValue = DateTime.Now;
            while (settings.getOperationDays() != dateValue.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture))
            {
                dateValue = dateValue.AddDays(1);
            }

            // 次回の運行日を代入
            int operationYear = Int32.Parse(dateValue.ToString("yyyy", System.Globalization.CultureInfo.InvariantCulture));
            int operationMonth = Int32.Parse(dateValue.ToString("MM", System.Globalization.CultureInfo.InvariantCulture));
            int operationDate = Int32.Parse(dateValue.ToString("dd", System.Globalization.CultureInfo.InvariantCulture));
            dateTimePickerOperationDays.Value = new DateTime(operationYear, operationMonth, operationDate);
        }

        private void buttonUserList_Click(object sender, EventArgs e)
        {
            UserList userList = new UserList();
            userList.ShowDialog();

            // データを読み込む
            databaseLoad();
        }

        public void databaseLoad()
        {
            checkedListBoxUserSelection.Items.Clear();

            UserListDatabase users = new UserListDatabase();
            /*

                        string[] names = users.loadName();

                        foreach (string name in names)
                        {
                            checkedListBoxUserSelection.Items.Add(name);
                        }
            */

            // IdとNameを辞書で管理
            var userInfo = new Dictionary<int, string>();
            userInfo = users.getUserInfo();

            foreach (string name in userInfo.Values)
            {
                //MessageBox.Show(id.ToString());
                checkedListBoxUserSelection.Items.Add(name);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            // 設定情報を読み込み
            Settings settings = new Settings();

            // 最大乗車人数の確認
            // 選択されている項目の個数を取得
            int checkedCount = 0;
            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                checkedCount++;
            }
            // 選択されている項目の数が最大乗車人数を超えている場合
            if (checkedCount > settings.getMaximumPeople())
            {
                MessageBox.Show("最大乗車人数【" + settings.getMaximumPeople() + "人】を超えています。");
                return;
            }
            // 誰も選択されていない場合
            else if (checkedCount == 0)
            {
                MessageBox.Show("乗車する人を選択してください。");
                return;
            }

            // 運行日の曜日を確認する
            string dayOfWeek = dateTimePickerOperationDays.Value.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture);
            string dayOfWeekJp = dateTimePickerOperationDays.Value.ToString("ddd");

            if (dayOfWeek != settings.getOperationDays())
            {
                DialogResult result = MessageBox.Show("運行日は" + dayOfWeekJp + "曜日です。このまま乗車名簿を作成しますか？", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }


            // IdとNameを辞書で管理
            UserListDatabase users = new UserListDatabase();
            var userInfo = new Dictionary<int, string>();
            userInfo = users.getUserInfo();


            //★同姓同名が存在しない前提
            // 乗車する人の情報をリストに追加する
            int[] ids = new int[0];
            int count = 0;

            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                foreach (var data in userInfo)
                {
                    if (item == data.Value)
                    {
                        Array.Resize(ref ids, count + 1);
                        ids[count] = data.Key;
                        count++;

                        MessageBox.Show(data.Key +":" + data.Value);
                    }
                }

            }

            // 乗車する人の情報を取得する
            


            /*
                        UserListDatabase users = new UserListDatabase();
                        string[] userData = users.getDB();

                        foreach (string ud in userData)
                        {
                            // 乗車する人に選択されている場合
                            //★チェックされているIdを渡す
                            if ()
                            {
                                MessageBox.Show(ud);
                            }
                        }
            */




            // リストを作成する
            MessageBox.Show("作成完了！");

        }
    }
}
