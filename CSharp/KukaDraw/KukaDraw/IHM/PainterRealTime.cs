using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KukaDraw.Brain;
using KukaDraw.Com;
using KukaDraw.Core;

namespace KukaDraw.IHM
{
    public partial class PainterRealTime : Form
    {
        private bool paint = false;
        private Graphics g;
        private List<PointF> tabpointF;
        private Pen pen;
        private ClientTcp myClient;
        private Orders myOrder;
        private int? initX = null;
        private int? initY = null;
        private Log toto = null;

        public PainterRealTime(ClientTcp client)
        {
            InitializeComponent();
            this.g = this.pPainter.CreateGraphics();
            this.tabpointF = new List<PointF>();
            this.myOrder = new Orders();
            this.myClient = client;
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            this.g.Clear(this.pPainter.BackColor);
            this.tabpointF.Clear();
        }

        private void pPainter_MouseDown(object sender, MouseEventArgs e)
        {
            this.paint = true;
            this.initX = null;
            this.initY = null;
        }

        private void pPainter_MouseUp(object sender, MouseEventArgs e)
        {
            this.paint = false;
            this.initX = null;
            this.initY = null;
            scaleTabPointF();
            this.myOrder.addOrder(this.tabpointF);
            this.myOrder.giveOrders(this.myClient);
            this.toto = new Log(this.tabpointF); //debug
            this.tabpointF.Clear();
        }

        private void pPainter_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.paint)
            {
                //Setting the Pen BackColor and line Width
                this.pen = new Pen(Color.Black, 1.0f);

                //Drawing the line.
                PointF p1 = new PointF(initX ?? e.X, initY ?? e.Y);
                PointF p2 = new PointF(e.X, e.Y);
                this.g.DrawLine(pen, p1, p2);

                //Adding point to List<PointF>
                this.tabpointF.Add(p1);
                this.tabpointF.Add(p2);

                //change Init
                this.initX = e.X;
                this.initY = e.Y;
            }
        }

        //fonction de scalling de l'ecrant sur la feuille du kuka
        private void scaleTabPointF()
        {
            List<PointF> tmptabpointF = new List<PointF>();

            foreach (PointF pointF in this.tabpointF)
            {
                //invertion du y pour coller au repere de la feuille.
                tmptabpointF.Add(new PointF((pointF.X / 4), (((840 - pointF.Y) / 4))));
            }
            this.tabpointF = tmptabpointF;
            //showListePointF();


        }
    }
}
