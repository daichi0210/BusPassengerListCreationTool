using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

            // 列ヘッダーの文字列を変更する
            int columnCount = 0;
            while (columnCount < dataGridViewUsers.ColumnCount)
            {
                string cellValue = "";

                switch (dataGridViewUsers.Columns[columnCount].HeaderCell.Value)
                {
                    case "Id":
                        break;
                    case "Name":
                        cellValue = "名前";
                        break;
                    case "Address":
                        cellValue = "住所";
                        break;
                    case "TEL":
                        cellValue = "電話番号";
                        break;
                    case "BusStop":
                        cellValue = "バス停";
                        break;
                    case "Remarks":
                        cellValue = "備考";
                        break;
                    default:
                        cellValue = "";
                        break;
                }

                if (cellValue != "")
                {
                    dataGridViewUsers.Columns[columnCount].HeaderCell.Value = cellValue;
                }

                columnCount++;
            }

            // アクティブなセルの選択を解除する
            dataGridViewUsers.ClearSelection();

            // Idの列を非表示
            dataGridViewUsers.Columns["Id"].Visible = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int targetIndex = -1;

            // 選択されている行を取得
            //foreach (DataGridViewRow r in dataGridViewUsers.SelectedRows)
            //{
            //    targetIndex = r.Index;
            //}
            targetIndex = dataGridViewUsers.CurrentCell.RowIndex;


            //★初期状態で一行目が選択されているので不都合？
            if (targetIndex == -1)
            {
                // 削除の確認メッセージ
                MessageBox.Show("削除するデータを選択してください。");
            }

            //★すべてのデータを削除することができない…。
            else
            {
                // 選択されている行のIDの値を取得
                DataGridViewRow selectedRow = dataGridViewUsers.Rows[targetIndex];
                var idValue = selectedRow.Cells["Id"].Value;
                int targetId = Int32.Parse(idValue.ToString());

                // 削除の確認メッセージ
                DialogResult result = MessageBox.Show("選択されたデータを削除しますか？", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
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
}
