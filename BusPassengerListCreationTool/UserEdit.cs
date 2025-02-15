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
    public partial class UserEdit : Form
    {
        public int editTargetId { get; set; }

        public UserEdit()
        {
            InitializeComponent();
        }

        private void UserEdit_Load(object sender, EventArgs e)
        {
            MessageBox.Show(editTargetId.ToString());

            //★データを編集
            //UserListDatabase users = new UserListDatabase();
            //users.getSingleData(targetId);
            //users.editDB(targetId);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //★編集の処理を追加

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
