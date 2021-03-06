﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic.Interfaces;

namespace Karnaugh_Logic
{
    /// <summary>
    /// カルノー図の要素クラス
    /// </summary>
    public class KarnoughComponent : IKarnoughComponent
    {
        /// <summary>
        /// 設定されている値。初期値はNull
        /// </summary>
        public TruthValue values { get; set; }
        
        
        /// <summary>
        /// ブロック化後のブロック番号。
        /// 未定義の場合は0
        /// </summary>
        public List<byte> blockValue { get; set; }

        public KarnoughComponent(TruthValue value = TruthValue.Null)
        {
            this.blockValue = new List<byte>();
            this.values = value;

        }

        public KarnoughComponent(List<byte> blockValue,TruthValue value = TruthValue.Null)
        {
            this.blockValue = blockValue;
            this.values = value;
        }

    }
}
