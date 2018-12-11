using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic.Interfaces;

namespace Karnaugh_Logic
{
    /// <summary>
    /// 真理値表データ
    /// </summary>
    public class LogicTable :ILogicTable
    {
        /// <summary>
        /// 変数名
        /// </summary>
        public List<string> valueNames { get; set; }

        /// <summary>
        /// 論理式が1となる変数の組み合わせ
        /// </summary>
        public List<List<bool>> trueList { get; set; }

        /// <summary>
        /// 論理式が0となる組み合わせ
        /// </summary>
        public List<List<bool>> falseList { get; set; }

        /// <summary>
        /// 論理式が0または1になる組み合わせ
        /// </summary>
        public List<List<bool>> nullList { get; set; }

        public LogicTable()
        {
            trueList = new List<List<bool>>();
            falseList = new List<List<bool>>();
            nullList = new List<List<bool>>();
        }

        /// <summary>
        /// trueとなる論理値の組み合わせを追加
        /// </summary>
        /// <param name="vs">論理値組み合わせ(リストの要素数は変数の数と同じ)</param>
        public void addTrueList(List<bool> vs)
        {
            trueList.Add(vs);
        }

        /// <summary>
        /// falseとなる論理値の組み合わせを追加
        /// </summary>
        /// <param name="vs">論理値組み合わせ(リストの要素数は変数の数と同じ)</param>
        public void addFalseList(List<bool> vs)
        {
            falseList.Add(vs);
        }


        /// <summary>
        /// nullとなる論理値の組み合わせを追加
        /// </summary>
        /// <param name="vs">論理値組み合わせ(リストの要素数は変数の数と同じ)</param>
        public void addNullList(List<bool> vs)
        {
            nullList.Add(vs);
        }

    }
}
