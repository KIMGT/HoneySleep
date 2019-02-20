using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParsingGasulPDF
{
    public partial class frmView : Form
    {
        public frmView()
        {
            InitializeComponent();
        }

        public frmView(String cnt)
        {
            InitializeComponent();
            rtView.Text = cnt;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtView_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmView_Load(object sender, EventArgs e)
        {

        }
    }
}
