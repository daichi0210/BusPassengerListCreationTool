using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Immutable;

// Excel����p
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
// Excel�N���p
using System.Diagnostics;
using System.Security.Cryptography;
using System.Globalization;

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

            // ��q���X�g���o�X��̏��Ԃɕ��ёւ���
            UserInfo[] uiBusStopOrder = new UserInfo[ui.Length];
            string[] busStopOrder = settings.getBusStopName();
            int uibIndex = 0;

            foreach (string busStop in busStopOrder)
            {
                foreach (UserInfo i in ui)
                {
                    if (i.BusStop == busStop)
                    {
                        uiBusStopOrder[uibIndex] = i;

                        uibIndex++;
                    }
                }
            }

            //���^�s���̓��t�ɂ���dayOfWeek
            string fileName = "���������Ԗ���_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";

            // �e���v���[�g�t�@�C�����R�s�[
            //�����㏑���m�F
            //���t�H���_���Ȃ��ꍇ
            //���e���v���[�g���Ȃ��ꍇ
            File.Copy("template\\_busPassengerList.xlsx", "busPassengerList\\" + fileName);

            // ���݂̃f�B���N�g�����擾
            string currentDirectory = Directory.GetCurrentDirectory();

            // ���݂̃f�B���N�g������̑��΃p�X���쐬
            // Excel�t�@�C��
            //string excelFile = Path.Combine(currentDirectory, "bus passenger list\\" + fileName);
            //string excelFile = Path.Combine(currentDirectory, "template\\_busPassengerList.xlsx");
            string excelFile = Path.Combine(currentDirectory, "busPassengerList\\" + fileName);
            //string excelFile = Path.Combine(currentDirectory, "template\\" + fileName);

            // Excel�̎��s�t�@�C���̃p�X���w��
            string excelPath = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
            
            // Excel�t�@�C���ɏo��
            using (var wb = new XLWorkbook(excelFile))
            //using (var wb = new XLWorkbook())
            {
                // ���[�N�V�[�g"�e���v���[�g"���w��
                //var ws = wb.AddWorksheet("��Ԗ���");
                var ws = wb.Worksheet("��Ԗ���");

                // ���[�N�V�[�g����ύX
                //���L�q����
                
                // �N��a��ŕ\�����邽�߂̏���
                CultureInfo japanese = new CultureInfo("ja-JP");
                japanese.DateTimeFormat.Calendar = new JapaneseCalendar();

                // �^�s���̃Z��
                //��F2�ȂǃA���t�@�x�b�g�ƍs�ԍ��ɒ���
                var operationDaysCell = ws.Cell(2, 6);

                // ���[�N�V�[�g�����^�s���ɕύX
                //ws.Cell(1, 1).Value = "�^�s��";
                operationDaysCell.Value = dateTimePickerOperationDays.Value.ToString("gg y�N M�� d��", japanese);

                // ���p�ҏ�����������
                //var row = ws.Row(1);
                //var column = ws.Column(1);

                // �J�n�s���w��
                int startRow = 5;
                // �J�n����w��
                int startColumn = 2;

                // �J�n�Z�����w��
                //var cell = ws.Cell(startColumn, startRow);

                // �w�b�_�[
                /*
                ws.Cell(startRow, startColumn).Value = "���O";
                ws.Cell(startRow, startColumn + 1).Value = "�Z��";
                ws.Cell(startRow, startColumn + 2).Value = "�d�b�ԍ�";
                ws.Cell(startRow, startColumn + 3).Value = "�o�X��";
                ws.Cell(startRow, startColumn + 4).Value = "���l";
                startRow++;
                */

                // �e�Z���֒l����
                foreach (UserInfo uis in uiBusStopOrder)
                {
                    ws.Cell(startRow, startColumn).Value = uis.Name;
                    ws.Cell(startRow, startColumn + 1).Value = uis.Address;
                    ws.Cell(startRow, startColumn + 2).Value = uis.Tel;
                    ws.Cell(startRow, startColumn + 3).Value = uis.BusStop;
                    ws.Cell(startRow, startColumn + 4).Value = uis.Remarks;

                    startRow++;
                }
                // ��Ȃ̕��̃Z������������
                for (int i = 0; i < settings.getMaximumPeople() - uiBusStopOrder.Length; i++)
                {
                    ws.Cell(startRow, startColumn).Value = "";
                    ws.Cell(startRow, startColumn + 1).Value = "";
                    ws.Cell(startRow, startColumn + 2).Value = "";
                    ws.Cell(startRow, startColumn + 3).Value = "";
                    ws.Cell(startRow, startColumn + 4).Value = "";

                    startRow++;
                }

                // �ۑ�
                //���㏑���ۑ�
                //wb.SaveAs("template\\" + fileName);
                wb.SaveAs(excelFile);
            }

            // Excel���J��
            //���J������v�����BExcel�t�@�C���ŏ����o�������ł�������
            Process.Start(excelPath, excelFile);


            // ���X�g���쐬����
            MessageBox.Show("�쐬�����I");

        }
    }
}
