using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;


namespace Karnaugh_Logic
{
    /// <summary>
    /// カルノー図の描画クラス
    /// </summary>
    public class KarnaughGraphDraw : IDisposable
    {
        //private Device device;
        private WindowRenderTarget render;
        private KarnaughGraphControl target;

        //解像度
        private int width;
        private int height;


        public KarnaughGraphDraw(KarnaughGraphControl target)
        {
            this.width = target.Width;
            this.height = target.Height;

            var hwnd = new HwndRenderTargetProperties();
            hwnd.Hwnd = target.Handle;
            hwnd.PixelSize = new Size2(this.width, this.height);

            var factory = new SharpDX.Direct2D1.Factory();
            render = new SharpDX.Direct2D1.WindowRenderTarget(factory, new RenderTargetProperties(), hwnd);
            this.target = target;

        }

        /// <summary>
        /// リソースの破棄
        /// </summary>
        public void Dispose()
        {
            if(render != null)
            {
                render.Dispose();
                render = null;
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        public void Paint()
        {
            render.BeginDraw();

            render.Clear(new RawColor4(0.3f, 0.3f, 0.3f, 0f));

            var fontFactory = new SharpDX.DirectWrite.Factory();
            var textFormat = new TextFormat(fontFactory, "メイリオ", 24.0f);
            textFormat.TextAlignment = TextAlignment.Center;
            var textBrush = new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4(1f, 1f, 1f, 1f));
            render.DrawText("ここにカルノー図が表示されます", textFormat, new RawRectangleF(10, (this.height/2)-12, target.Width-10, target.Height-10), textBrush, DrawTextOptions.None);

            render.EndDraw();
        }
    }
}
