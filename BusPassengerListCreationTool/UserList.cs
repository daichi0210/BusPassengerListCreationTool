using System;
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
    public partial class UserList : Form
    {
        public UserList()
        {
            InitializeComponent();
        }

        private void UserList_Load(object sender, EventArgs e)
        {
            // データを取得
            UserListDatabase users = new UserListDatabase();
            DataTable dataTable = users.loadDB();

            // DataGridViewにデータをバインドする
            dataGridViewUsers.DataSource = dataTable;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int targetId = -1;

            // 選択されている行のIDの値を取得
            foreach (DataGridViewRow r in dataGridViewUsers.SelectedRows)
            {
                targetId = r.Index;
            }
            //int targetId = Int32.Parse(textBoxTargetId.Text);

            //★初期状態で一行目が選択されているので不都合？
            if (targetId == -1)
            {
                // 削除の確認メッセージ
                MessageBox.Show("削除するデータを選択してください。");
            }
            //★すべてのデータを削除することができない…。
            else
            {
                // 削除の確認メッセージ
                MessageBox.Show("選択されたデータを削除します。");

                // データを削除
                UserListDatabase users = new UserListDatabase();
                users.deleteDB(targetId);

                // データを取得
                DataTable dataTable = users.loadDB();

                // DataGridViewにデータをバインドする
                dataGridViewUsers.DataSource = dataTable;
            }
        }
    }
}
