using System.Data.SQLite;
using System.Data;

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

        private void button2_Click(object sender, EventArgs e)
        {
            UserRegistration userRegistration = new UserRegistration();
            userRegistration.ShowDialog();

            databaseLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();
        }

        public void databaseLoad()
        {
            // データを取得
            UserListDatabase users = new UserListDatabase();
            DataTable dataTable = users.loadDB();

            // DataGridViewにデータをバインドする
            dataGridViewUsers.DataSource = dataTable;
        }
    }
}
