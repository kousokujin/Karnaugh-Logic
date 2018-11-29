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
using Karnaugh_Logic.Interfaces;


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

        //線の色
        private SharpDX.Direct2D1.Brush LineColor;
        //背景色
        private RawColor4 background;

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
            
            //いろいろ設定
            LineColor = new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4(1f, 1f, 1f, 1f));
            background = new RawColor4(0.3f, 0.3f, 0.3f, 0f);
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

            render.Clear(background);

            //Value2Map();
            var fontFactory = new SharpDX.DirectWrite.Factory();
            var textFormat = new TextFormat(fontFactory, "メイリオ", 24.0f);
            textFormat.TextAlignment = TextAlignment.Center;
            var textBrush = new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4(1f, 1f, 1f, 1f));
            render.DrawText("ここにカルノー図が表示されます", textFormat, new RawRectangleF(10, (this.height/2)-12, target.Width-10, target.Height-10), textBrush, DrawTextOptions.None);

            render.EndDraw();
        }

        /// <summary>
        /// カルノー図データから
        /// </summary>
        /// <param name="map"></param>
        internal void DrawGraph(IKarnoughMap map)
        {
            render.BeginDraw();
            render.Clear(new RawColor4(0.3f, 0.3f, 0.3f, 0f));

            switch (map.dimension)
            {
                case 2:
                    break;
            }
        }

        //ここからカルノー図描画メソッド

        //2変数
        public void Value2Map()
        {
            render.BeginDraw();

            render.Clear(background);

            int margin = 10;
            int ColumHeight = 30;
            int RowWidth = 50;

            //外枠
            render.DrawLine(new RawVector2(margin, margin), new RawVector2(margin, height - margin), LineColor);
            render.DrawLine(new RawVector2(margin, margin), new RawVector2(width - margin, margin), LineColor);
            render.DrawLine(new RawVector2(width - margin, height - margin), new RawVector2(width - margin, margin), LineColor);
            render.DrawLine(new RawVector2(width - margin, height - margin), new RawVector2(margin, height - margin), LineColor);

            render.DrawLine(new RawVector2(margin, margin + (ColumHeight * 2)), new RawVector2(width - margin, margin + (ColumHeight * 2)), LineColor);
            render.DrawLine(new RawVector2(margin + (RowWidth * 2), margin), new RawVector2(margin + (RowWidth * 2), height - margin), LineColor);

            render.DrawLine(new RawVector2(margin + (RowWidth * 2), margin + ColumHeight), new RawVector2(width - margin, margin + ColumHeight), LineColor);
            render.DrawLine(new RawVector2(margin + RowWidth, margin + (ColumHeight * 2)), new RawVector2(margin + RowWidth, height - margin), LineColor);

            int columCount = 4;
            int rowCount = 4;

            //縦の罫線
            for (int i = 1; i < columCount; i++)
            {
                int valueWidth = ((width - margin) - (margin + (RowWidth * 2)))/columCount;
                int x = margin + (RowWidth * 2) + (valueWidth * i);
                int y_start = margin + ColumHeight;
                int y_end = height - margin;

                render.DrawLine(new RawVector2(x, y_start), new RawVector2(x, y_end), LineColor);
            }

            //横の罫線
            for (int i = 1; i < rowCount; i++)
            {
                int valueHeight = ((height - margin) - (margin + (ColumHeight * 2)))/rowCount;
                int y = margin + (ColumHeight * 2) + (valueHeight * i);
                int x_start = margin + RowWidth;
                int x_end = width - margin;

                render.DrawLine(new RawVector2(x_start, y), new RawVector2(x_end, y),LineColor);
            }

            render.EndDraw();
        }
  
    }
}
