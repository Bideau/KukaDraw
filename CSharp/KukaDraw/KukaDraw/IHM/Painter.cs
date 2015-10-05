using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KukaDraw.IHM
{
    public partial class Painter : Form
    {
        bool paint = false;
        Graphics g;
        List<PointF> tabpointF;
        Pen pen;
        int? initX = null;
        int? initY = null;

        public Painter()
        {
            InitializeComponent();
            g = pPainter.CreateGraphics();
            
            tabpointF = new List<PointF>();
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            // envoyer le tableau à a order.
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            g.Clear(pPainter.BackColor);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            // sauvgarder l'image.
        }

        private void pPainter_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
        }

        private void pPainter_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            initX = null;
            initY = null;
        }

        private void pPainter_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {

                //Setting the Pen BackColor and line Width
                pen = new Pen(Color.Black, 1.0f);
                //Pen p = new Pen(btn_PenColor.BackColor, float.Parse(cmb_PenSize.Text));
                //Drawing the line.
                PointF p1 = new PointF(initX ?? e.X, initY ?? e.Y);
                tabpointF.Add(p1);
                PointF p2 = new PointF(e.X, e.Y);
                tabpointF.Add(p2);
                Console.WriteLine("p1 = " + p1 + " p2 " + p2);
                g.DrawLine(pen, p1, p2);
                initX = e.X;
                initY = e.Y;
                //tabpointF.Add(new PointF(e.X, e.Y));
            }
        }
    }
}
