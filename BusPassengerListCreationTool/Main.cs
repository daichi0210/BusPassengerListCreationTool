using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Immutable;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

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
            // データを読み込む
            databaseLoad();

            // 設定情報を読み込み
            Settings settings = new Settings();

            // 本日の日付を取得
            DateTime dateValue = DateTime.Now;
            while (settings.getOperationDays() != dateValue.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture))
            {
                dateValue = dateValue.AddDays(1);
            }

            // 次回の運行日を代入
            int operationYear = Int32.Parse(dateValue.ToString("yyyy", System.Globalization.CultureInfo.InvariantCulture));
            int operationMonth = Int32.Parse(dateValue.ToString("MM", System.Globalization.CultureInfo.InvariantCulture));
            int operationDate = Int32.Parse(dateValue.ToString("dd", System.Globalization.CultureInfo.InvariantCulture));
            dateTimePickerOperationDays.Value = new DateTime(operationYear, operationMonth, operationDate);
        }

        private void buttonUserList_Click(object sender, EventArgs e)
        {
            UserList userList = new UserList();
            userList.ShowDialog();

            // データを読み込む
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

            // IdとNameを辞書で管理
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
            // 設定情報を読み込み
            Settings settings = new Settings();

            // 最大乗車人数の確認
            // 選択されている項目の個数を取得
            int checkedCount = 0;
            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                checkedCount++;
            }
            // 選択されている項目の数が最大乗車人数を超えている場合
            if (checkedCount > settings.getMaximumPeople())
            {
                MessageBox.Show("最大乗車人数【" + settings.getMaximumPeople() + "人】を超えています。");
                return;
            }
            // 誰も選択されていない場合
            else if (checkedCount == 0)
            {
                MessageBox.Show("乗車する人を選択してください。");
                return;
            }

            // 運行日の曜日を確認する
            string dayOfWeek = dateTimePickerOperationDays.Value.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture);
            string dayOfWeekJp = dateTimePickerOperationDays.Value.ToString("ddd");

            if (dayOfWeek != settings.getOperationDays())
            {
                DialogResult result = MessageBox.Show("運行日は" + dayOfWeekJp + "曜日です。このまま乗車名簿を作成しますか？", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }


            // IdとNameを辞書で管理
            UserListDatabase users = new UserListDatabase();
            var userInfo = new Dictionary<int, string>();
            userInfo = users.getUserInfo();


            //★同姓同名が存在しない前提
            // 乗車する人の情報を取得する
            UserInfo[] ui = Array.Empty<UserInfo>();
            int count = 0;

            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                foreach (var data in userInfo)
                {
                    if (item == data.Value)
                    {
                        // 配列を拡張
                        Array.Resize(ref ui, count + 1);
                        
                        // 配列を初期化
                        ui[count] = new UserInfo();

                        //
                        DataTable userData = users.getUserData(data.Key);

                        // 使用者情報を代入
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

            // 乗客リストをバス停の順番に並び替える



            ////
            //
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "-" + "乗客リスト" + ".xlsx";

            // テンプレートファイルをコピー
            File.Copy("template\\_busPassengerList.xlsx", "application forms\\" + fileName);

            // 現在のディレクトリを取得
            string currentDirectory = Directory.GetCurrentDirectory();

            // 現在のディレクトリからの相対パスを作成
            // Excelファイル
            string wordFile = Path.Combine(currentDirectory, "application forms\\" + fileName);

            // Excelに出力
            var excelApp = new Excel.Application();
            var wbs = excelApp?.Workbooks;
            var wb = wbs.Add();
            var wss = wb?.Sheets;
            var ws = wss[1] as Excel.Worksheet;
            var targetRange = ws?.Range["A1:A10"] as Excel.Range;
            var columns = targetRange?.Columns as Excel.Range;

            if (excelApp is not null && wb is not null && ws is not null && targetRange is not null)
            {
                try
                {
                    excelApp.Visible = true;
                    var startDate = DateTime.Today;
                    var dates = new DateTime[10];
                    for (int i = 0; i < 10; i++)
                    {
                        dates[i] = startDate.AddDays(i);
                    }
                    targetRange.NumberFormat = "yyyy年mm月dd日";
                    targetRange.Value = dates;
                    columns.AutoFit();
                }
                finally
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(columns);
                    Marshal.ReleaseComObject(targetRange);
                    Marshal.ReleaseComObject(ws);
                    Marshal.ReleaseComObject(wss);
                    Marshal.ReleaseComObject(wb);
                    Marshal.ReleaseComObject(wbs);
                    Marshal.ReleaseComObject(excelApp);
#pragma warning restore CA1416
                }
            }




            // リストを作成する
            MessageBox.Show("作成完了！");

        }
    }
}
