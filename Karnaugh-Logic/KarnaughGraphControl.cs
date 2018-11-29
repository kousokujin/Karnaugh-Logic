using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Karnaugh_Logic
{
    /// <summary>
    /// カルノー図の描画コントロール
    /// </summary>
    public partial class KarnaughGraphControl : UserControl
    {
        private KarnaughGraphDraw drawer = null;

        public KarnaughGraphControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            drawer = new KarnaughGraphDraw(this);
        }

        private void KarnaughGraphControl_Paint(object sender, PaintEventArgs e)
        {
            System.Console.WriteLine("drawbefore");
            //drawer = new KarnaughGraphDraw(this);
            drawer.Paint();
        }

        public void testDraw()
        {
            drawer.Value2Map();
        }
    }
}
