using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic.Interfaces;
using Newtonsoft.Json;

namespace Karnaugh_Logic
{
    class KarnoughEngine
    {
        public static IKarnoughMap deserializer(string json)
        {
            JsonKarnoughMap map = JsonConvert.DeserializeObject<JsonKarnoughMap>(json);
            IKarnoughMap convered_map = new KarnoughMap();

            int x = XvalueCount(map.values.Count());
            int y = YvalueCount(map.values.Count());

            convered_map.dimension = map.dim;
            convered_map.valueNames = map.values;

            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    convered_map.setMapPoint(new KarnoughComponent(0, TruthValue.False), i, j, 0);
                }
            }

            foreach(JsonKarnoughComponent i in map.map)
            {
                TruthValue val = convertTruthValue(i.value);
                convered_map.setMapPoint(new KarnoughComponent((byte)i.block_id, val), i.x, i.y,i.z);
            }

            return convered_map;
        }

        /// <summary>
        /// 変数数を与えるとカルノー図の列数を返す
        /// </summary>
        /// <param name="valueCount">変数数</param>
        /// <returns>列数</returns>
        public static int XvalueCount(int valueCount)
        {
            int x = 2;
            for(int i = 2; i < valueCount; i++)
            {
                if(i%2 == 0)
                {
                    x = x + 2;
                }
            }
            return x;
        }

        public static int YvalueCount(int valueCount)
        {
            int y = 2;
            for(int i = 2; i < valueCount; i++)
            {
                if(i%2 == 1)
                {
                    y = y + 2;
                }
            }
            return y;
        }

        public static TruthValue convertTruthValue(string val)
        {
            switch (val)
            {
                case "true":
                    return TruthValue.True;
                case "false":
                    return TruthValue.False;
                default:
                    return TruthValue.Null;
            }
        }
    }

    class JsonKarnoughMap
    {
        public List<string> values;
        public int dim;
        public List<JsonKarnoughComponent> map;
    }

    class JsonKarnoughComponent
    {
        public int x;
        public int y;
        public int z;
        public string value;
        public int block_id;
    }
}
