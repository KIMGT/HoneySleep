using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Diagnostics;
using ParsingGasulPDF.Models;
using ParsingGasulPDF.Services;

namespace ParsingGasulPDF
{
    public partial class frmParsingInfo : Form
    {
        public frmParsingInfo()
        {
            InitializeComponent();

            //초기화
            _cboCompy.DataSource = CommonService.get손보회사들();
            _cboCompy.DisplayMember = "회사명";


        }


        //한글추출
        public string ExtraPdfHangul(String Filename)
        {
            StringBuilder rtnSB = new StringBuilder(10000);
            /*
            string strText = string.Empty;
            PdfReader reader = new PdfReader((string)Filename);
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();


            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                String s = PdfTextExtractor.GetTextFromPage(reader, page, its);

                //윈도우한글기본형식 CP949(코드949)
                //PDF파일한글추출한 형식 Ascii 형식의 CP949를 byte[]형식
                //Ascii 형식의 CP949형식을 UTF8형식의 btye[]변경한다.
                //byte[]형식을 스트링으로변환한다.
                //byte[] bS = Encoding.Default.GetBytes(s);
                //byte[] sAsciiToUtf8S = ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, bS);
                //s = Encoding.UTF8.GetString(sAsciiToUtf8S);


                //s = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(s)));
                //s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                strText = strText + s;
            }
            reader.Close();
            return strText;
            */
            using (PdfReader reader = new PdfReader((string)Filename))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    rtnSB.Append(PdfTextExtractor.GetTextFromPage(reader, page));
                }
            }
            return rtnSB.ToString();

        }


        private void frmParsingInfo_Load(object sender, EventArgs e)
        {
        }

        private void btn가설파일선택_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                txtFileName.Text = file;
            }
        }

        private void btnView1_Click(object sender, EventArgs e)
        {
            //상품선택
            String insrCompany = ((DataRowView)_cboCompy.SelectedItem).Row[0].ToString();

            //가설파일
            String fileName = txtFileName.Text.Trim();
            if ("".Equals(fileName.ToString()))
            {
                MessageBox.Show("파일을 선택해주세요.");
                return;
            }
            String SRCCnt = ExtraPdfHangul(fileName);
            Form frm = new frmView(SRCCnt);
            frm.ShowDialog();
        }

        private void btnView2_Click(object sender, EventArgs e)
        {
            //상품선택
            String insrCompany = ((DataRowView)_cboCompy.SelectedItem).Row[0].ToString();

            if ("".Equals(insrCompany.ToString()))
            {
                MessageBox.Show("회사코드를 선택해주세요.");
                return;
            }

            //가설파일
            String fileName = txtFileName.Text.Trim();
            if ("".Equals(fileName.ToString()))
            {
                MessageBox.Show("파일을 선택해주세요.");
                return;
            }
            String SRCCnt = ExtraPdfHangul(fileName);

            int stPoint = 0;
            int edPoint = 0;
            String sEdString = "";
            String SRCCnt2 = "";

            stPoint = SRCCnt.IndexOf(ParsingService.get담보내용위치(insrCompany, ParsingService.담보내용위치구분.담보내용위치구분처음));
            sEdString = ParsingService.get담보내용위치(insrCompany, ParsingService.담보내용위치구분.담보내용위치구분마지막);
            edPoint = SRCCnt.IndexOf(sEdString);
            if (edPoint < 0)
            {
                SRCCnt2 = SRCCnt.Substring(stPoint + 5);
            }
            else
            {
                SRCCnt2 = SRCCnt.Substring(stPoint + 5, (edPoint - stPoint + 5));
            }
           
            Form frm = new frmView(SRCCnt2);
            frm.ShowDialog();
        }




        private void btn가설등록_Click(object sender, EventArgs e)
        {

        }
    }
}
