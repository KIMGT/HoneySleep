using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Data.SqlClient;
using ParsingGasulPDF.Models;
using System.Diagnostics;

namespace ParsingGasulPDF.Services
{
    class ParsingService
    {
        public  enum 담보내용위치구분
        {
             담보내용위치구분처음,
             담보내용위치구분마지막
        }


        public static String get담보내용위치(String compyCode, 담보내용위치구분  p구분)
        {
            string rtn = "";
            switch (compyCode)
            {
                case "HY":
                case "0171":  //현대해상
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "위험보장" : "상품별 보험료 할인 ";
                    break;
                case "KB":  //KB손보
                case "0172":  //KB손보
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ?  "가입내용" : "주의사항";
                    break;
                case "DO":  //동부
                case "0173":  //동부
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "가입담보" : "예상해지환급금";
                    break;
                case "HA":  //한화손보
                case "0174":  //한화손보
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "가입담보리스트" : "보험료 합계";
                    break;
                case "ME": //메리츠
                case "0175": //메리츠
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "가입담보리스트" : "보장 합계보험료";
                    break;
                case "LO": //롯데손보
                case "0176": //롯데손보
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "보장사항" : "보장보험료합계";
                    break;
                case "MG": //MG손해보험
                case "0177": //MG손해보험
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "보 장 명" : "납입면제에 관한 ";
                    break;
                case "HE": //흥국화재
                case "0178": //흥국화재
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "보장사항" : "보 장 보 험 료";
                    break;
                case "NH": //NH손보
                case "0184": //NH손보
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "보장내역(기본" : "제도성특약";
                    break;
                case "SA": //삼성화재
                case "0170": //삼성화재
                    rtn = (p구분 == 담보내용위치구분.담보내용위치구분처음) ? "보장내역(기본" : "보장보험료 합계";
                    break;

                default:
                    break;
            }
            return rtn;
        }


        //한글추출
        public static string ExtraPdfHangul(String Filename)
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

        #region 회사별 Parsing

        public string 한화손보(String sex , int age ,DataTable stdPrtd, string srcPrdt ,String b삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");

                Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(",@"\(").Replace(@")", @"\)").Replace(@"/", @"\/")) ;  
                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        PrdtPriceInfo datarow = getContractDetail한화(sex, age,dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, b삭제구분);
            }
            


            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetail한화(String sex, int age ,DataRow dr,  String strChunk,ref StringBuilder rtnStr)
        {
            
            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;

            String parsingType = dr.Field<String>("parsing_type");
            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);

            if (parsingType == "패턴1")
            {

            }
            //기준가입금액
            Match m = Regex.Match(strChunk, @"\s[0-9,]+만원");
            if (m.Success)
            {
                s가입금액 = m.Value.Replace("만원", "").Replace(",","");
                prdtPriceInfo.std_contract_amt = int.Parse(s가입금액);
            } else
            {
                rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
            }
            //보험료
            m = Regex.Match(strChunk, @"\s[0-9,]+원");
            if (m.Success)
            {
                s보험료 = m.Value.Replace("원", "").Replace(",", "");
                prdtPriceInfo.premium = int.Parse(s보험료);
            }
            else
            {
                rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 보험료 오류"));
            }

            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";

            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"\s\d+(세|년)만기\s");
                if (m.Success)
                {

                    prdtPriceInfo.expiration = m.Value.Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도 오류"));
                }

                //납입기간
                m = Regex.Match(strChunk, @"\s(\d+세 |\d+년|전기|일시)납\s");
                if (m.Success)
                {
                    prdtPriceInfo.pay_term =  m.Value.Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 납입기간 오류"));
                }
            }         

            return prdtPriceInfo;
        }


        /*
         ** 패턴1 종료위치 보험기간 / 납입기간  가입금액 보험료  OR 갱신형 :  가입금액 보험료
         ** 패턴2 숫자 가입금액 보험료 로 구성되어 있는경우 (갱신형)
         ** 패턴3 가입금액 보험료로 구성(갱신형)
         ** 패턴4 가입금액 보험료로 구성(갱신형) 가입금액 한글
         */
        public string 메리츠화재(String sex, int age, DataTable stdPrtd, string srcPrdt,String  s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                //if (s담보명.IndexOf("특정암진단비") >= 0) Debugger.Break();
                Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));
                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        PrdtPriceInfo datarow = getContractDetail메리츠화재(sex, age, dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds ,s삭제구분);
            }



            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetail메리츠화재(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {

            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료 = string.Empty;

            String parsingType = dr.Field<String>("parsing_type");

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);

            //
            String s종료위치 = dr.Field<String>("ed_string");
            Match m = Regex.Match(strChunk, @s종료위치);
            if (m.Success)
            {
                s가입금액보험료 = m.Value;
            }
            else
            {
                rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 종료위치 오류"));
            }



            //기준가입금액 // 보험료
            if (parsingType == "패턴2"  || parsingType == "패턴3" || parsingType == "패턴4")
            {
                if (parsingType == "패턴2")
                    m = Regex.Match(strChunk, @"\s[0-9,]+\s[0-9,]+\s[0-9,]+");
                else if (parsingType == "패턴3")
                    m = Regex.Match(strChunk, @"\s[0-9,]+\s[0-9,]+\s");
                else
                    m = Regex.Match(strChunk, @"[0-9억천백만]+원\s[0-9,]+");

                if (m.Success)
                {
                    String[] tmp = m.Value.Trim().Replace(",", "").Split(' ');
                    if (tmp.Length > 0)
                    {
                        if (parsingType == "패턴2")
                        {
                            prdtPriceInfo.std_contract_amt = int.Parse(tmp[1]); //가입금액
                            prdtPriceInfo.premium = int.Parse(tmp[2]);     //보험료

                        }
                        else if (parsingType == "패턴3")
                        {
                            prdtPriceInfo.std_contract_amt = int.Parse(tmp[0]); //가입금액
                            prdtPriceInfo.premium = int.Parse(tmp[1]);     //보험료
                        }
                        else
                        {
                            //패턴 4
                            prdtPriceInfo.std_contract_amt = convertHangulToNumber(tmp[0]); //가입금액
                            prdtPriceInfo.premium = int.Parse(tmp[1]);     //보험료
                        }

                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액/보험료 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액/보험료 오류"));
                }

            }
            else
            {
                //일반적인경우  패턴1 
                m = Regex.Match(s가입금액보험료, @"[0-9,]+\s[0-9,]+");
                if (m.Success)
                {
                    String[] tmp = m.Value.Replace(",", "").Split(' ');
                    if (tmp.Length == 2)
                    {
                        prdtPriceInfo.std_contract_amt = int.Parse(tmp[0]); //가입금액
                        prdtPriceInfo.premium = int.Parse(tmp[1]);     //보험료
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액/보험료 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액/보험료 오류"));
                }
            }
            


            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"\/\s\d+(세|년)");
                if (m.Success)
                {
                    prdtPriceInfo.expiration  = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 납입기간 오류"));
                }

                //납입기간
                m = Regex.Match(strChunk, @"\s\d+(세|년)\s\/");
                if (m.Success)
                {

                    prdtPriceInfo.pay_term = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도 오류"));
                }

            }

            return prdtPriceInfo;
        }

        /*
         * 패턴 1 : 가입금액위치와 보험료위치가 있는경우
         * 패턴 2 : 가입금액과 보험료가 같이 있을경우
         * 패턴 3 : 보험료 합산  - 도수,주사료,MRI
         * */
        public string 롯데손보(String sex, int age, DataTable stdPrtd, string srcPrdt,String  s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = dr.Field<String>("st_string");
                //s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                //if (s담보명.IndexOf("기타피부유사암진단비") >= 0) Debugger.Break();
                Match m = Regex.Match(srcPrdt, @s시작위치);   //시작위치를 변환하지 않는다.
                //Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);



                        //예외문자
                        String 예외문자 = dr.Field<String>("except_string");
                        bool 담보파싱 = true;
                        if (예외문자.Trim().Length > 0)
                        {
                            if (strChunk.IndexOf(예외문자) >= 0) 담보파싱 = false;
                        }

                        //삭제문자
                        String 삭제문자 = dr.Field<String>("del_string");
                        if (삭제문자.Trim().Length > 0)
                        {
                            strChunk = strChunk.Replace(삭제문자, "");
                        }
                        if (담보파싱) {
                            PrdtPriceInfo datarow = getContractDetail롯데손보(sex, age, dr, strChunk, ref rtnStr);
                            contr_ds.Add(datarow);
                        }
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류" ));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetail롯데손보(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {

            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            String parsingType = dr.Field<String>("parsing_type");

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);


            if (parsingType == "패턴1")           //가입금액위치와 보험료위치가 있는경우
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                s보험료시작문자열 = dr.Field<String>("premium_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",","")); //가입금액
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }

                m = Regex.Match(strChunk, s보험료시작문자열);
                if (m.Success)
                {
                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",","")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 보험료 오류"));
                }
            }
            else if (parsingType == "패턴2")
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    String[] aryTmp = split가입금액보험료(s가입금액보험료);
                    if (aryTmp != null )
                    {
                        if ( String.IsNullOrWhiteSpace(aryTmp[0]) == true || String.IsNullOrWhiteSpace(aryTmp[1]) == true)
                        {
                            rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 분리 오류- 값이 없다"));
                        }
                        else
                        {
                            prdtPriceInfo.std_contract_amt = int.Parse(aryTmp[0].Replace(",", "")); //가입금액
                            prdtPriceInfo.premium = int.Parse(aryTmp[1].Replace(",", "")); //보험료

                        }

                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 분리 오류"));
                    }
                    
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }


            }
            else if (parsingType == "패턴3" || parsingType == "패턴4")
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                s보험료시작문자열 = dr.Field<String>("premium_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    String[] aryTmp = split가입금액보험료(s가입금액보험료);
                    if (aryTmp != null)
                    {
                        if (String.IsNullOrWhiteSpace(aryTmp[0]) == true || String.IsNullOrWhiteSpace(aryTmp[1]) == true)
                        {
                            rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 분리 오류- 값이 없다"));
                        }
                        else
                        {
                            prdtPriceInfo.std_contract_amt = int.Parse(aryTmp[0].Replace(",", "")); //가입금액
                            prdtPriceInfo.premium = int.Parse(aryTmp[1].Replace(",", "")); //보험료
                        }
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 분리 오류"));
                    }
                }
                MatchCollection mc = Regex.Matches(strChunk.Substring(strChunk.IndexOf(m.Value) + m.Value.Length), @s보험료시작문자열);
                long n보험료 = 0;
                foreach (Match m_main in mc)
                {
                    n보험료 = n보험료 + int.Parse(Regex.Match(m_main.Value, @"[0-9,]+$").Value.Replace(",", ""));
                }
                prdtPriceInfo.premium = prdtPriceInfo.premium + n보험료;
            }


            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Trim().Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기
                m = Regex.Match(strChunk, @"\/\d+(세|년)");
                if (m.Success)
                {
                    prdtPriceInfo.expiration = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 납입기간 오류"));
                }

                //납기
                m = Regex.Match(strChunk, @"(\d+세|\d+년|일시납)\/");
                if (m.Success)
                {

                    prdtPriceInfo.pay_term = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도 오류"));
                }

            }

            //만단위로변경
            if (prdtPriceInfo.std_contract_amt != 0) prdtPriceInfo.std_contract_amt = prdtPriceInfo.std_contract_amt / 10000;
            return prdtPriceInfo;
        }


        private String[] split가입금액보험료(String stmp)
        {
            int cnt = 0;
            int pos = 0;
            String[] aryRtn = { "", "" };
            if (stmp.Length >0 && stmp.IndexOf(",") > 0)
            {
                foreach (Char x in stmp)
                {
                    if (x == ',') cnt = 0;
                    else cnt = cnt + 1;

                    if (cnt > 3)
                    {
                        break;
                    }
                    pos = pos + 1;
                }
                aryRtn[0] = stmp.Substring(0, pos);
                aryRtn[1] = stmp.Substring(pos);

            }
            else
            {
                return null;
            }
            return aryRtn;
        }

        /*
         * 패턴 1 : 가입금액위치와 보험료위치가 있는경우 (종료위치)
         * 패턴 2 : 가입금액위치  보험료 시작위치가 각각있는경우
         * 패턴 3 : 가입금액위치와 보험료위치가 있는경우 (종료위치)  -- 여러개임 (21대 질병수술비 .. ) 위치는 반대로 찾아야한다.
         * */
        public string KB손보(String sex, int age, DataTable stdPrtd, string srcPrdt,String  s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String s패턴 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                s패턴 = dr.Field<String>("parsing_type").Trim();
                //if (s담보명.IndexOf("응급실내원비(응급)") >= 0) Debugger.Break();
                Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    if ("패턴3".Contains(s패턴))   //종료시작위치가 있는 패턴은
                    {
                        m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["), RegexOptions.RightToLeft);
                        n종료시작위치 = m.Index;
                        m = Regex.Match(srcPrdt.Substring(n종료시작위치), @s종료위치);
                    }
                    else
                    {
                        m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    }
                       

                    if (m.Success)
                    {
                        
                        if ("패턴3".Contains(s패턴))   //종료시작위치가 있는 패턴은
                        {
                            n종료위치 = (n종료시작위치 - n시작위치) + m.Index + m.Value.Length + 1;
                            strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        }
                        else
                        {
                            n종료위치 = m.Index + m.Value.Length + 1;
                            strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        }

                            

                        //삭제문자
                        String 삭제문자 = dr.Field<String>("del_string");
                        if (삭제문자.Trim().Length > 0)
                        {
                            strChunk = strChunk.Replace(삭제문자, "");
                        }
                        PrdtPriceInfo datarow = getContractDetailKB손보(sex, age, dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetailKB손보(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {

            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료시작문자열 = @"\s[0-9억천백만원]+\s[0-9,]+";
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            String parsingType = dr.Field<String>("parsing_type");

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);


            if (parsingType == "패턴1")           //가입금액위치와 보험료위치가 있는경우
            {
                m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9억천백만원]+").Value;
                    prdtPriceInfo.std_contract_amt = convertHangulToNumber(s가입금액보험료.Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }

            }
            else if (parsingType == "패턴2")
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    prdtPriceInfo.std_contract_amt = convertHangulToNumber(m.Value.Replace(",", "")); //가입금액
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }

                s보험료시작문자열 = dr.Field<String>("premium_st_str");
                m = Regex.Match(strChunk, s보험료시작문자열);
                if (m.Success)
                {
                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 보험료 오류"));
                }
            }
            else if (parsingType == "패턴3")
            {
                MatchCollection mc = Regex.Matches(strChunk, @s가입금액보험료시작문자열);
                long n보험료 = 0;
                foreach (Match m_main in mc)
                {
                    if (n보험료 == 0)  //최초일때만
                    {
                        s가입금액보험료 = Regex.Match(m_main.Value, @"[0-9억천백만원]+").Value;
                        prdtPriceInfo.std_contract_amt = convertHangulToNumber(s가입금액보험료.Replace(",", "")); //가입금액
                    }
                    n보험료 = n보험료 + int.Parse(Regex.Match(m_main.Value, @"[0-9,]+$").Value.Replace(",", ""));
                }
                prdtPriceInfo.premium = n보험료;

            }
            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"\/\d+(세|년)");
                if (m.Success)
                {
                    prdtPriceInfo.expiration = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도 오류"));
                }

                //납입기간
                m = Regex.Match(strChunk, @"\s\d+년\/");
                if (m.Success)
                {
                    prdtPriceInfo.pay_term = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 납입기간 오류"));
                }
            }

            return prdtPriceInfo;
        }

        /*
         * 패턴 1 : 가입금액위치와 보험료위치가 있는경우 (종료위치)
         * 패턴 2 : 가입금액위치  보험료 시작위치가 각각있는경우(갱신형)
         * 패턴 3 : 기간별로 시작위치가 변경이 될때  1.(일시|\d+년|\d+세)납\d+(년|세)만기 + 첫번째위치 검색하고 있으면 사용 없으면 첫번째위치로 검색
         * 패턴 4 : 기간별로 시작위치가 변경이 될때  1.(일시|\d+년|\d+세)납\d+(년|세)만기 + 첫번째위치 검색하고 있으면 사용 없으면 첫번째위치로 검색
         * */
        public string 현대해상(String sex, int age, DataTable stdPrtd, string srcPrdt, String s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                String parsingType = dr.Field<String>("parsing_type");
                 //if (s담보명.IndexOf("만성당뇨합병증진단") >= 0) Debugger.Break();
                Match m;

                if (parsingType == "패턴3")
                {
                     m = Regex.Match(srcPrdt, @"(일시|\d+년|\d+세)납\d+(년|세)만기"  + @s시작위치);
                     if (!m.Success)  //기간에따라변경되는것들
                     {
                         m = Regex.Match(srcPrdt, @s시작위치.Replace(@"\d+(년|세)납\d+(년|세)만기",""));
                     }
                }
                else
                {
                      m = Regex.Match(srcPrdt, @s시작위치);   //변환하지않음
                }

                    
               // if (!m.Success)  //5대골절수술이 변경됨..
               // {
               //     m = Regex.Match(srcPrdt, @s시작위치.Replace(@"\d+(년|세)납\d+(년|세)만기",""));
               // }

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        PrdtPriceInfo datarow = getContractDetail현대해상(sex, age, dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetail현대해상(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {

            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료시작문자열 = @"\s[0-9,]+\s[0-9,]+";
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            String parsingType = dr.Field<String>("parsing_type");

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);


            if (parsingType == "패턴1" || parsingType == "패턴3")           //가입금액위치와 보험료위치가 있는경우
            {
                m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"\s[0-9,]+").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }

            }
            else if (parsingType == "패턴2")
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+\s").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
                }
            }
            else if (parsingType == "패턴5")   // 반복
            {


                MatchCollection mc = Regex.Matches(strChunk, @s가입금액보험료시작문자열);
                long n보험료 = 0;
                long n이전보험료 = 0;
                long n현재보험료 = 0;
                bool bfirst = true;
                foreach (Match m_main in mc)
                {
                    if (bfirst)
                    {
                        s가입금액보험료 = Regex.Match(m_main.Value, @"[0-9,]+\s").Value;
                        prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",", "")); //가입금액
                        bfirst = false;
                    }

                    n현재보험료 = int.Parse(Regex.Match(m_main.Value, @"[0-9,]+$").Value.Replace(",", ""));
                    if (n이전보험료 != n현재보험료)
                    {
                        n보험료 = n보험료 + n현재보험료;
                    }
                    n이전보험료 = n현재보험료;
                }
                prdtPriceInfo.premium = n보험료; //보험료
            }
           prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                //"\d+(년|세)납\d+(년|세)만기""
                m = Regex.Match(strChunk, @"\d+(년|세)만기");
                if (m.Success)
                {
                    prdtPriceInfo.expiration  = m.Value.Replace("/", "").Trim();
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 납입기간 오류"));
                }

                //납입기간
                m = Regex.Match(strChunk, @"(일시|전기|\d+(년|세))납");
                if (m.Success)
                {
                    prdtPriceInfo.pay_term = m.Value.Replace("/", "").Trim();

                    //if (m.Value.Equals("전기납"))
                    //{
                    //    prdtPriceInfo.pay_term = prdtPriceInfo.expiration;
                    //}
                    //else if (m.Value.Equals("일시납"))
                    //{
                    //    prdtPriceInfo.pay_term = "0";
                    //}
                    //else
                    //{
                    //    prdtPriceInfo.pay_term = m.Value.Replace("/", "").Trim();
                    //}
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도 오류"));
                }

            }

            //만단위로변경
            if (prdtPriceInfo.std_contract_amt != 0) prdtPriceInfo.std_contract_amt = prdtPriceInfo.std_contract_amt / 10000;
            return prdtPriceInfo;
        }

        /*
         *  패턴1 : 가입금액위치와 보험료위치가 있는경우(비갱신형)
         *  패턴2 : 가입금액위치와 보험료위치가 각각 있는경우(갱신형)
         */
        public string 동부화재(String sex, int age, DataTable stdPrtd, string srcPrdt, String s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                //if (s담보명.IndexOf("질병수술비") >= 0) Debugger.Break();
                Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);

                        //포함문자
                        String 포함문자 = dr.Field<String>("embed_string");
                        bool 담보파싱 = true;
                        if (포함문자.Trim().Length > 0)
                        {
                            if (strChunk.IndexOf(포함문자) < 0) 담보파싱 = false;
                        }
                        if (담보파싱)
                        {
                            PrdtPriceInfo datarow = getContractDetail동부화재(sex, age, dr, strChunk, ref rtnStr);
                            contr_ds.Add(datarow);
                        }
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetail동부화재(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {


            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료시작문자열 = @"\s[0-9,]+\s[0-9,]+";
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            String parsingType = dr.Field<String>("parsing_type");

            String s삭제문자 = dr.Field<String>("del_string");
            if (s삭제문자.Trim() != "")
            {
                strChunk = strChunk.Replace(s삭제문자.Trim(), "");
            }

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);



            if (parsingType == "패턴1")           //가입금액위치와 보험료위치가 있는경우
            {
                m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
                }

            }
            else if (parsingType == "패턴2")
            {

                m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
                }

            }
            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"\s\d+(세|년)만기\d+(세|년)납");
                if (m.Success)
                {
                    prdtPriceInfo.expiration = Regex.Match(m.Value, @"\d+(세|년)만기").Value; 

                    prdtPriceInfo.pay_term = Regex.Match(m.Value, @"\d+(세|년)납").Value;
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도/납입기간 오류"));
                }
            }

            return prdtPriceInfo;
        }

        /*
         *흥국
         *  패턴 1  시작위치부터 종료위치 순방향
         *  패턴 2  시작위치부터 종료위치 역방향
         *  패턴 3  실손도수,MRI,주사료 순방향,복수,보험료 같은것 제외
         *  패턴 4  암수술비  순방향 가입금액 * 배수처리   가입금액 * 가입금액시작문자  
         *  패턴 5  암수술비  역방향 가입금액 * 배수처리   가입금액 * 가입금액시작문자  
         *  패턴 7  순방향 가입금액 위치 보험료 위치 각각
         *  패턴 8  순방향 가입금액 위치 보험료 위치 가입금액 * 배수처리   가입금액 * 가입금액시작문자   
         *  패턴 9  역방향 가입금액 위치 보험료 위치 각각
         */
        public string 흥국화재(String sex, int age, DataTable stdPrtd, string srcPrdt, String s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String s패턴 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm").Trim();
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                s패턴 = dr.Field<String>("parsing_type").Trim();
                //if (s담보명.IndexOf("유치보존치료비") >= 0) Debugger.Break();
                Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    
                    if (m.Success)
                    {
                        if ("패턴1,패턴3,패턴4,패턴6,패턴7,패턴8".Contains(s패턴))
                        {
                            m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                            n종료위치 = m.Index + m.Value.Length + 1;
                            strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        }else if ("패턴2,패턴5,패턴9".Contains(s패턴))
                        {
                            m = Regex.Match(srcPrdt.Substring(0,n시작위치 + s시작위치.Length), @s종료위치, RegexOptions.RightToLeft);
                            n종료위치 = m.Index;
                            strChunk = srcPrdt.Substring(n종료위치, n시작위치 - n종료위치 + s시작위치.Length);
                        }
                        //else if ("패턴3".Contains(s패턴))
                        //{
                        //    n종료위치 =  srcPrdt.LastIndexOf(s종료위치) + s종료위치.Length + 1 - n시작위치;
                        //    strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        //}
                        else
                        {
                            rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 패턴 오류"));
                        }
                        PrdtPriceInfo datarow = getContractDetail흥국화재(sex, age, dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        private PrdtPriceInfo getContractDetail흥국화재(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {

            //보험상세
            
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료시작문자열 = @"\s[0-9,]+\s[0-9,]+";
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            string s패턴 = dr.Field<String>("parsing_type").Trim();


            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);
            if ("패턴7,패턴8,패턴9".Contains(s패턴))
            {
                s가입금액보험료시작문자열 = dr.Field<String>("contract_amt_st_str").Trim();
            }

            m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
            if (m.Success)
            {

                //가입금액
                if ("패턴4,패턴5".Contains(s패턴))
                {
                    String 배수 = dr.Field<String>("contract_amt_st_str").Trim();
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+").Value.Replace(",", "");
                    s가입금액보험료 = (Int32.Parse(s가입금액보험료) * Single.Parse(배수)).ToString("####0");
                    
                }
                else  if ("패턴6,패턴7,패턴9".Contains(s패턴))
                {
                    s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str").Trim();

                    Match mm = Regex.Match(strChunk, s가입금액시작문자열);
                    if (mm.Success)
                    {
                        s가입금액보험료 = Regex.Match(mm.Value, @"[0-9,]+").Value;                  //앞에 가입금액이 있다.
                        s가입금액보험료 = s가입금액보험료.Replace(",", "");
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                    }
                }
                else if ("패턴8".Contains(s패턴))
                {
                    
                    s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str").Trim();
                    Match mm = Regex.Match(strChunk, s가입금액시작문자열);
                    if (mm.Success)
                    {
                        String 배수 = dr.Field<String>("except_string").Trim();
                        s가입금액보험료 = Regex.Match(mm.Value, @"[0-9,]+").Value;                  //앞에 가입금액이 있다.
                        s가입금액보험료 = s가입금액보험료.Replace(",", "");
                        s가입금액보험료 = (Int32.Parse(s가입금액보험료) * Single.Parse(배수)).ToString("####0");
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                    }
                }
                else
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+").Value.Replace(",", "");
                }
                prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료); //가입금액


                //보험료   --- 보험료합산
                if ("패턴3".Contains(s패턴))
                {
                    MatchCollection mc = Regex.Matches(strChunk, @s가입금액보험료시작문자열);
                    long n보험료 = 0;
                    long n이전보험료 = 0;
                    long n현재보험료 = 0;
                    foreach (Match m_main in mc)
                    {
                         n현재보험료 = int.Parse(Regex.Match(m_main.Value, @"[0-9,]+$").Value.Replace(",", ""));
                        if (n이전보험료 != n현재보험료)
                        {
                            n보험료 = n보험료 + n현재보험료;
                        }
                        n이전보험료 = n현재보험료;
                    }
                    s보험료 = n보험료.ToString();
                    
                }else  if ("패턴7,패턴8,패턴9".Contains(s패턴))
                {
                    s보험료시작문자열 = dr.Field<String>("premium_st_str").Trim();

                    Match mm = Regex.Match(strChunk, s보험료시작문자열);
                    if (mm.Success)
                    {
                        s보험료 = Regex.Match(mm.Value, @"[0-9,]+$").Value;                  //**세만기 #,##0
                        s보험료 = s보험료.Replace(",", "");
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                    }
                }
                else
                {
                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                }
                prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
            }
            else
            {
                rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
            }



            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"(\d+|일)(시납|년납|세납|년갱신)\s\d+(년|세)만기");
                if (m.Success)
                {
                    prdtPriceInfo.expiration = Regex.Match(m.Value, @"\d+(세|년)만기").Value;

                    prdtPriceInfo.pay_term = Regex.Match(m.Value, @"(\d+|일)(시납|년납|세납|년갱신)").Value;
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도/납입기간 오류"));
                }
            }
            //만단위로변경
            if (prdtPriceInfo.std_contract_amt != 0)
            {
                //Double dTmp = Convert.ToDouble(prdtPriceInfo.std_contract_amt) / 10000;
                prdtPriceInfo.std_contract_amt = (int)Math.Round( Convert.ToDouble(prdtPriceInfo.std_contract_amt) / 10000,0);
            }
                
            return prdtPriceInfo;
        }

        /*
         *  패턴1 : 가입금액위치와 보험료위치가 있는경우(비갱신형)
         *  패턴2 : 가입금액위치와 보험료위치가 각각 있는경우(갱신형)
         *  패턴3 : 가입금액과보험료가하나로있음
         *  패턴4 : 가입금액과보험료가 하나임
         *  패턴6 : 가입금액위치와 보험료위치가 각각 있는경우(갱신형) 인데 아래부터검색
         */
        public string MG손해(String sex, int age, DataTable stdPrtd, string srcPrdt, String s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String s패턴 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                s패턴 = dr.Field<String>("parsing_type");
                //if (s담보명.IndexOf("질병사망") >= 0) Debugger.Break();
                Match m;

                if ("패턴6".Contains(s패턴))   //아래부터검색한다
                {
                    m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["), RegexOptions.RightToLeft);
                }
                else
                {
                    m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));
                }

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        PrdtPriceInfo datarow = getContractDetailMG손해(sex, age, dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        /*
         *  패턴1 : 가입금액위치와 보험료위치가 있는경우(비갱신형)
         *  패턴2 : 가입금액위치와 보험료위치가 각각 있는경우(갱신형)
         *  패턴3 : 가입금액과보험료가하나로있음
         *  패턴4 : 가입금액과보험료가 하나임
         */
        private PrdtPriceInfo getContractDetailMG손해(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {


            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료시작문자열 = @"[0-9,]+\s*\d+(년|세)\/\d+(세|년)\s*[0-9,]+";
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            String parsingType = dr.Field<String>("parsing_type");

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);




            if (parsingType == "패턴1")           //가입금액위치와 보험료위치가 있는경우
            {
                String s삭제문자 = dr.Field<String>("del_string");
                if (s삭제문자.Trim() != "")
                {
                    strChunk = strChunk.Replace(s삭제문자.Trim(), "");
                }

                m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
                if (m.Success)
                {


                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+\s*\d+(년|세)\/").Value;
                    if (Regex.Match(s가입금액보험료, @"[0-9]{2}(년|세)\/").Success)
                    {
                        s가입금액보험료 = s가입금액보험료.Replace(Regex.Match(s가입금액보험료, @"[0-9]{2}(년|세)\/").Value, "");
                        prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Trim().Replace(",", "")); //가입금액
                    }
                    else if (Regex.Match(s가입금액보험료, @"[0-9]{1}(년|세)\/").Success)
                    {
                        s가입금액보험료 = s가입금액보험료.Replace(Regex.Match(s가입금액보험료, @"[0-9]{1}(년|세)\/").Value, "");
                        prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Trim().Replace(",", "")); //가입금액
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
                    }
                    

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
                }
            }
            else if (parsingType == "패턴2" || parsingType == "패턴6")           //갱신형들
            {
                String s삭제문자 = dr.Field<String>("del_string");
                if (s삭제문자.Trim() != "")
                {
                    strChunk = strChunk.Replace(s삭제문자.Trim(), "");
                }

                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value.Trim(), @"[0-9,]+").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Replace(",", "")); //가입금액
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액  오류"));
                }

                s보험료시작문자열 = dr.Field<String>("premium_st_str");
                m = Regex.Match(strChunk, @s보험료시작문자열);
                if (m.Success)
                {
                    s보험료 = Regex.Match(m.Value.Trim(), @"[0-9,]+$").Value;
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 보험료  오류"));
                }

            }
            else if (parsingType == "패턴3")           //가입금액과보험료가하나로있음 -->예)간편심사 특정나이 가입금액시작문자열로한다.
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+$").Value;
                    String[] aryTmp = split가입금액보험료(s가입금액보험료);
                    if (aryTmp != null)
                    {
                        prdtPriceInfo.std_contract_amt = int.Parse(aryTmp[0].Replace(",", "")); //가입금액
                        prdtPriceInfo.premium = int.Parse(aryTmp[1].Replace(",", "")); //보험료
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 분리 오류"));
                    }

                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }
            }
            else if (parsingType == "패턴4")           //가입금액과보험료가하나로있음 구분을 할수 없음 --->프로그래밍
            {
                s가입금액시작문자열 = dr.Field<String>("contract_amt_st_str");
                String s가입금액위치 = dr.Field<String>("del_string");
                m = Regex.Match(strChunk, @s가입금액시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 =  Regex.Match(m.Value, @"[0-9,]+$").Value;
                    String[] aryTmp = { "0", "0" };
                    aryTmp[0] = s가입금액보험료.Substring(0, int.Parse(s가입금액위치) -1);
                    aryTmp[1] = s가입금액보험료.Substring(int.Parse(s가입금액위치) -1);
                    if (aryTmp != null)
                    {
                        prdtPriceInfo.std_contract_amt = int.Parse(aryTmp[0].Replace(",", "")); //가입금액
                        prdtPriceInfo.premium = int.Parse(aryTmp[1].Replace(",", "")); //보험료
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 분리 오류"));
                    }

                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 오류"));
                }
            }
            else
            {
                rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + "  분석패턴 오류"));
            }

            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"[0-9]{2}(년|세)\/\d+(세|년)");
                if (m.Success != true)
                {
                    m = Regex.Match(strChunk, @"[0-9]{1}(년|세)\/\d+(세|년)");
                } 
                if (m.Success)
                {
                    prdtPriceInfo.expiration = Regex.Match(m.Value, @"\/\d+(세|년)").Value.Replace("/", "");

                    prdtPriceInfo.pay_term  = Regex.Match(m.Value, @"\d+(년|세)\/").Value.Replace("/","");
                    
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도/납입기간 오류"));
                }
            }

            return prdtPriceInfo;
        }


        public string NH손해(String sex, int age, DataTable stdPrtd, string srcPrdt, String s삭제구분)
        {
            StringBuilder rtnStr = new StringBuilder(2000);

            String s담보명 = string.Empty;
            String s시작위치 = string.Empty;
            String s종료위치 = string.Empty;
            String s패턴 = string.Empty;
            String strChunk = string.Empty;

            String s가입금액 = string.Empty;
            String s보험기간 = string.Empty;

            int n시작위치 = 0;
            int n종료위치 = 0;
            List<PrdtPriceInfo> contr_ds = new List<PrdtPriceInfo>();
            foreach (DataRow dr in stdPrtd.Rows)
            {

                //보험기준명
                s담보명 = dr.Field<String>("insur_nm");
                s시작위치 = @"\s" + dr.Field<String>("st_string");
                s종료위치 = dr.Field<String>("ed_string");
                s패턴 = dr.Field<String>("parsing_type");
                //if (s담보명.IndexOf("상해후유장해") >= 0) Debugger.Break();
                Match m = Regex.Match(srcPrdt, @s시작위치.Replace(@"(", @"\(").Replace(@")", @"\)").Replace(@"[", @"\["));

                if (m.Success)
                {
                    n시작위치 = m.Index;
                    m = Regex.Match(srcPrdt.Substring(n시작위치), @s종료위치);
                    if (m.Success)
                    {
                        n종료위치 = m.Index + m.Value.Length + 1;
                        strChunk = srcPrdt.Substring(n시작위치, n종료위치);
                        PrdtPriceInfo datarow = getContractDetailNH손해(sex, age, dr, strChunk, ref rtnStr);
                        contr_ds.Add(datarow);
                    }
                    else
                    {
                        rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 종류위치 오류"));
                    }
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age.ToString(), dr.Field<String>("insur_disp_nm") + " 시작위치 오류(" + dr.Field<int>("plancnt").ToString() + ")"));
                }

            }
            if (contr_ds.Count > 0)
            {
                int count = setPrdtPriceInfo(contr_ds, s삭제구분);
            }



            return rtnStr.ToString();
        }

        /*
         *  패턴1 : 가입금액위치와 보험료위치가 있는경우(비갱신형)
         *  패턴2 : 가입금액위치와 보험료위치가 각각 있는경우(갱신형)
         */
        private PrdtPriceInfo getContractDetailNH손해(String sex, int age, DataRow dr, String strChunk, ref StringBuilder rtnStr)
        {


            //보험상세
            string s가입금액 = string.Empty;
            string s보험료 = string.Empty;
            String s가입금액보험료시작문자열 = @"[0-9,]+\s[0-9,]+\s\d+\s*(년|세)납\s\d+\s*(세|년)만기";
            String s가입금액보험료시작문자열2 = @"[0-9,]+\s[0-9,]+\s추가납입형";
            String s가입금액보험료 = string.Empty;
            String s가입금액시작문자열 = string.Empty;
            String s보험료시작문자열 = string.Empty;
            Match m;

            String parsingType = dr.Field<String>("parsing_type");

            //기본값설정
            PrdtPriceInfo prdtPriceInfo = getPrdtPriceInfo(dr, sex, age);




            if (parsingType == "패턴1")           //가입금액위치와 보험료위치가 있는경우
            {

                m = Regex.Match(strChunk, @s가입금액보험료시작문자열);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Trim().Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+\s\d+\s*(년|세)납").Value;
                    s보험료 = s보험료.Replace(Regex.Match(s보험료, @"[0-9]+\s*(년|세)납").Value, "");
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액 보험료 오류"));
                }
            }
            else if (parsingType == "패턴2")           //갱신형들
            {
                m = Regex.Match(strChunk, @s가입금액보험료시작문자열2);
                if (m.Success)
                {
                    s가입금액보험료 = Regex.Match(m.Value, @"[0-9,]+").Value;
                    prdtPriceInfo.std_contract_amt = int.Parse(s가입금액보험료.Trim().Replace(",", "")); //가입금액

                    s보험료 = Regex.Match(m.Value, @"[0-9,]+\s추가납입형").Value;
                    s보험료 = s보험료.Replace(Regex.Match(s보험료, @"추가납입형").Value, "");
                    prdtPriceInfo.premium = int.Parse(s보험료.Replace(",", "")); //보험료
                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 가입금액  오류"));
                }

            }
            else
            {
                rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + "  분석패턴 오류"));
            }

            prdtPriceInfo.renewal_pd = dr.Field<int>("renewal_pd");
            prdtPriceInfo.renewal_yn = dr.Field<String>("renewal_yn");
            if (dr.Field<String>("renewal_yn").Equals("Y")) //갱신형일경우
            {
                prdtPriceInfo.pay_term = dr.Field<int>("renewal_pd").ToString() + "년납";
                prdtPriceInfo.expiration = dr.Field<int>("renewal_pd").ToString() + "년만기";
            }
            else
            {
                //만기년도
                m = Regex.Match(strChunk, @"\d+\s*(년|세)납\s\d+\s*(세|년)만기");
                if (m.Success)
                {
                    prdtPriceInfo.pay_term  = Regex.Match(m.Value, @"\d+\s*(년|세)납").Value;

                    prdtPriceInfo.expiration = Regex.Match(m.Value, @"\d+\s*(세|년)만기").Value.Replace(" " ,"");

                }
                else
                {
                    rtnStr.AppendLine(String.Format("{0}-{1}-{2}-{3}: {4}", dr.Field<String>("compy_cd"), dr.Field<String>("prdt_cd"), sex, age, dr.Field<String>("insur_disp_nm") + " 만기년도/납입기간 오류"));
                }
            }

            return prdtPriceInfo;
        }



        private int convertHangulToNumber(String Hangul)
        {
            //XXXX억XXX천XXX백XXXX만  ==> 숫자로변경 만단위만 변경함
            int ntmp = 0;
            //억 
            Match m = Regex.Match(Hangul, @"[0-9]+억");
            if (m.Success)
            {
                ntmp = 10000 * int.Parse(m.Value.Replace("억", ""));
            }
            //천만원
            m = Regex.Match(Hangul, @"[0-9]+천");
            if (m.Success)
            {
                ntmp = ntmp + 1000 * int.Parse(m.Value.Replace("천", ""));
            }
            //백
            m = Regex.Match(Hangul, @"[0-9]+백");
            if (m.Success)
            {
                ntmp = ntmp + 100 * int.Parse(m.Value.Replace("백", ""));
            }
            //만원
            m = Regex.Match(Hangul, @"[0-9]+만");
            if (m.Success)
            {
                ntmp = ntmp + int.Parse(m.Value.Replace("만", ""));
            }

            return ntmp;
        }

        /**
         *  보험료 테이블에 저장한다.
         **/
        private int setPrdtPriceInfo(List<PrdtPriceInfo> ct,String b삭제모드 = "전체삭제")
        {
            //계약 사항을 저장하고 contract_seq를 구합니다.
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };
            int cnt = 0;
            sb.AppendLine("SET NOCOUNT ON")
             .AppendLine(" INSERT INTO [TB_TIC_PRDT_PRICE]")
             .AppendLine("	([compy_cd]")
             .AppendLine("	,[prdt_cd]")
             .AppendLine("	,[insur_cd]")
             .AppendLine("	,[insur_nm]")
             .AppendLine("	,[sex]")
             .AppendLine("	,[age]")
             .AppendLine("	,[pay_term]")
             .AppendLine("	,[expiration]")
             .AppendLine("	,[std_contract_amt]")
             .AppendLine("	,[renewal_yn]")
             .AppendLine("	,[renewal_pd]")
             .AppendLine("	,[notice_yn]")
             .AppendLine("	,[premium])")
             .AppendLine("VALUES")
             .AppendLine("	(@compy_cd")
             .AppendLine("	,@prdt_cd")
             .AppendLine("	,@insur_cd")
             .AppendLine("	,@insur_nm")
             .AppendLine("	,@sex")
             .AppendLine("	,@age")
             .AppendLine("	,@pay_term")
             .AppendLine("	,@expiration")
             .AppendLine("	,@std_contract_amt")
             .AppendLine("	,@renewal_yn")
             .AppendLine("	,@renewal_pd")
             .AppendLine("	,@notice_yn")
            .AppendLine("	,@premium )");

            if (b삭제모드.Equals("전체삭제")){
                sb2.AppendLine("SET NOCOUNT ON")
                 .AppendLine(" delete TB_TIC_PRDT_PRICE")
                 .AppendLine("	where compy_cd = @compy_cd and  prdt_cd =  @prdt_cd and sex = @sex and age =@age ");
            }
            else
            {
                sb2.AppendLine("SET NOCOUNT ON")
                 .AppendLine(" delete TB_TIC_PRDT_PRICE")
                 .AppendLine("	where compy_cd = @compy_cd and  prdt_cd =  @prdt_cd and  insur_cd = @insur_cd  and sex = @sex and age =@age ");
            }

            using (SqlConnection con = new SqlConnection(Common.Core.DBConnectionString))
            {
                con.Open();

                // SqlTrasaction 클래스의인스턴스생성
                SqlTransaction tran = con.BeginTransaction(); // 트랜잭션시작
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.Transaction = tran; // 현재사용할트랜잭션객체지정
                try
                {
                    //parameter
                    cmd.Parameters.Add("@compy_cd", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@prdt_cd", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@sex", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@age", SqlDbType.Int);

                    //parameter
                    cmd.Parameters.Add("@insur_cd", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@insur_nm", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@pay_term", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@expiration", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@std_contract_amt", SqlDbType.Int);
                    cmd.Parameters.Add("@renewal_yn", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@renewal_pd", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@notice_yn", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@premium", SqlDbType.BigInt);

                    foreach (PrdtPriceInfo rec in ct)
                    {
                        cmd.Parameters["@compy_cd"].Value = rec.compy_cd;
                        cmd.Parameters["@prdt_cd"].Value = rec.prdt_cd;
                        cmd.Parameters["@insur_cd"].Value = rec.insur_cd;
                        cmd.Parameters["@insur_nm"].Value = rec.insur_nm;
                        cmd.Parameters["@sex"].Value = rec.sex;
                        cmd.Parameters["@age"].Value = rec.age;
                        cmd.Parameters["@pay_term"].Value = rec.pay_term;
                        cmd.Parameters["@expiration"].Value = rec.expiration;
                        cmd.Parameters["@std_contract_amt"].Value = rec.std_contract_amt;
                        cmd.Parameters["@renewal_yn"].Value = rec.renewal_yn;
                        cmd.Parameters["@renewal_pd"].Value = rec.renewal_pd;
                        cmd.Parameters["@notice_yn"].Value = rec.notice_yn;
                        cmd.Parameters["@premium"].Value = rec.premium;

                        if (b삭제모드.Equals("전체삭제"))
                        {
                            if (cnt ==0)            //시작일경우
                            {
                                cmd.CommandText = sb2.ToString();// 쿼리지정
                                cmd.ExecuteNonQuery(); // 실행
                            }
                        }
                        else
                        {
                            //보험코드마다 삭제한다.
                            cmd.CommandText = sb2.ToString();// 쿼리지정
                            cmd.ExecuteNonQuery(); // 실행                                    
                        }
                      
                        cmd.CommandText = sb.ToString();// 쿼리지정

                        cmd.ExecuteNonQuery(); // 실행
                        cnt = cnt + 1;
                    }

                    tran.Commit(); // 트랜잭션commit
                }
                catch (Exception e)
                {
                    tran.Rollback(); // 에러발생시rollback
                    throw e;
                }
            }
            return ct.Count;
        }

        private PrdtPriceInfo getPrdtPriceInfo(DataRow dr, String sex, int age)
        {
            PrdtPriceInfo prdtPriceInfo = new PrdtPriceInfo();
            prdtPriceInfo.sex = sex;
            prdtPriceInfo.age = age;
            prdtPriceInfo.compy_cd = dr.Field<String>("compy_cd");
            prdtPriceInfo.prdt_cd = dr.Field<String>("prdt_cd");
            prdtPriceInfo.insur_cd = dr.Field<String>("insur_cd");
            prdtPriceInfo.insur_nm = dr.Field<String>("insur_disp_nm");
            prdtPriceInfo.notice_yn = dr.Field<String>("notice_yn");
            return prdtPriceInfo;
        }
        #endregion
    }
}
