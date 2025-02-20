using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPassengerListCreationTool
{
    class UserInfo
    {
        private string name;    // 名前
        private string address;  // 住所
        private string tel;      // 電話番号
        private string busStop;  // 乗車バス停
        private string remarks;  // 備考

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        public string BusStop
        {
            get { return busStop; }
            set { busStop = value; }
        }
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
    }
}
