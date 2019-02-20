using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;

namespace ParsingGasulPDF.Services
{
    class GasulPDFService
    {
        public static DataTable getCustList(String consultant_id)
        {
            //데이터를 읽어 온다.
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")

                .AppendLine("  select customer_seq as 고객코드, ")
                .AppendLine("  customer_name as 고객명 ")
                .AppendLine("  from tb_customer ")
                .AppendLine("  where consultant_id =  @consultant_id ")
                .AppendLine("  order by customer_name ");

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@consultant_id", consultant_id),
            };

            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(Common.Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw e;
            }

            return dt;
        }



        public static DataTable getPrdtPDF()
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine("  select ")
                .AppendLine(" good_company as 회사코드,  ")
                .AppendLine(" good_seq as 상품코드,  ")
                .AppendLine(" good_name as 상품명, ")
                .AppendLine(" sales_st_date as 상품등록일 ")
                .AppendLine(" from[dbo].[tb_prdt_pdf]  ")
                .AppendLine(" where use_yn = 'Y'  ")
                .AppendLine(" and sales_st_date is not null ");
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(Common.Core.DBConnectionString, CommandType.Text, sb.ToString());
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw e;
            }

            return dt;
        }


        public static DataTable getPrdtPdfDedail(string goodCompany, string goodSeq)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine(" select ")
                .AppendLine("	a.good_company AS 회사코드, ")
                .AppendLine("	a.good_seq AS 상품코드, ")
                .AppendLine("	(select sm from TB_MATLPCDP as cdp where cdp.th_code = a.good_company) as 회사명,")
                .AppendLine("	a.good_name AS 상품명, ")
                .AppendLine("	b.good_item_item as 보험대분류코드,")
                .AppendLine("	b.good_item_dtal as 보험중분류코드,")
                .AppendLine("	c.jgyty_code as 보험코드,")
                .AppendLine("	c.gigy  as 기준가입금액,")
                .AppendLine("	b.good_item_name 보험명, ")
                .AppendLine("	b.exceptword as 제외단어, ")
                .AppendLine(" b.add_item_item as 추가보험대분류코드, ")
                .AppendLine("   b.add_item_dtal as 추가보험중분류코드, ")
                .AppendLine("  d.jgyty_code as 추가보험코드, ")
                .AppendLine("  d.gigy as 추가기준가입금액 ")
                .AppendLine(" from ")
                .AppendLine("    tb_prdt_pdf a")
                .AppendLine("	join tb_prdt_pdf_detail  b")
                .AppendLine("		on a.good_company = b.good_company ")
                .AppendLine("		and a.good_seq = b.good_seq ")
                .AppendLine("  join TB_MATLPMSS c ")
                .AppendLine("		on b.good_item_dtal = c.good_item_dtal")
                .AppendLine("		and b.good_item_item = c.good_item_item")
                .AppendLine("  left join TB_MATLPMSS d ")
                .AppendLine("		on b.add_item_dtal = d.good_item_dtal")
                .AppendLine("		and b.add_item_item = d.good_item_item")
                .AppendLine(" where ")
                .AppendLine("	1=1 ")
                .AppendLine("	and a.good_company = @good_company ")
                .AppendLine("	and a.good_seq = @good_seq ");

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@good_company", goodCompany),
                new System.Data.SqlClient.SqlParameter("@good_seq", goodSeq)
            };

            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(Common.Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw e;
            }

            return dt;
        }


    }
}
