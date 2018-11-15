using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karnaugh_Logic.Interfaces
{
    /// <summary>
    /// 真理値表のデータ形式のインターフェース
    /// List<bool>の変数の数は論理式の変数の数と同じ
    /// </summary>
    interface ILogicTable
    {
        /// <summary>
        /// 論理式が1となる変数の組み合わせ
        /// </summary>
        List<List<bool>> trueList
        {
            get;
            set;
        }

        /// <summary>
        /// 論理式が0となる変数の組み合わせ
        /// これはなくてもいいかも？
        /// </summary>
        List<List<bool>> falseList
        {
            get;
            set;
        }

        /// <summary>
        /// 論理式が0か1になる組み合わせ
        /// ドントケア用
        /// </summary>
        List<List<bool>> nullList
        {
            get;
            set;
        }

        /// <summary>
        /// trueとなる論理値の組み合わせを追加
        /// </summary>
        /// <param name="">論理値組み合わせ(リストの要素数は変数の数と同じ)</param>
        void addTrueList(List<bool>　trueList);

        /// <summary>
        /// falseとなる論理値の組み合わせ
        /// </summary>
        /// <param name="falseList">論理値組み合わせ(リストの要素数は変数の数と同じ)</param>
        void addFalseList(List<bool> falseList);

        /// <summary>
        /// nullとなる論理値の組み合わせ
        /// </summary>
        /// <param name="nullList">論理値の組み合わせ(リストの要素数は変数の数と同じ</param>
        void addNullList(List<bool> nullList);
    }

    /// <summary>
    /// カルノー図による簡略化後のデータのインターフェース
    /// 各変数の積
    /// </summary>
    interface IKarnoughLogic
    {
        /// <summary>
        /// List<byte>の変数の数は論理値の変数と同じ。
        /// 0:false 1:true  2:なし
        /// </summary>
        List<List<byte>> values
        {
            get;
            set;
        }
    }


    /// <summary>
    /// カルノー図インターフェース
    /// </summary>
    interface IKarnoughMap
    {
        /// <summary>
        /// パラメーターで指定したカルノー図の値を返す。
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="z">z座標(2次元の時は0)</param>
        /// <returns></returns>
        byte getMapPoint(int x, int y, int z = 0);

        /// <summary>
        /// ILogicTableからカルノー図を生成する。
        /// </summary>
        /// <param name="table">真理値表</param>
        void createMap(ILogicTable table);

        /// <summary>
        /// 簡略化の実行
        /// </summary>
        /// <returns>簡略化後の変数データ</returns>
        IKarnoughLogic optimize();
    }
}
