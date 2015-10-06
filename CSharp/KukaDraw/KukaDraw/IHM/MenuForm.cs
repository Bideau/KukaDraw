using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using KukaDraw.Com;

namespace KukaDraw.IHM
{
    public partial class MenuForm : Form
    {
        private ClientTcp client;
        private bool connected = false;
        public MenuForm()
        {
            InitializeComponent();
            this.client = new ClientTcp();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                this.client.InitConfig(int.Parse(tbPort.Text), tbAddress.Text);
                this.client.Connect();
                Thread.Sleep(42);
                this.connected = this.client.getStatus();
                if (connected == true)
                {
                    this.bConnect.Text = "Deconnexion";
                    this.lStatus.Text = "Connecter à " + this.client.getConnectTo();
                    this.gbMenu.Visible = true;
                }              
            }
            else
            {
                this.client.Disconnect();
                this.connected = false;
                this.bConnect.Text = "Connexion";
                this.lStatus.Text = "Déconnecter ";
                this.gbMenu.Visible = false;
            }
        }

        private void bDrawSVG_Click(object sender, EventArgs e)
        {
            OpenSVGForm openSVG = new OpenSVGForm(this.client);
            openSVG.Show();
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            Painter painter = new Painter(this.client);
            painter.Show();
        }
        public ClientTcp getClient()
        {
            return this.client;
        }

        private void bConvert_Click(object sender, EventArgs e)
        {

        }
    }
}
