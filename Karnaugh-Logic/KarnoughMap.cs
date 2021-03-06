﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic.Interfaces;

namespace Karnaugh_Logic
{
    public class KarnoughMap : IKarnoughMap
    {
        /// <summary>
        /// 変数名
        /// </summary>
        public List<string> valueNames { get; set; }

        /// <summary>
        /// 次元数
        /// </summary>
        public int dimension { get; set; }

        //この中にカルノー図が格納される
        private List<List<List<IKarnoughComponent>>> valueLists;

        int x_max;
        int y_max;
        int z_max;

        protected TruthValue default_value;

        public KarnoughMap()
        {
            //List<IKarnoughComponent> x = new List<IKarnoughComponent>();
            //List<List<IKarnoughComponent>> y = new List<List<IKarnoughComponent>>();

            valueLists = new List<List<List<IKarnoughComponent>>>();
            //y.Add(x);
            //valueLists.Add(y);
            valueNames = new List<string>();

            x_max = 0;
            y_max = 0;
            z_max = 0;
            addX();
            addY();
            addZ();

            default_value = TruthValue.Null;
            this.dimension = 2;
        }

        /// <summary>
        /// パラメーターで指定したカルノー図の値を返す。
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="z">z座標(2次元の時は0)</param>
        /// <returns>カルノー図の要素</returns>
        public IKarnoughComponent getMapPoint(int x,int y,int z = 0)
        {
            if(z_max <= z || y_max <= y || x_max <= x)
            {
                return new KarnoughComponent(default_value);
            }

            List<List<IKarnoughComponent>> y_list = valueLists[z];
            List<IKarnoughComponent> x_list = y_list[y];
            IKarnoughComponent outValue = x_list[x];

            return outValue;
        }

        /// <summary>
        /// パラメータで指定した座標にカルノー図の値を設定
        /// </summary>
        /// <param name="x">x座標</param>
        /// <param name="y">y座標</param>
        /// <param name="z">z座標</param>
        /// <param name="value">設定したい値</param>
        public void setMapPoint(IKarnoughComponent value,int x,int y,int z = 0)
        {   
            if(z > (z_max-1))
            {
                int d = z - (z_max-1);
                
                for(int i = 0; i < d; i++)
                {
                    addZ();
                }
            }

            if(y > (y_max-1))
            {
                int d = y - (y_max-1);
                for(int i = 0; i < d; i++)
                {
                    addY();
                }
            }

            if(x > (x_max-1))
            {
                int d = x - (x_max-1);
                for(int i = 0; i < d; i++)
                {
                    addX();
                }
            }

            List<List<IKarnoughComponent>> comList = valueLists[z];
            List<IKarnoughComponent> comList2 = comList[y];
            comList2[x] = value;
        }

        //zの要素数追加
        private void addZ()
        {
            List<List<IKarnoughComponent>> y = new List<List<IKarnoughComponent>>();
            for (int i = 0; i < y_max; i++)
            {
                List<IKarnoughComponent> x = new List<IKarnoughComponent>();
                for (int j = 0; j < x_max; j++)
                {
                    x.Add(new KarnoughComponent(default_value));
                }
                y.Add(x);
            }
            valueLists.Add(y);

            z_max++;
        }

        //yの要素数追加
        private void addY()
        {
            foreach(List<List<IKarnoughComponent>> com in valueLists)
            {
                List<IKarnoughComponent> x = new List<IKarnoughComponent>();
                for(int j = 0; j < x_max; j++)
                {
                    x.Add(new KarnoughComponent(default_value));
                }
                com.Add(x);
                
            }
            y_max++;
        }

        //xの要素数追加
        private void addX()
        {
            foreach(List<List<IKarnoughComponent>> y_list in valueLists)
            {
                foreach(List<IKarnoughComponent> x_list in y_list)
                {
                    x_list.Add(new KarnoughComponent(default_value));
                }
            }
            x_max++;
        }

        /// <summary>
        /// ブロックIDのIKarnoughComponentを取得する
        /// </summary>
        /// <param name="blockId">ブロックID</param>
        /// <returns>そのBlockIDを持つIKarnoughComopnent</returns>
        public List<IAxisKarnoughComponent> getBlockIDList(byte blockId)
        {
            List<IAxisKarnoughComponent> comlist = new List<IAxisKarnoughComponent>();

            int x = 0;
            int y = 0;
            int z = 0;
            foreach(List<List<IKarnoughComponent>> Zlst in this.valueLists)
            {
                x = 0;
                y = 0;
                foreach(List<IKarnoughComponent> Ylst in Zlst)
                {
                    x = 0;
                    //var lst = Ylst.Where(x => x.blockValue == blockId);
                    foreach(IKarnoughComponent Xlst in Ylst)
                    {
                        foreach (byte bID in Xlst.blockValue)
                        {
                            if (bID == blockId)
                            {
                                AxisKarnoughComponent tmp = new AxisKarnoughComponent(Xlst, x, y, z);
                                comlist.Add(tmp);
                            }
                        }
                        x++;
                    }
                    y++;
                }
                z++;
            }

            return comlist;
        }

    }

    /// <summary>
    /// 座標情報が追加されたKarnoughComponent
    /// </summary>
    public class AxisKarnoughComponent : KarnoughComponent,IAxisKarnoughComponent
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public AxisKarnoughComponent(IKarnoughComponent com, int x, int y, int z = 0)
        {
            values = com.values;
            blockValue = com.blockValue;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
