using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Immutable;

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
            // ��Ԃ���l�̏����擾����
            UserInfo[] ui = Array.Empty<UserInfo>();
            int count = 0;

            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                foreach (var data in userInfo)
                {
                    if (item == data.Value)
                    {
                        // �z����g��
                        Array.Resize(ref ui, count + 1);
                        
                        // �z���������
                        ui[count] = new UserInfo();

                        //
                        DataTable userData = users.getUserData(data.Key);

                        // �g�p�ҏ�����
                        ui[count].Name = userData.Rows[0]["Name"].ToString();
                        ui[count].Address = userData.Rows[0]["Address"].ToString();
                        ui[count].Tel = userData.Rows[0]["TEL"].ToString();
                        ui[count].BusStop = userData.Rows[0]["BusStop"].ToString();
                        ui[count].Remarks = userData.Rows[0]["Remarks"].ToString();

                        count++;

                        break;
                    }
                    //MessageBox.Show(data.Value);
                }
            }

            foreach (UserInfo i in ui)
            {
                MessageBox.Show(i.Name + "," + i.Address + "," + i.Tel + "," + i.BusStop + "," + i.Remarks );
            }

            // ��q���X�g���o�X��̏��Ԃɕ��ёւ���




            // ���X�g���쐬����
            MessageBox.Show("�쐬�����I");

        }
    }
}
