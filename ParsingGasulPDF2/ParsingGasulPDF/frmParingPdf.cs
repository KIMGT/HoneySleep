using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ParsingGasulPDF.Services;

namespace ParsingGasulPDF
{
    public partial class frmParingPdf : Form
    {
        public frmParingPdf()
        {
            InitializeComponent();

            //상품설정
            //초기화

            //상품구분
            Dictionary<String, String> comboSource3 = new Dictionary<String, String>();
            comboSource3.Add("01", "종합");
            comboSource3.Add("02", "3대질병");
            comboSource3.Add("03", "어린이100세");
            comboSource3.Add("04", "어린이30세");
            comboSource3.Add("09", "어린이무해지");
            comboSource3.Add("05", "간편");
            comboSource3.Add("07", "실손");
            comboSource3.Add("08", "유병자실손");

            _cbo상품구분.DisplayMember = "Value";
            _cbo상품구분.ValueMember = "Key";
            _cbo상품구분.DataSource = new BindingSource(comboSource3, null);


            //상품
            _cboPrdt.DataSource = CommonService.get상품들(((KeyValuePair<String, String>)_cbo상품구분.SelectedItem).Key);
            _cboPrdt.DisplayMember = "name";
            _cboPrdt.ValueMember = "cd";


            //성별설정
            Dictionary<String, String> comboSource = new Dictionary<String,String>();
            comboSource.Add("M", "남자");
            comboSource.Add("F", "여자");
            comboSource.Add("X", "태아");

            _cbo성별.DisplayMember = "Value";
            _cbo성별.ValueMember = "Key";
            _cbo성별.DataSource = new BindingSource(comboSource, null);

            //파싱구분
            Dictionary<String, String> comboSource2 = new Dictionary<String, String>();
            comboSource2.Add("전체삭제", "전체담보");
            comboSource2.Add("부분삭제", "부분담보");
            _cboInputGB.DisplayMember = "Value";
            _cboInputGB.ValueMember = "Key";
            _cboInputGB.DataSource = new BindingSource(comboSource2, null);
        }

        private void btn가설파일선택_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    txtDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void _cbo성별_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            //상품선택
            String s상품 = ((DataRowView)_cboPrdt.SelectedItem).Row[0].ToString();   //회사(2자리) + 상품6자리 (상품7자리)
            String s성별 = ((KeyValuePair<String,String>)_cbo성별.SelectedItem).Key;
            String s삭제구분 = ((KeyValuePair<String, String>)_cboInputGB.SelectedItem).Key;
            String sPattern = s상품 + s성별 + "*.pdf";
            String s디렉토리 = txtDir.Text;
            String s회사코드 = s상품.Substring(0, 2);
            String s상품코드  = s상품.Substring(2, s상품.Length - 2);
            
            int n연령 = 0;
            ParsingService parsingService = new ParsingService();


            if (!string.IsNullOrWhiteSpace(s디렉토리)){
                string[] files = Directory.GetFiles(s디렉토리, sPattern);

                if (files.Length <=0)
                {
                    MessageBox.Show("가설파일이 없습니다.");
                    return;
                }

                //등록

                _rhRtn.Text = "";
                StringBuilder sb = new StringBuilder(5000);
                
                foreach (String file in files)
                {
                    n연령 = int.Parse(file.Substring(file.Length - 6, 2));
                    int stPoint = 0;
                    int edPoint = 0;
                    String SRCCnt = ParsingService.ExtraPdfHangul(file);
                    stPoint = SRCCnt.IndexOf(ParsingService.get담보내용위치(s회사코드, ParsingService.담보내용위치구분.담보내용위치구분처음));
                    edPoint = SRCCnt.IndexOf(ParsingService.get담보내용위치(s회사코드, ParsingService.담보내용위치구분.담보내용위치구분마지막));
                    if (edPoint == -1) edPoint = SRCCnt.Length - 10;
                   String SRCCnt2 = SRCCnt.Substring(stPoint + 5, (edPoint - stPoint + 5));
                    DataTable dt = CommonService.get상품파싱정보(s회사코드, s상품코드, s성별, n연령, s삭제구분);
                    if (s회사코드.Equals("HA"))     //한화
                    {
                        sb.Append(parsingService.한화손보(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("ME"))     //메리츠
                    {
                        sb.Append(parsingService.메리츠화재(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("LO"))     //롯데
                    {
                        sb.Append(parsingService.롯데손보(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("KB"))     //KB
                    {
                        sb.Append(parsingService.KB손보(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("HY"))     //현대해상
                    {
                        sb.Append(parsingService.현대해상(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("DO"))     //동부화재
                    {
                        sb.Append(parsingService.동부화재(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("HE"))     //흥국화재
                    {
                        sb.Append(parsingService.흥국화재(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("MG"))     //MG손해
                    {
                        sb.Append(parsingService.MG손해(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                    else if (s회사코드.Equals("NH"))     //NH손해
                    {
                        sb.Append(parsingService.NH손해(s성별, n연령, dt, SRCCnt2, s삭제구분));
                    }
                }
                _rhRtn.Text = sb.ToString();
            }
        }

        private void _rhRtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cboPrdt.DataSource = CommonService.get상품들(((KeyValuePair<String, String>)_cbo상품구분.SelectedItem).Key);
            _cboPrdt.DisplayMember = "name";
            _cboPrdt.ValueMember = "cd";
        }

        private void frmParingPdf_Load(object sender, EventArgs e)
        {

        }
    }
}
