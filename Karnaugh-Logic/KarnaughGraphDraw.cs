using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Karnaugh_Logic
{
    /// <summary>
    /// カルノー図の描画クラス
    /// </summary>
    public class KarnaughGraphDraw : IDisposable
    {
        private Device device = null;

        public KarnaughGraphDraw(KarnaughGraphControl target)
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            device = new Device(
                0,
                DeviceType.Hardware,
                target,
                CreateFlags.HardwareVertexProcessing,
                pp
                );
        }

        /// <summary>
        /// リソースの破棄
        /// </summary>
        public void Dispose()
        {
            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        public void Paint()
        {
            if (device == null) return;

            device.Clear(ClearFlags.Target, Color.FromArgb(0x333333), 1.0f, 0);

            device.BeginScene();
            device.EndScene();
            device.Present();
        }
    }
}
