using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPassengerListCreationTool
{

    internal class Settings
    {
        // 乗車人数の指定する
        private int maximumPeople = 8;
        // バスの運行曜日
        private string operationDays = "Fri";
        // バス停の一覧
        private string[] busStopName = {
                "末",
                "更毛",
                "桜ヶ丘",
                "本堂",
                "ヒロミストアー前",
                "羽坂団地",
                "細坂",
                "北堀",
                "恐神"
            };
        
        public int getMaximumPeople()
        {
            return maximumPeople;
        }

        public string getOperationDays() {
            return operationDays;
        }

        public string[] getBusStopName()
        {
            return busStopName;
        }
    }

}
