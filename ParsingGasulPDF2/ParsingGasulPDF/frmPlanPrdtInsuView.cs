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
    public partial class frmPlanPrdtInsuView : Form
    {
        public frmPlanPrdtInsuView()
        {
            InitializeComponent();

            _cbo플랜.DataSource = CommonService.get플랜();
            _cbo플랜.DisplayMember = "name";
            _cbo플랜.ValueMember = "cd";

            _cboPrdt.DataSource = CommonService.get상품들by플랜("170701"); //초기값가져오기
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void _cbo플랜_SelectedIndexChanged(object sender, EventArgs e)
        {

            _cboPrdt.DataSource = CommonService.get상품들by플랜(  ((DataRowView)_cbo플랜.SelectedItem).Row[0].ToString());
            _cboPrdt.DisplayMember = "name";
            _cboPrdt.ValueMember = "cd";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //상품선택
            String s플랜ID = (String)_cbo플랜.SelectedValue;
            String s상품 = ((DataRowView)_cboPrdt.SelectedItem).Row[0].ToString();   //회사(2자리) + 상품6자리
            String s성별 = ((KeyValuePair<String, String>)_cbo성별.SelectedItem).Key;
            String s회사코드 = "";
            String s상품코드 = "";
            String s연령 = txtAge.Text;

            Match m = Regex.Match(s상품, @"[a-zA-Z]+");
            {
                s회사코드 = m.Value;
            }

             m = Regex.Match(s상품, @"\d+");
            if (m.Success)
            {
                s상품코드 = m.Value;
            }




            int n연령 = 0;
            int n예정보험료연령 = 0;
            if (int.TryParse(s연령.ToString(), out n연령) == false)
            {
                MessageBox.Show("연령을 넣어주세요..");
                return;
            }

            if (s성별.Equals("X") && n연령 != 0)
            {
                MessageBox.Show("태아 연령은 0세입입니다..");
                return;
            }


            DataTable dt = CommonService.get플랜보험료비교(s플랜ID, s회사코드, s상품코드, s성별, n연령);
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

            int sum예정보험료 = 0;
            int sum보험료 = 0;
            int sum추출보험료 = 0;
            int sum플랜건수 = 0;
            int sum추출건수 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {

                if (String.IsNullOrEmpty((String)dataGridView1.Rows[i].Cells["예정"].Value) == false )
                {
                    if (dataGridView1.Rows[i].Cells["예정"].Value.ToString().Trim() !="")
                    {
                        int.TryParse(dataGridView1.Rows[i].Cells["예정"].Value.ToString().Trim(), out n예정보험료연령);

                        if (n연령 < n예정보험료연령)
                        {
                            sum예정보험료 += Convert.ToInt32(dataGridView1.Rows[i].Cells["플랜보험금"].Value);
                        }
                    }
                }
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells["플랜보험금"].Value) > 0)
                {
                    sum플랜건수 += 1;
                }
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells["추출보험료"].Value) > 0)
                {
                    sum추출건수 += 1;
                }
                sum보험료 += Convert.ToInt32(dataGridView1.Rows[i].Cells["플랜보험금"].Value);
                sum추출보험료 += Convert.ToInt32(dataGridView1.Rows[i].Cells["추출보험료"].Value);
            }

            String disp보험료 = "";
            if (sum예정보험료 > 0)
            {
                disp보험료 =  String.Format("플랜합계보험료-{0} : 추출합계보험료-{1} : 예정-{2} , 플랜건수 : {3} ,추출건수 :{4}",sum보험료.ToString("#,##0"), sum추출보험료.ToString("#,##0"), sum예정보험료.ToString("#,##0"), sum플랜건수, sum추출건수);
            }
            else
            {
                disp보험료 = String.Format("플랜합계보험료-{0} : 추출합계보험료-{1},플랜건수 : {2} ,추출건수 :{3} ", sum보험료.ToString("#,##0"), sum추출보험료.ToString("#,##0"), sum플랜건수, sum추출건수);
            }
            txtSum.Text = disp보험료;
        }
    }
}
