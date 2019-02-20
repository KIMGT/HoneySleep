using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParsingGasulPDF.Services;

namespace ParsingGasulPDF
{
    public partial class frmPrdtAgeView : Form
    {
        public frmPrdtAgeView()
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


            String s상품 = ((DataRowView)_cboPrdt.SelectedItem).Row[0].ToString();   //회사(2자리) + 상품6자리
            String s회사코드 = "";
            String s상품코드 = "";

            Match m = Regex.Match(s상품, @"[a-zA-Z]+");
            {
                s회사코드 = m.Value;
            }

            m = Regex.Match(s상품, @"\d+");
            if (m.Success)
            {
                s상품코드 = m.Value;
            }


            _cboInsu.DataSource = CommonService.get담보들(s회사코드, s상품코드);
            _cboInsu.DisplayMember = "insur_nm";
            _cboInsu.ValueMember = "insur_cd";

            //성별설정
            Dictionary<String, String> comboSource = new Dictionary<String, String>();
            comboSource.Add("M", "남자");
            comboSource.Add("F", "여자");
            comboSource.Add("X", "태아");

            _cbo성별.DisplayMember = "Value";
            _cbo성별.ValueMember = "Key";
            _cbo성별.DataSource = new BindingSource(comboSource, null);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //상품선택
            String s성별 = ((KeyValuePair<String, String>)_cbo성별.SelectedItem).Key;
            String s상품 = ((DataRowView)_cboPrdt.SelectedItem).Row[0].ToString();   //회사(2자리) + 상품6자리
            String s회사코드 = "";
            String s상품코드 = "";
            String s담보코드 = ((DataRowView)_cboInsu.SelectedItem).Row[0].ToString();   //담보코드

            Match m = Regex.Match(s상품, @"[a-zA-Z]+");
            {
                s회사코드 = m.Value;
            }

            m = Regex.Match(s상품, @"\d+");
            if (m.Success)
            {
                s상품코드 = m.Value;
            }




            DataTable dt = CommonService.get연령별추출담보정보(s회사코드, s상품코드, s담보코드, s성별);
            dataGridView1.DataSource = dt;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[i].Width = 65;
            }

        }

        private void _cbo상품구분_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cboPrdt.DataSource = CommonService.get상품들(((KeyValuePair<String, String>)_cbo상품구분.SelectedItem).Key);
            _cboPrdt.DisplayMember = "name";
            _cboPrdt.ValueMember = "cd";
        }

        private void _cboPrdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            String s상품 = ((DataRowView)_cboPrdt.SelectedItem).Row[0].ToString();   //회사(2자리) + 상품6자리
            String s회사코드 = "";
            String s상품코드 = "";

            Match m = Regex.Match(s상품, @"[a-zA-Z]+");
            {
                s회사코드 = m.Value;
            }

            m = Regex.Match(s상품, @"\d+");
            if (m.Success)
            {
                s상품코드 = m.Value;
            }


            _cboInsu.DataSource = CommonService.get담보들(s회사코드, s상품코드);
            _cboInsu.DisplayMember = "insur_nm";
            _cboInsu.ValueMember = "insur_cd";
        }
    }
}
