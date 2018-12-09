using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic.Interfaces;

namespace Karnaugh_Logic
{
    public class KarnoughLogic : IKarnoughLogic
    {
        /// <summary>
        /// 変数名
        /// </summary>
        public List<string> valueNames{ get; set; }

        /// <summary>
        /// 論理式の積の集合。積を和でつなぐ。
        /// </summary>
        public List<List<TruthValue>> values { get; set; }

        /// <summary>
        /// 論理式を文字列として出力
        /// </summary>
        /// <returns>論理式</returns>
        public string genLogicExpression()
        {
            string outputStr = "";

            foreach (List<TruthValue> lstValue in values)
            {
                int count = 0;
                foreach(TruthValue v in lstValue)
                {
                    switch (v)
                    {
                        case TruthValue.True:
                            outputStr += (valueNames[count] + "*");
                            break;
                        case TruthValue.False:
                            outputStr += (notExp(valueNames[count]) + "*");
                            break;
                    }
                    count++;
                }
                outputStr += "+";
            }

            return outputStr;
        }

        private string notExp(string str)
        {
            return string.Format("not({0})", str);
        }
    }
}
