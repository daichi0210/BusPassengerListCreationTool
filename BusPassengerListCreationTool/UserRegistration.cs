using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BusPassengerListCreationTool
{
    public partial class UserRegistration : Form
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void UserRegistration_Load(object sender, EventArgs e)
        {
            // バス停の候補を代入
            Settings settings = new Settings();
            comboBoxBusStop.Items.AddRange(settings.getBusStopName());
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            //★入力チェック

            //★データベースに登録
            string name = textBoxName.Text;
            string address = textBoxAddress.Text;
            string TEL = textBoxTEL.Text;
            string busStop = comboBoxBusStop.Text;
            string remarks = textBoxRemarks.Text;

            UserListDatabase users = new UserListDatabase();
            users.addDB(name, address, TEL, busStop, remarks);

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
