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
    public partial class frmPrdtView : Form
    {
        public frmPrdtView()
        {
            InitializeComponent();
            //상품설정
            //초기화
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


            _cbo상품구분.DisplayMember = "Value";
            _cbo상품구분.ValueMember = "Key";
            _cbo상품구분.DataSource = new BindingSource(comboSource3, null);

            _cboPrdt.DataSource = CommonService.get상품들(((KeyValuePair<String, String>)_cbo상품구분.SelectedItem).Key);
            _cboPrdt.DisplayMember = "name";
            _cboPrdt.ValueMember = "cd";

            //성별설정
            Dictionary<String, String> comboSource = new Dictionary<String, String>();
            comboSource.Add("M", "남자");
            comboSource.Add("F", "여자");
            comboSource.Add("X", "태아");

            _cbo성별.DisplayMember = "Value";
            _cbo성별.ValueMember = "Key";
            _cbo성별.DataSource = new BindingSource(comboSource, null);
            txtAge.Text = "40";


        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            //상품선택
            String s상품 = ((DataRowView)_cboPrdt.SelectedItem).Row[0].ToString();   //회사(2자리) + 상품6자리
            String s성별 = ((KeyValuePair<String, String>)_cbo성별.SelectedItem).Key;
            String s회사코드 = s상품.Substring(0, 2);
            String s상품코드 = s상품.Substring(2, s상품.Length -2);
            String s연령 = txtAge.Text;

            int n연령 = 0;
            if (int.TryParse(s연령.ToString(), out n연령) == false ){
                MessageBox.Show("연령을 넣어주세요..");
                return;
            }

            if (s성별.Equals("X")  && n연령 != 0)
            {
                MessageBox.Show("태아 연령은 0세입입니다..");
                return;
            }


            DataTable dt = CommonService.get추출담보정보(s회사코드, s상품코드, s성별, n연령);
            dataGridView1.DataSource = dt;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dataGridView1.Columns[i].HeaderText == "seq")
                {
                    dataGridView1.Columns[i].ReadOnly = false;
                }
                else
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                    
                dataGridView1.Columns[i].Width = 65;
            }

            int sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }

            txtSum.Text = sum.ToString("#,##0");



        }

        private void _cbo상품구분_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cboPrdt.DataSource = CommonService.get상품들(((KeyValuePair<String, String>)_cbo상품구분.SelectedItem).Key);
            _cboPrdt.DisplayMember = "name";
            _cboPrdt.ValueMember = "cd";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtSum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void _cbo성별_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void _cboPrdt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
