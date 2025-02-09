using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;

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

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();
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
            // 乗車人数の上限を取得する
            

            //★チェックされているIdを渡す

            // 乗車する人の情報をリストに追加する
            UserListDatabase users = new UserListDatabase();
            string[] userData = users.getDB();

            foreach (string ud in userData)
            {
                MessageBox.Show(ud);
            }

            // リストを作成する

        }
    }
}
