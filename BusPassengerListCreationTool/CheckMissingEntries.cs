using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusPassengerListCreationTool
{
    class CheckMissingEntries
    {
        // 未入力・未選択チェック
        public bool NoInput(string value, string category)
        {
            if (value == String.Empty)
            {
                MessageBox.Show(category + "を入力してください。");
                return true;
            }

            return false;

        }
        public bool NoChoice(string value, string category)
        {
            if (value == String.Empty)
            {
                MessageBox.Show(category + "を選択してください。");
                return true;
            }

            return false;
        }
    }
}
