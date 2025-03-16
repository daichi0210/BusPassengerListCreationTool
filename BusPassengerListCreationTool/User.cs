using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BusPassengerListCreationTool
{
    class User
    {
        private static Dictionary<string, string> _column = new Dictionary<string, string>()
        {
            {"LastName", "TEXT"},// 姓
            {"FirstName", "TEXT"},// 名
            {"LastNameKana", "TEXT"},// セイ
            {"FirstNameKana", "TEXT"},// メイ
            {"Address", "TEXT"},// 住所
            {"Tel", "TEXT"},// 固定電話番号
            {"MobileNumber", "TEXT"},// 携帯電話番号
            {"BusStop", "TEXT"},// バス停
            {"Remarks", "TEXT" }// 備考
        };

        private string _lastName;       // 姓
        private string _firstName;      // 名
        private string _lastNameKana;   // 姓（カナ）
        private string _firstNameKana;  // 名（カナ）
        private string _address;        // 住所
        private string _tel;            // 電話番号（固定電話）
        private string _mobileNumber;   // 電話番号（携帯電話）
        private string _busStop;        // 乗車バス停
        private string _remarks;        // 備考

        public User()
        {
            var t = typeof(User);
            foreach (var f in t.GetFields())
            {
                var n = f.Name;
                var v = f.GetValue(t);
                MessageBox.Show(string.Format("フィールド名：{0}, 値：{1}", n, v));
            }


        }

        public Dictionary<string, string> Column
        {
            get { return _column; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string LastNameKana
        {
            get { return _lastNameKana; }
            set { _lastNameKana = value; }
        }
        public string FirstNameKana
        {
            get { return _firstNameKana; }
            set { _firstNameKana = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
        }
        public string BusStop
        {
            get { return _busStop; }
            set { _busStop = value; }
        }
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
    }
}
