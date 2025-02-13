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

            // �ő��Ԑl���̊m�F
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
            // �N���I������Ă��Ȃ��ꍇ
            else if (checkedCount == 0)
            {
                MessageBox.Show("��Ԃ���l��I�����Ă��������B");
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



            // �{���̓��t���擾
            DateTime dateValue = DateTime.Now;
            while (settings.getOperationDays() != dateValue.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture))
            {
                dateValue = dateValue.AddDays(1);
            }

            // ����̉^�s������
            MessageBox.Show("���̉^�q���F" + dateValue.ToString("yyyy�NMM��dd���iddd�j", System.Globalization.CultureInfo.InvariantCulture));




            // ���X�g���쐬����

        }
    }
}
