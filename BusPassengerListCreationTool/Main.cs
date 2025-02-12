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

            // �f�[�^��ǂݍ���
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
            // �ݒ����ǂݍ���
            Settings settings = new Settings();

            // �I������Ă��鍀�ڂ̌����擾
            int checkedCount = 0;
            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                checkedCount++;
            }
            
            // �I������Ă��鍀�ڂ̐����ő��Ԑl���𒴂��Ă���ꍇ
            if (checkedCount > settings.getMaximumPeople())
            {
                MessageBox.Show("�ő��Ԑl���y" + settings.getMaximumPeople() + "�l�z�𒴂��Ă��܂��B");
                return;
            }

            

            //// ��Ԃ���l�̏������X�g�ɒǉ�����
            UserListDatabase users = new UserListDatabase();
            string[] userData = users.getDB();

            foreach (string ud in userData)
            {
                //MessageBox.Show(ud);
            }

            // �I������Ă��鍀�ڂ��擾


            //���`�F�b�N����Ă���Id��n��

            // �ő��Ԑl���̊m�F



            // ����̉^�s������

            // ���X�g���쐬����

        }
    }
}
