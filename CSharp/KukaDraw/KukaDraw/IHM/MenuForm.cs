using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            client = new ClientTcp();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            if (connected == false)
            {
                this.client.InitConfig(int.Parse(tbPort.Text), tbAddress.Text);
                this.client.Connect();
                this.connected = true;
                this.bConnect.Text = "Deconnexion";
                this.lStatus.Text = "Connecter à " + this.client.getConnectTo();
                
            }
            else
            {
                this.client.Disconnect();
                this.connected = false;
                this.bConnect.Text = "Connexion";
                this.lStatus.Text = "Déconnecter ";
            }

        }

        private void bDrawSVG_Click(object sender, EventArgs e)
        {
            OpenSVGForm openSVG = new OpenSVGForm();
            openSVG.Show();
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            Painter painter = new Painter();
            painter.Show();
        }
    }
}
