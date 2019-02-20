using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParsingGasulPDF.Services;

namespace ParsingGasulPDF
{
    public partial class frmPrdtCD : Form
    {
        public frmPrdtCD()
        {
            InitializeComponent();

            //초기화
            _cboCompy.DataSource = CommonService.get손보회사들();
            _cboCompy.DisplayMember = "회사명";

            //상품구분
            Dictionary<String, String> comboSource3 = new Dictionary<String, String>();
            comboSource3.Add("01", "실손종합");
            comboSource3.Add("02", "3대질병_저해지");
            comboSource3.Add("03", "어린이100세");
            comboSource3.Add("04", "어린이30세");
            comboSource3.Add("09", "어린이_저해지");
            comboSource3.Add("05", "간편");
            comboSource3.Add("07", "실손");
            comboSource3.Add("08", "유병자실손");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
