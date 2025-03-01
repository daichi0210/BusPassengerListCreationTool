using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.Immutable;

// Excel操作用
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
// Excel起動用
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

            // 乗客リストをバス停の順番に並び替える
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

            ////
            //★運行日の日付にするdayOfWeek
            string fileName = "あごころ乗車名簿_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";

            // テンプレートファイルをコピー
            //★上書き確認
            //★フォルダがない場合
            //★テンプレートがない場合
            //File.Copy("template\\_busPassengerList.xlsx", "bus passenger list\\" + fileName);

            // 現在のディレクトリを取得
            string currentDirectory = Directory.GetCurrentDirectory();

            // 現在のディレクトリからの相対パスを作成
            // Excelファイル
            //string excelFile = Path.Combine(currentDirectory, "bus passenger list\\" + fileName);
            //string excelFile = Path.Combine(currentDirectory, "template\\_busPassengerList.xlsx");
            string excelFile = Path.Combine(currentDirectory, "template\\" + fileName);

            // Excelの実行ファイルのパスを指定
            string excelPath = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
            
            // Excelファイルに出力
            //using (var wb = new XLWorkbook(excelFile))
            using (var wb = new XLWorkbook())
            {
                var ws = wb.AddWorksheet("乗車名簿");
                var row = ws.Row(1);
                var column = ws.Column(1);

                // 年を和暦で表示するための準備
                CultureInfo japanese = new CultureInfo("ja-JP");
                japanese.DateTimeFormat.Calendar = new JapaneseCalendar();

                // 運行日をセルへ代入
                ws.Cell(1, 1).Value = "運行日";
                ws.Cell(1, 2).Value = dateTimePickerOperationDays.Value.ToString("ggy年M月d日", japanese);


                // 開始行を指定
                int startRow = 3;
                // 開始列を指定
                int startColumn = 1;

                // 開始セルを指定
                //var cell = ws.Cell(startColumn, startRow);

                // ヘッダー
                ws.Cell(startRow, startColumn).Value = "名前";
                ws.Cell(startRow, startColumn + 1).Value = "住所";
                ws.Cell(startRow, startColumn + 2).Value = "電話番号";
                ws.Cell(startRow, startColumn + 3).Value = "バス停";
                ws.Cell(startRow, startColumn + 4).Value = "備考";

                startRow++;

                // 各セルへ値を代入
                foreach (UserInfo uis in uiBusStopOrder)
                {
                    ws.Cell(startRow, startColumn).Value = uis.Name;
                    ws.Cell(startRow, startColumn + 1).Value = uis.Address;
                    ws.Cell(startRow, startColumn + 2).Value = uis.Tel;
                    ws.Cell(startRow, startColumn + 3).Value = uis.BusStop;
                    ws.Cell(startRow, startColumn + 4).Value = uis.Remarks;

                    startRow++;
                }

                wb.SaveAs("template\\" + fileName);
            }

            // Excelを開く
            //★開き方を要検討。Excelファイルで書き出すだけでいいかも
            Process.Start(excelPath, excelFile);


            // リストを作成する
            MessageBox.Show("作成完了！");

        }
    }
}
