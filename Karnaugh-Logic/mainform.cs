using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Karnaugh_Logic.Interfaces;

namespace Karnaugh_Logic
{
    public partial class mainWindow : Form
    {
        private KarnaughGraphControl karnaughCnt = null;

        public mainWindow()
        {
            InitializeComponent();
            PythonPathBox.Text = loadConfig();
            karnaughCnt = new KarnaughGraphControl();
            karnaughCnt.Width = this.mainSplitContainer.Panel2.Width;
            karnaughCnt.Height = this.mainSplitContainer.Panel2.Height;
            this.mainSplitContainer.Panel2.Controls.Add(karnaughCnt);
            statusLabel.Text = "準備完了";

        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            if(File.Exists(PythonPathBox.Text) == false)
            {
                MessageBox.Show("指定したPython実行環境が存在しません。","Pythonエラー");
                return;
            }
            statusLabel.Text = "簡略化中";

            KarnoughEngine eng = new KarnoughEngine();
            eng.python_env = PythonPathBox.Text;
            eng.script = @"jsontest.py";
            IKarnoughMap map = await Task.Run(()=> genMap(eng, LogicTexBox.Text));

            string exp = genExp(eng, map);
            afterExpBox.Text = exp;
            karnaughCnt.testDraw(map);

            statusLabel.Text = "簡略化完了";
        }

        private IKarnoughMap genMap(KarnoughEngine eng,string exp)
        {
            return eng.solve(exp);
        }

        private string genExp(KarnoughEngine eng,IKarnoughMap map)
        {
            IKarnoughLogic log = eng.getExp(map);
            return log.genLogicExpression();
        }

        //pythonの環境設定されたとき
        private void PythonPathButton_Click(object sender, EventArgs e)
        {
            if(PythonFileBrowser.ShowDialog() == DialogResult.OK)
            {
                PythonPathBox.Text = PythonFileBrowser.FileName;
                saveConfig();
            }
        }

        //メニューの終了ボタン
        private void Exit_item_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //設定ファイルの保存
        private void saveConfig()
        {
            //保存先のファイル名
            string fileName = @"config.xml";

            config obj = new config();
            obj.pythonpath = PythonPathBox.Text;


            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(config));
            //書き込むファイルを開く（UTF-8 BOM無し）
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fileName, false, new System.Text.UTF8Encoding(false));
            //シリアル化し、XMLファイルに保存する
            serializer.Serialize(sw, obj);
            //ファイルを閉じる
            sw.Close();
        }

        //設定ファイルの読み込み
        private string loadConfig()
        {
            string fileName = @"config.xml";

            if(File.Exists(fileName) == false)
            {
                return "";
            }

            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(config));
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));
            config obj = (config)serializer.Deserialize(sr);
            //ファイルを閉じる
            sr.Close();

            return obj.pythonpath;
        }
    }

    public class config
    {
        public string pythonpath;
    }
}
