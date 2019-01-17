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
        /// カルノー図から論理式を生成
        /// </summary>
        /// <param name="map">カルノー図</param>
        /// <returns>論理式</returns>
        public string getExpress(IKarnoughMap map)
        {
            IKarnoughLogic logi = getExp(map);
            return logi.genLogicExpression();
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

        public IKarnoughLogic getExp(IKarnoughMap map)
        {
            IKarnoughLogic outputVal = new KarnoughLogic();
            outputVal.valueNames = map.valueNames;

            //List<List<bool>> valuelists = new List<List<bool>>();

            //とりあえずBlockIDが10まで
            for(byte i = 1; i < 10; i++)
            {
                List<List<bool>> valuelists = new List<List<bool>>();
                List<IAxisKarnoughComponent> com = map.getBlockIDList(i);
                List<TruthValue> truthval = new List<TruthValue>(); ;
                if (com.Count == 0)
                {
                    break;
                }

                foreach(IAxisKarnoughComponent c in com)
                {
                    List<bool> booleanLST = axisX(map.valueNames.Count(), c.x);
                    booleanLST.AddRange(axisY(map.valueNames.Count(), c.y));
                    valuelists.Add(booleanLST);
                }
                
                for(int j = 0; j < map.valueNames.Count(); j++)
                {
                    List<bool> Xlist = new List<bool>();
                    foreach(List<bool> lst in valuelists)
                    {
                        Xlist.Add(lst[j]);
                    }

                    bool prevLogic = Xlist[0];
                    TruthValue result;
                    if(prevLogic == true)
                    {
                        result = TruthValue.True;
                    }
                    else
                    {
                        result = TruthValue.False;
                    }
                    foreach(bool b in Xlist)
                    {
                        if(prevLogic != b)
                        {
                            result = TruthValue.Null;
                            break;
                        }
                    }
                    truthval.Add(result);
                }
                outputVal.values.Add(truthval);
            }

            //未定義の部分(blockid=0)
            List<IAxisKarnoughComponent> nodefinedBool = map.getBlockIDList(0);
            foreach (IAxisKarnoughComponent c in nodefinedBool)
            {
                if (c.values == TruthValue.True)
                {
                    List<bool> valList = axisX(map.valueNames.Count(), c.x);
                    valList.AddRange(axisY(map.valueNames.Count(), c.y));
                    List<TruthValue> val = new List<TruthValue>();
                    foreach (bool b in valList)
                    {
                        if (b == true)
                        {
                            val.Add(TruthValue.True);
                        }
                        else
                        {
                            val.Add(TruthValue.False);
                        }
                    }
                    outputVal.values.Add(val);
                }
            }

            return outputVal;
        }

        private List<bool> axisX(int valueCount,int point)
        {
            switch (valueCount)
            {
                case 2:
                    switch (point)
                    {
                        case 0:
                            return new List<bool> { false };
                        case 1:
                            return new List<bool> { true };
                        default:
                            return new List<bool>();
                    }
                case 3:
                case 4:
                    switch (point)
                    {
                        case 0:
                            return new List<bool> { false, false };
                        case 1:
                            return new List<bool> { false, true };
                        case 2:
                            return new List<bool> { true, true };
                        case 3:
                            return new List<bool> { true, false };
                        default:
                            return new List<bool>();
                    }
                case 5:
                case 6:
                    switch (point)
                    {
                        case 0:
                            return new List<bool> { false, false, false };
                        case 1:
                            return new List<bool> { false, false, true };
                        case 2:
                            return new List<bool> { false, true, true };
                        case 3:
                            return new List<bool> { false, true, false };
                        case 4:
                            return new List<bool> { true, true, false };
                        case 5:
                            return new List<bool> { true, true, true };
                        case 6:
                            return new List<bool> { true, false, true };
                        case 7:
                            return new List<bool> { true, false, false };
                        default:
                            return new List<bool>();
                    }
                default:
                    return new List<bool>();
            }
        }

        private List<bool> axisY(int valueCount,int point)
        {
            switch (valueCount)
            {
                case 2:
                case 3:
                    switch (point)
                    {
                        case 0:
                            return new List<bool> { false };
                        case 1:
                            return new List<bool> { true };
                        default:
                            return new List<bool>();
                    }
                case 4:
                case 5:
                    switch (point)
                    {
                        case 0:
                            return new List<bool> { false, false };
                        case 1:
                            return new List<bool> { false, true };
                        case 2:
                            return new List<bool> { true, true };
                        case 3:
                            return new List<bool> { true, false };
                        default:
                            return new List<bool>();
                    }
                case 6:
                    switch (point)
                    {
                        case 0:
                            return new List<bool> { false, false, false };
                        case 1:
                            return new List<bool> { false, false, true };
                        case 2:
                            return new List<bool> { false, true, true };
                        case 3:
                            return new List<bool> { false, true, false };
                        case 4:
                            return new List<bool> { true, true, false };
                        case 5:
                            return new List<bool> { true, true, true };
                        case 6:
                            return new List<bool> { true, false, true };
                        case 7:
                            return new List<bool> { true, false, false };
                        default:
                            return new List<bool>();
                    }
                default:
                    return new List<bool>();
            }
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
