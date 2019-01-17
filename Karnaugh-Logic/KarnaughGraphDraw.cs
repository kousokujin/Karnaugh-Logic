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
        private SharpDX.Direct2D1.Factory fact;

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

            this.fact = new SharpDX.Direct2D1.Factory();
            render = new SharpDX.Direct2D1.WindowRenderTarget(this.fact, new RenderTargetProperties(), hwnd);
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

        public void Value2Map(IKarnoughMap map)
        {
            int valueCount = map.valueNames.Count();
            //int valueCount = 3;
            render.BeginDraw();

            render.Clear(background);

            int margin = 10;
            int ColumHeight = 30;
            int RowWidth = 100;

            //外枠
            render.DrawLine(new RawVector2(margin, margin), new RawVector2(margin, height - margin), LineColor);
            render.DrawLine(new RawVector2(margin, margin), new RawVector2(width - margin, margin), LineColor);
            render.DrawLine(new RawVector2(width - margin, height - margin), new RawVector2(width - margin, margin), LineColor);
            render.DrawLine(new RawVector2(width - margin, height - margin), new RawVector2(margin, height - margin), LineColor);

            render.DrawLine(new RawVector2(margin, margin + (ColumHeight * 2)), new RawVector2(width - margin, margin + (ColumHeight * 2)), LineColor);
            render.DrawLine(new RawVector2(margin + (RowWidth * 2), margin), new RawVector2(margin + (RowWidth * 2), height - margin), LineColor);

            render.DrawLine(new RawVector2(margin + (RowWidth * 2), margin + ColumHeight), new RawVector2(width - margin, margin + ColumHeight), LineColor);
            render.DrawLine(new RawVector2(margin + RowWidth, margin + (ColumHeight * 2)), new RawVector2(margin + RowWidth, height - margin), LineColor);

            //行と列の数の設定
            int columCount = 2;
            int rowCount = 2;
            for (int i = 3; i <= valueCount; i++)
            {
                if (i % 2 == 1)
                {
                    columCount = columCount * 2;
                }
                else
                {
                    rowCount = rowCount * 2;
                }
            }

            //セルの大きさ
            int valueWidth = ((width - margin) - (margin + (RowWidth * 2))) / columCount;
            int valueHeight = ((height - margin) - (margin + (ColumHeight * 2))) / rowCount;

            //縦の罫線
            for (int i = 1; i < columCount; i++)
            {
                //int valueWidth = ((width - margin) - (margin + (RowWidth * 2)))/columCount;
                int x = margin + (RowWidth * 2) + (valueWidth * i);
                int y_start = margin + ColumHeight;
                int y_end = height - margin;

                render.DrawLine(new RawVector2(x, y_start), new RawVector2(x, y_end), LineColor);
            }

            //横の罫線
            for (int i = 1; i < rowCount; i++)
            {
                //int valueHeight = ((height - margin) - (margin + (ColumHeight * 2)))/rowCount;
                int y = margin + (ColumHeight * 2) + (valueHeight * i);
                int x_start = margin + RowWidth;
                int x_end = width - margin;

                render.DrawLine(new RawVector2(x_start, y), new RawVector2(x_end, y), LineColor);
            }

            //文字の描画
            var fontFactory = new SharpDX.DirectWrite.Factory();
            var textFormat = new TextFormat(fontFactory, "メイリオ", 24.0f);
            textFormat.TextAlignment = TextAlignment.Center;
            var textBrush = new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4(1f, 1f, 1f, 1f));

            //列のインデックス
            string[] columStr = valueStr(columCount);
            for (int i = 0; i < columCount; i++)
            {
                int x_shift = margin + (RowWidth * 2);
                int x_start = x_shift + (valueWidth * i);
                int x_end = x_shift + (valueWidth * (i + 1));

                int y_start = margin + ColumHeight;
                int y_end = margin + (ColumHeight * 2);

                TextFormat textTmpFormat;
                if(columCount >= 8)
                {
                    textTmpFormat = new TextFormat(fontFactory, "メイリオ", 20.0f);
                    textTmpFormat.TextAlignment = TextAlignment.Center;
                }
                else
                {
                    textTmpFormat = textFormat;
                }
                render.DrawText(columStr[i], textTmpFormat, new RawRectangleF(x_start, y_start, x_end, y_end), textBrush, DrawTextOptions.None);
            }

            //行のインデックス
            string[] rowStr = valueStr(rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                int x_start = margin + RowWidth;
                int x_end = x_start + RowWidth;

                int y_shift = margin + (ColumHeight * 2);
                int y_start = y_shift + (valueHeight * i) + (valueHeight/2-10);
                int y_end = y_start + (valueHeight * (i + 1));

                render.DrawText(rowStr[i], textFormat, new RawRectangleF(x_start, y_start, x_end, y_end), textBrush, DrawTextOptions.None);
            }

            //mapを描画
            for (int i = 0; i < rowCount; i++)
            {
                for(int j = 0; j < columCount; j++)
                {
                    int xStartCell = margin + (RowWidth * 2) + (valueWidth * j);
                    int yStartCell = margin + (ColumHeight * 2) + (valueHeight * i) + (valueHeight/2 - 10);
                    int xEndCell = xStartCell + valueWidth;
                    int yEndCell = yStartCell + valueHeight;


                    TruthValue v = map.getMapPoint(j, i).values;
                    string s = "";
                    switch (v)
                    {
                        case TruthValue.False:
                            s = "0";
                            break;
                        case TruthValue.True:
                            s = "1";
                            break;
                        case TruthValue.Null:
                            s = "d";
                            break;
                    }

                    var MaptextFormat = new TextFormat(fontFactory, "メイリオ", 30.0f);
                    var mapBrush = indexFontColor(map.getMapPoint(j, i).blockValue);
                    MaptextFormat.TextAlignment = TextAlignment.Center;
                    render.DrawText(s, MaptextFormat, new RawRectangleF(xStartCell, yStartCell, xEndCell, yEndCell), mapBrush, DrawTextOptions.None);
                }
            }

            //列の変数名
            int x_label_Xstart = margin + (RowWidth * 2);
            int x_label_Xend = width - margin;
            int x_label_Ystart = margin;
            int x_label_Yend = margin + ColumHeight;
            render.DrawText(getlabel(map,valueCount,true), textFormat, new RawRectangleF(x_label_Xstart, x_label_Ystart, x_label_Xend, x_label_Yend), textBrush, DrawTextOptions.None);

            //行の変数名
            int y_label_Xstart = margin;
            int y_label_Xend = margin + RowWidth;
            int y_label_Ystart = margin + (ColumHeight * 2) + ((valueHeight * rowCount) / 2) - (10 * rowCount /4);
            int y_label_Yend = height - margin;
            render.DrawText(getlabel(map, valueCount, false), textFormat, new RawRectangleF(y_label_Xstart, y_label_Ystart, y_label_Xend, y_label_Yend), textBrush, DrawTextOptions.None);

            render.EndDraw();
        }

        /// <summary>
        /// 変数数に応じてハミング距離が違う01列を返す。カルノー図の表で使う
        /// </summary>
        /// <param name="valueCount">変数の数</param>
        /// <returns></returns>
        private string[] valueStr(int valueCount)
        {
            string[] values;
            switch (valueCount)
            {
                case 2:
                    values = new string[] { "0", "1" };
                    break;
                case 4:
                    values = new string[]{ "00", "01", "11", "10" };
                    break;
                case 8:
                    values = new string[]{ "000", "001", "011", "010", "110", "111", "101", "100" };
                    break;
                default:
                    values = new string[] { "0", "1" }; //とりあえず1ことおなじ
                    break;
            }

            return values;
        }

        private string getlabel(IKarnoughMap com, int count,bool isXlabel)
        {
            string str = "";

            if (isXlabel)
            {
                int loop = 0;
                for(int i = 0; i < count; i++)
                {
                    if(i % 2 == 0)
                    {
                        loop++;
                    }
                }
                for(int i = 0; i < loop; i++)
                {
                    str += com.valueNames[i];
                    if(i != (loop - 1))
                    {
                        str += "/";
                    }
                }
            }
            else
            {
                int loop = 0;
                for(int i = 0; i < count; i++)
                {
                    if(i %2 == 1)
                    {
                        loop++;
                    }
                }
                int shift = com.valueNames.Count - loop;
                for (int i = 0; i < loop; i++) {
                    str += com.valueNames[shift + i];
                    if(i != (loop - 1))
                    {
                        str += "\n";
                    }
                }
            }

            return str;
        }

        private SolidColorBrush indexFontColor(int index)
        {
            switch (index)
            {
                case 0:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4(1f, 1f, 1f, 1f));
                case 1:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4((252f / 255f), (139f / 255f), (108f / 255f), 1f));
                case 2:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4((142f / 255f), (252f / 255f), (108f / 255f), 1f));
                case 3:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4((108f / 255f), (252f / 255f), (252f / 255f), 1f));
                case 4:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4((112f / 255f), (168f / 255f), (216f / 255f), 1f));
                case 5:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4((125f / 255f), (121f / 255f), (209f / 255f), 1f));
                case 6:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4((199f / 255f), (150f / 255f), (206f / 255f), 1f));
                default:
                    return new SharpDX.Direct2D1.SolidColorBrush(render, new RawColor4(1f, 1f, 1f, 1f));

            }
        }

        /// <summary>
        /// 座標の回転・平行移動
        /// </summary>
        /// <param name="x">xの平行移動</param>
        /// <param name="y">yの平行移動</param>
        /// <param name="theta">回転角</param>
        /// <returns>座標変換の行列</returns>
        private RawMatrix3x2 TransrationMatrix(float x,float y, double theta)
        {
            //自分で書かないといけないのにキレそう
            var matrix = new RawMatrix3x2();
            matrix.M11 = (float)Math.Cos(theta);
            matrix.M12 = (float)Math.Sin(theta);
            matrix.M21 = (float)Math.Sin(theta)*(-1);
            matrix.M22 = (float)Math.Cos(theta);
            matrix.M31 = x;
            matrix.M32 = y;

            return matrix;
        }
  
    }
}
