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

/*
 * @author : Aubert Christophe
 * @date : 29/09/2015
 */

namespace KukaDraw.IHM
{
    public partial class OpenSVGForm : Form
    {
        private string pathFile;
        private SvgDocument svgFile;
        SVGParser parser = new SVGParser();
       
        
        public OpenSVGForm()
        {
            InitializeComponent();
            this.svgFile = new SvgDocument();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.DefaultExt = ".svg";
            fileDialog.Filter = "SVG document (.svg) |* .svg";
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
        }
    }
}
