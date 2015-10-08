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
    public partial class Painter : Form
    {
        private bool paint = false;
        private Graphics g;
        private List<PointF> tabpointF;
        private Pen pen;
        private ClientTcp myClient;
        private Orders myOrder;
        private int? initX = null;
        private int? initY = null;

        //debug
        private Log debug;

        public Painter(ClientTcp client)
        {
            InitializeComponent();
            this.g = this.pPainter.CreateGraphics();
            this.tabpointF = new List<PointF>();
            this.myOrder = new Orders();
            this.myClient = client;
            debug = new Log(Constants.logPainter);
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            this.myOrder.giveOrders(this.myClient);
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            this.g.Clear(this.pPainter.BackColor);
            this.tabpointF.Clear();
        }

        private void pPainter_MouseDown(object sender, MouseEventArgs e)
        {
            this.paint = true;
        }

        private void pPainter_MouseUp(object sender, MouseEventArgs e)
        {
            this.paint = false;
            this.initX = null;
            this.initY = null;
            scaleTabPointF();
            this.myOrder.addOrder(this.tabpointF);
            this.debug.writeLog(this.tabpointF);
        }

        private void pPainter_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.paint)
            {
                //Setting the Pen BackColor and line Width
                this.pen = new Pen(Color.Black, Constants.brushSize);

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
                tmptabpointF.Add(new PointF((pointF.X / Constants.scalling) + Constants.marginX, ((Constants.canvasDwY - pointF.Y) / Constants.scalling) + Constants.marginY));
            }
            this.tabpointF = tmptabpointF;
            //showListePointF();

            
        }

        //Fonction de debug qui affiche les valeur du tabpointF
        public void showListePointF()
        {
            foreach (PointF pointF in this.tabpointF)
            {
                Console.WriteLine("x : {0} y : {1}", pointF.X, pointF.Y);
            }
        }
    }
}
