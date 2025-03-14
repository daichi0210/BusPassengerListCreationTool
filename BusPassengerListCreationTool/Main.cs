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

            UserListDatabase uld = new UserListDatabase();

            // IdとNameを辞書で管理
            var userInfo = new Dictionary<int, string>();
            userInfo = uld.getUserInfo();

            foreach (string name in userInfo.Values)
            {
                //MessageBox.Show(id.ToString());
                checkedListBoxUserSelection.Items.Add(name);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            // 設定情報を読み込み
            Settings s = new Settings();

            // 最大乗車人数の確認
            // 選択されている項目の個数を取得
            int checkedCount = 0;
            foreach (string item in checkedListBoxUserSelection.CheckedItems)
            {
                checkedCount++;
            }
            // 選択されている項目の数が最大乗車人数を超えている場合
            if (checkedCount > s.getMaximumPeople())
            {
                MessageBox.Show("最大乗車人数【" + s.getMaximumPeople() + "人】を超えています。");
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

            if (dayOfWeek != s.getOperationDays())
            {
                DialogResult result = MessageBox.Show("運行日は" + dayOfWeekJp + "曜日です。このまま乗車名簿を作成しますか？", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // IdとNameを辞書で管理
            UserListDatabase uld = new UserListDatabase();
            var userInfo = new Dictionary<int, string>();
            userInfo = uld.getUserInfo();

            //★同姓同名が存在しない前提
            // 乗車する人の情報を取得する
            //UserInfo[] ui = Array.Empty<UserInfo>();
            User[] ui = Array.Empty<User>();
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
                        //ui[count] = new UserInfo();
                        ui[count] = new User();

                        //
                        DataTable userData = uld.getUserData(data.Key);

                        // 使用者情報を代入
                        ui[count].LastName = userData.Rows[0]["LastName"].ToString();
                        ui[count].FirstName = userData.Rows[0]["FirstName"].ToString();
                        ui[count].Address = userData.Rows[0]["Address"].ToString();
                        ui[count].Tel = userData.Rows[0]["Tel"].ToString();
                        ui[count].MobileNumber = userData.Rows[0]["MobileNumber"].ToString();
                        ui[count].BusStop = userData.Rows[0]["BusStop"].ToString();
                        ui[count].Remarks = userData.Rows[0]["Remarks"].ToString();

                        count++;

                        break;
                    }
                }
            }

            // 乗客リストをバス停の順番に並び替える
            //UserInfo[] uiBusStopOrder = new UserInfo[ui.Length];
            User[] uiBusStopOrder = new User[ui.Length];
            string[] busStopOrder = s.getBusStopName();
            int uibIndex = 0;

            foreach (string busStop in busStopOrder)
            {
                foreach (User i in ui)
                {
                    if (i.BusStop == busStop)
                    {
                        uiBusStopOrder[uibIndex] = i;

                        uibIndex++;
                    }
                }
            }

            // 出力するExcelファイル名を指定
            string fileName = "あごころ乗車名簿_" + dateTimePickerOperationDays.Value.ToString("yyyy-MM-dd") + ".xlsx";

            // 現在のディレクトリを取得
            string currentDirectory = Directory.GetCurrentDirectory();

            // テンプレートファイルが存在しない場合、エラーを表示する
            string templateFile = Path.Combine(currentDirectory, "template\\" + "_busPassengerList.xlsx");
            if (!(File.Exists(templateFile)))
            {
                MessageBox.Show("テンプレートファイルが存在しません。");
                return;
            }

            // 出力用フォルダが存在しない場合、作成する
            string outputFolder = Path.Combine(currentDirectory, "busPassengerList");
            if (!(Directory.Exists(outputFolder)))
            {
                DirectoryInfo di = new DirectoryInfo(outputFolder);
                di.Create();
            }

            // 現在のディレクトリからの相対パスを作成
            string excelFile = Path.Combine(currentDirectory, "busPassengerList\\" + fileName);

            // 出力ファイルと同名のExcelファイルが開かれている場合、エラーが発生する
            try
            {
                // テンプレートファイルをコピー（同名のファイルが存在する場合は上書きする）
                File.Copy("template\\_busPassengerList.xlsx", "busPassengerList\\" + fileName, true);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Excelファイルを閉じてください。");
                return;
            }

            // Excelの実行ファイルのパスを指定
            string excelPath = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
            
            // Excelファイルに出力
            using (var wb = new XLWorkbook(excelFile))
            {
                // ワークシート"テンプレート"を指定
                var ws = wb.Worksheet("乗車名簿");

                // ワークシート名を変更
                ws.Name = dateTimePickerOperationDays.Value.ToString("MMdd");

                // 年を和暦で表示するための準備
                CultureInfo japanese = new CultureInfo("ja-JP");
                japanese.DateTimeFormat.Calendar = new JapaneseCalendar();

                // 運行日のセル
                var operationDaysCell = ws.Cell(2, 6);

                // 運行日を挿入
                operationDaysCell.Value = dateTimePickerOperationDays.Value.ToString("gg y年 M月 d日", japanese);

                // 開始行を指定
                int startRow = 5;
                // 開始列を指定
                int startColumn = 2;

                // 各セルへ値を代入
                //foreach (UserInfo uis in uiBusStopOrder)
                foreach (User uis in uiBusStopOrder)
                {
                    ws.Cell(startRow, startColumn).Value = uis.LastName + "　" + uis.FirstName;
                    ws.Cell(startRow, startColumn + 1).Value = uis.Address;
                    ws.Cell(startRow, startColumn + 2).Value = uis.Tel + Environment.NewLine + uis.MobileNumber;
                    ws.Cell(startRow, startColumn + 3).Value = uis.BusStop;
                    ws.Cell(startRow, startColumn + 4).Value = uis.Remarks;

                    startRow++;
                }

                // 空席の分のセルを書き換え
                for (int i = 0; i < s.getMaximumPeople() - uiBusStopOrder.Length; i++)
                {
                    ws.Cell(startRow, startColumn).Value = "";
                    ws.Cell(startRow, startColumn + 1).Value = "";
                    ws.Cell(startRow, startColumn + 2).Value = "";
                    ws.Cell(startRow, startColumn + 3).Value = "";
                    ws.Cell(startRow, startColumn + 4).Value = "";

                    startRow++;
                }

                // 上書き保存
                wb.SaveAs(excelFile);
            }

            // Excelを開く
            //★開き方を要検討。Excelファイルで書き出すだけでいいかも
            Process.Start(excelPath, excelFile);


            // リストを作成する
            MessageBox.Show("作成完了！");

        }
    }
}
