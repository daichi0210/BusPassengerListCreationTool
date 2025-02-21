using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPassengerListCreationTool
{
    class UserInfo
    {
        private string _name;    // 名前
        private string _address;  // 住所
        private string _tel;      // 電話番号
        private string _busStop;  // 乗車バス停
        private string _remarks;  // 備考
/*
        public UserInfo(string name, string address, string tel, string busStop, string remarks)
        {
            _name = name;
            _address = address;
            _tel = tel;
            _busStop = busStop;
            _remarks = remarks;
        }
*/
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
