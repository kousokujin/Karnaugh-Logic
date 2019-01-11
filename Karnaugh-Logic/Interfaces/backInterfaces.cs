using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic;

namespace Karnaugh_Logic.Interfaces
{

    /// <summary>
    /// 変数名を扱うインターフェイス
    /// </summary>
    public interface IValueName
    {
        List<string> valueNames
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 真理値表のデータ形式のインターフェース
    /// List<bool>の変数の数は論理式の変数の数と同じ
    /// </summary>
    public interface ILogicTable : IValueName
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
        void addTrueList(List<bool> trueList);

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
    /// 文字列で表された論理式から真理値表を作成
    /// </summary>
    public interface IToLogicTable : IValueName
    {
        /// <summary>
        /// 論理式から真理値表を作るメソッド
        /// </summary>
        /// <param name="LogicStr">論理式</param>
        /// <returns>真理値表</returns>
        ILogicTable convertLogicTable(string LogicStr);
    }

    /// <summary>
    /// カルノー図による簡略化後のデータのインターフェース
    /// 各変数の積
    /// </summary>
    public interface IKarnoughLogic : IValueName
    {
        /// <summary>
        /// List<byte>の変数の数は論理値の変数と同じ。
        /// </summary>
        List<List<TruthValue>> values
        {
            get;
            set;
        }

        /// <summary>
        /// 簡略化後の式を文字列で出力
        /// </summary>
        /// <returns>簡略化後の論理式</returns>
        string genLogicExpression();
    }


    /// <summary>
    /// カルノー図インターフェース
    /// </summary>
    public interface IKarnoughMap : IValueName
    {
        /// <summary>
        /// カルノー図の次元(3次元が最大)
        /// </summary>
        int dimension { get; set; }

        /// <summary>
        /// パラメーターで指定したカルノー図の値を返す。
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="z">z座標(2次元の時は0)</param>
        /// <returns>カルノー図の要素</returns>
        IKarnoughComponent getMapPoint(int x, int y, int z = 0);

        /// <summary>
        /// パラメータで指定した座標にカルノー図の値を設定
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="z">z座標</param>
        /// <param name="value"></param>
        void setMapPoint(IKarnoughComponent value,int x, int y, int z = 0);

        /// <summary>
        /// ブロックIDのIKarnoughComponentを取得する
        /// </summary>
        /// <param name="blockId">ブロックID</param>
        /// <returns>そのBlockIDを持つIKarnoughComopnent</returns>
        List<IKarnoughComponent> getBlockIDList(int blockId);

    }

    /// <summary>
    /// カルノー図の要素インターフェース
    /// </summary>
    public interface IKarnoughComponent
    {
        /// <summary>
        /// カルノー図の要素
        /// </summary>
        TruthValue values { get; set; }

        /// <summary>
        /// ブロック化後のブロック番号
        /// </summary>
        byte blockValue { get; set; }
    }
}
