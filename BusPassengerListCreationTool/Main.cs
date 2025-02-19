using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

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
            // �f�[�^��ǂݍ���
            databaseLoad();

            // �ݒ����ǂݍ���
            Settings settings = new Settings();

            // �{���̓��t���擾
            DateTime dateValue = DateTime.Now;
            while (settings.getOperationDays() != dateValue.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture))
            {
                dateValue = dateValue.AddDays(1);
            }

            // ����̉^�s������
            int operationYear = Int32.Parse(dateValue.ToString("yyyy", System.Globalization.CultureInfo.InvariantCulture));
            int operationMonth = Int32.Parse(dateValue.ToString("MM", System.Globalization.CultureInfo.InvariantCulture));
            int operationDate = Int32.Parse(dateValue.ToString("dd", System.Globalization.CultureInfo.InvariantCulture));
            dateTimePickerOperationDays.Value = new DateTime(operationYear, operationMonth, operationDate);
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
            /*

                        string[] names = users.loadName();

                        foreach (string name in names)
                        {
                            checkedListBoxUserSelection.Items.Add(name);
                        }
            */

            // Id��Name�������ŊǗ�
            var userInfo = new Dictionary<int, string>();
            userInfo = users.getUserInfo();

            foreach (string name in userInfo.Values)
            {
                //MessageBox.Show(id.ToString());
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

            // �^�s���̗j�����m�F����
            string dayOfWeek = dateTimePickerOperationDays.Value.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture);
            string dayOfWeekJp = dateTimePickerOperationDays.Value.ToString("ddd");

            if (dayOfWeek != settings.getOperationDays())
            {
                DialogResult result = MessageBox.Show("�^�s����" + dayOfWeekJp + "�j���ł��B���̂܂܏�Ԗ�����쐬���܂����H", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }


            // Id��Name�������ŊǗ�
            UserListDatabase users = new UserListDatabase();
            var userInfo = new Dictionary<int, string>();
            userInfo = users.getUserInfo();


            //���������������݂��Ȃ��O��
            // ��Ԃ���l�̏������X�g�ɒǉ�����
            int[] ids = new int[0];
            int count = 0;

            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                foreach (var data in userInfo)
                {
                    if (item == data.Value)
                    {
                        Array.Resize(ref ids, count + 1);
                        ids[count] = data.Key;
                        count++;

                        MessageBox.Show(data.Key +":" + data.Value);
                    }
                }

            }

            // ��Ԃ���l�̏����擾����
            


            /*
                        UserListDatabase users = new UserListDatabase();
                        string[] userData = users.getDB();

                        foreach (string ud in userData)
                        {
                            // ��Ԃ���l�ɑI������Ă���ꍇ
                            //���`�F�b�N����Ă���Id��n��
                            if ()
                            {
                                MessageBox.Show(ud);
                            }
                        }
            */




            // ���X�g���쐬����
            MessageBox.Show("�쐬�����I");

        }
    }
}
