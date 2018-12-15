using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            IKarnoughMap map = new KarnoughMap();
            //map.valueNames.Add("value1");
            //map.valueNames.Add("value2");
            //map.valueNames.Add("value3");

            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    IKarnoughComponent comp;
                    if (j % 2 == 0)
                    {
                        comp = new KarnoughComponent(0, TruthValue.False);
                    }
                    else
                    {
                        comp = new KarnoughComponent(0, TruthValue.True);
                    }

                    map.setMapPoint(comp, j, i);
                }
            }
            karnaughCnt.testDraw(map);
        }
    }
}
