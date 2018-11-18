using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karnaugh_Logic.Interfaces
{
    /// <summary>
    /// カルノー図を表示するインターフェース
    /// </summary>
    interface FrontEndInterface
    {
        /// <summary>
        /// カルノー図からGUIに図を生成。
        /// </summary>
        /// <param name="map">カルノー図</param>
        void drawKarnaughGraph(Interfaces.IKarnoughMap map);

        /// <summary>
        /// カルノー図で簡略化した式を表示
        /// </summary>
        /// <param name="logic">簡略化式</param>
        void showOptimizedLogic(string logic);
    }
}
