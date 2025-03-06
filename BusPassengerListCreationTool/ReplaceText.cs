using System;
using System.Text.RegularExpressions;

namespace BusPassengerListCreationTool
{
    public class ReplaceText
    {
        // 全角数字を半角数字に置換
        public string fullToHalfNumbers(string text)
        {
            return Regex.Replace(text, "[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());
        }

        // 全角ハイフンを半角ハイフンに置換
        public string fullToHalfHyphen(string text)
        {
            return Regex.Replace(text, "－", "-");
        }

    }
}