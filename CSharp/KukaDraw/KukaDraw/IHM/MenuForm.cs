using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * @author : Aubert Christophe
 * @date : 29/09/2015
 */

namespace KukaDraw.IHM
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {

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
