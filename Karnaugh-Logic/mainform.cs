using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void mainWindow_Load(object sender, EventArgs e)
        {
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
    }
}
