using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Karnaugh_Logic.Interfaces;
using Newtonsoft.Json;

namespace Karnaugh_Logic
{
    public class KarnoughEngine
    {
        public string python_env;
        public string script;

        public KarnoughEngine()
        {
            script = "test.py";
        }

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

        /// <summary>
        /// 論理式からIkarnoughMapを作成
        /// </summary>
        /// <param name="logicExp">論理式</param>
        /// <returns>map</returns>
        public IKarnoughMap solve(string logicExp)
        {
            string json = SolveMap(logicExp);
            IKarnoughMap map = deserializer(json);

            return map;
        }

        /// <summary>
        /// 論理式を入力してJSONのMAPを出力
        /// </summary>
        /// <param name="logicExp">論理式</param>
        /// <returns>JSONのマップ</returns>
        private string SolveMap(string logicExp)
        {
            List<string> args = new List<string>();
            args.Add(logicExp);

            string json = runPython(script, args);

            return json;
        }

        /// <summary>
        /// 指定されたPythonスクリプトを実行。python_envで仮想環境を指定
        /// </summary>
        /// <param name="script">スクリプト</param>
        /// <param name="args">スクリプト引数</param>
        /// <returns>実行結果(標準出力)</returns>
        private string runPython(string script,List<string> args)
        {
            ProcessStartInfo psi = new ProcessStartInfo(python_env);
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            string arg = script;

            foreach(string s in args)
            {
                arg += (" " + s);
            }
            arg.TrimEnd();
            psi.Arguments = arg;

            Process pro = new Process();
            pro.StartInfo = psi;
            pro.Start();

            StreamReader sr = pro.StandardOutput;
            string outputStr = sr.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            return outputStr;
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
        
        //継承元のmapにも適用させたくね？
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
