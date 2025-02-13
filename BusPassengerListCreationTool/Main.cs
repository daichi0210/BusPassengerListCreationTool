using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            databaseLoad();
        }

        private void buttonUserRegistration_Click(object sender, EventArgs e)
        {
            UserRegistration userRegistration = new UserRegistration();
            userRegistration.ShowDialog();

            databaseLoad();
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
            string[] names = users.loadName();

            foreach (string name in names)
            {
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

            //// 乗車する人の情報をリストに追加する
            UserListDatabase users = new UserListDatabase();
            string[] userData = users.getDB();

            foreach (string ud in userData)
            {
                //MessageBox.Show(ud);
            }

            // 選択されている項目を取得
            //★チェックされているIdを渡す



            // 本日の日付を取得
            DateTime dateValue = DateTime.Now;
            while (settings.getOperationDays() != dateValue.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture))
            {
                dateValue = dateValue.AddDays(1);
            }

            // 次回の運行日を代入
            MessageBox.Show("次の運航日：" + dateValue.ToString("yyyy年MM月dd日（ddd）", System.Globalization.CultureInfo.InvariantCulture));




            // リストを作成する

        }
    }
}
