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
       
        
        public OpenSVGForm()
        {
            InitializeComponent();
            svgFile = new SvgDocument();
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
                            pathFile = fileDialog.FileName;
                            tbPathFile.Text = pathFile;
                            svgFile = SvgDocument.Open(pathFile);
                            pbFileSVG.Image = svgFile.Draw();
                            //Console.WriteLine(svgFile.GetSvgDocument().GetXML());
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }
    }
}
