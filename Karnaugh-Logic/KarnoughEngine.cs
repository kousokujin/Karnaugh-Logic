using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karnaugh_Logic.Interfaces;
using Newtonsoft.Json;

namespace Karnaugh_Logic
{
    public class KarnoughEngine
    {
        public static IKarnoughMap deserializer(string json)
        {
            JsonKarnoughMap map = JsonConvert.DeserializeObject<JsonKarnoughMap>(json);

            /*
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
            */

            foreach(JsonKarnoughComponent i in map.map)
            {
                map.setMapPoint(i, i.x, i.y,i.z);
            }


            return map;
        }
    }

    class JsonKarnoughMap : KarnoughMap
    {

        public List<string> values
        {
            set
            {
                valueNames = value;
            }
            get
            {
                return valueNames;
            }
        }

        public int dim
        {
            set
            {
                dimension = value;
            }
            get
            {
                return dimension;   
            }
        }
        public List<JsonKarnoughComponent> map;

        public JsonKarnoughMap()
        {
            default_value = TruthValue.False;
        }
    }

    class JsonKarnoughComponent : KarnoughComponent
    {
        public int x;
        public int y;
        public int z;
        public string value
        {
            set
            {
                switch (value)
                {
                    case ("true"):
                        values = TruthValue.True;
                        return;
                    case ("false"):
                        values = TruthValue.False;
                        return;
                    default:
                        values = TruthValue.Null;
                        return;
                }
            }
            get
            {
                switch (values)
                {
                    case (TruthValue.True):
                        return "true";
                    case (TruthValue.False):
                        return "false";
                    default:
                        return "Null";
                }
            }
        }
        public int block_id
        {
            set
            {
                blockValue = (byte)value;
            }
            get
            {
                return blockValue;
            }
        }
    }
}
