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
            karnaughCnt = new KarnaughGraphControl();
            karnaughCnt.Width = this.mainSplitContainer.Panel2.Width;
            karnaughCnt.Height = this.mainSplitContainer.Panel2.Height;
            this.mainSplitContainer.Panel2.Controls.Add(karnaughCnt);

        }

        private void mainWindow_Paint(object sender, PaintEventArgs e)
        {
            //device.Clear(ClearFlags.Target, Color.FromArgb(0x333333), 1.0f, 0);
            //device.BeginScene();
            //device.EndScene();
            //device.Present();
        }

        private void mainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
           // if (device != null)
            //{
            //    device.Dispose();
            //    device = null;
           //}
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if(File.Exists(PythonPathBox.Text) == false)
            {
                MessageBox.Show("指定したPython実行環境が存在しません。","Pythonエラー");
                return;
            }

            KarnoughEngine eng = new KarnoughEngine();
            eng.python_env = PythonPathBox.Text;
            eng.script = "jsontest.py";
            IKarnoughMap map = genMap(eng, LogicTexBox.Text);
            string exp = genExp(eng, map);
            afterExpBox.Text = exp;
            karnaughCnt.testDraw(map);
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

        private void PythonPathButton_Click(object sender, EventArgs e)
        {
            if(PythonFileBrowser.ShowDialog() == DialogResult.OK)
            {
                PythonPathBox.Text = PythonFileBrowser.FileName;
            }
        }
    }
}
