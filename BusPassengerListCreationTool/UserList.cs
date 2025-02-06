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
    }
}
