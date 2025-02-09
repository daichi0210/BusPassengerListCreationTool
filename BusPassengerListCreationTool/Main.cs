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

            // �f�[�^��ǂݍ���
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
            // ��Ԑl���̏�����擾����
            

            //���`�F�b�N����Ă���Id��n��

            // ��Ԃ���l�̏������X�g�ɒǉ�����
            UserListDatabase users = new UserListDatabase();
            string[] userData = users.getDB();

            foreach (string ud in userData)
            {
                MessageBox.Show(ud);
            }

            // ���X�g���쐬����

        }
    }
}
