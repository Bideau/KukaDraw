using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;
using System.IO;
using KukaDraw.Brain;
using KukaDraw.Com;
using KukaDraw.Core;

namespace KukaDraw.IHM
{
    public partial class OpenSVGForm : Form
    {
        private string pathFile;
        private SvgDocument svgFile;
        private SVGParser parser;
        private Interpretor interpretor;
        private ClientTcp myClient;
  
        public OpenSVGForm(ClientTcp client)
        {
            InitializeComponent();
            this.svgFile = new SvgDocument();
            this.parser = new SVGParser();
            this.interpretor = new Interpretor();
            this.myClient = client;
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Constants.initialDirectory;
            fileDialog.DefaultExt = Constants.defaultExt;
            fileDialog.Filter = Constants.filter;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = fileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            this.pathFile = fileDialog.FileName;
                            this.tbPathFile.Text = this.pathFile;
                            this.svgFile = SvgDocument.Open(pathFile);
                            this.pbFileSVG.Image = this.svgFile.Draw();
                            this.parser.SvgXmlReader(myStream);                        
                        }
                    }
                }
                catch (Exception ex)
                {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            this.parser.Parse();
            this.interpretor.interpretation(this.parser.GetDataList());
            this.interpretor.myOrders.giveOrders(this.myClient);
            this.interpretor.myOrders.numberOFOrders();
        }
    }
}
