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

        public KarnoughLogic()
        {
            valueNames = new List<string>();
            values = new List<List<TruthValue>>();
        }

        /// <summary>
        /// 論理式を文字列として出力
        /// </summary>
        /// <returns>論理式</returns>
        public string genLogicExpression()
        {
            string outputStr = "";
            int n = 1;

            foreach (List<TruthValue> lstValue in values)
            {
                int count = 1;
                foreach (TruthValue v in lstValue)
                {
                    bool nullFlug = false;
                    switch (v)
                    {
                        case TruthValue.True:
                            outputStr += (valueNames[count-1]);
                            break;
                        case TruthValue.False:
                            outputStr += (notExp(valueNames[count-1]));
                            break;
                        case TruthValue.Null:
                            nullFlug = true;
                            break;
                    }

                    if (checkNullIndex(count, lstValue) == true)
                    {
                        break;
                    }

                    if (count < lstValue.Count() && nullFlug == false)
                    {
                        outputStr += "*";
                    }
                    count++;
                }

                if(lstValue.Count() > n)
                {
                    outputStr += "+";
                }
                n++;
            }

            return outputStr;
        }

        private string notExp(string str)
        {
            return string.Format("not({0})", str);
        }

        private bool checkNullIndex(int point,List<TruthValue> values)
        {
            bool output = true;
            for(int i=point;i<values.Count(); i++)
            {
                if(values[i] != TruthValue.Null)
                {
                    output = false;
                    break;
                }
            }

            return output;
        }
    }
}
