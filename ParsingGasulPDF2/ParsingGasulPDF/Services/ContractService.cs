using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Threading.Tasks;
using ParsingGasulPDF.Common;
using ParsingGasulPDF.Models;

namespace ParsingGasulPDF.Services
{
    class ContractService
    {
        //기계약을 신규로 저장합니다.
        public int setContract(Contract ct, List<ContractDetail> ctd)
        {
            //계약 사항을 저장하고 contract_seq를 구합니다.
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };
            int contract_seq = 0;


            if (ct.gasul_product_seq != 0)  //표준가설인경우
            {


                sb.AppendLine("SET NOCOUNT ON")
                 .AppendLine("INSERT INTO [tb_contract]")
                 .AppendLine("	([consultant_id]")
                 .AppendLine("	,[good_company]")
                 .AppendLine("	,[good_seq]")
                 .AppendLine("	,[good_name]")
                 .AppendLine("	,[contract_name]")
                 .AppendLine("	,[insured_seq]")
                 .AppendLine("	,[premium]")
                 .AppendLine("	,[contract_date]")
                 .AppendLine("	,[pay_term]")
                 .AppendLine("	,[expire_year]")
                 .AppendLine("	,[interest_rate]")
                 .AppendLine("	,[pay_end_year]")
                 .AppendLine("	,[keep]")
                 .AppendLine("	,[proposal]")
                 .AppendLine("	,[gasul_product_seq]")
                 .AppendLine("	,[in_date])")
                 .AppendLine("VALUES")
                 .AppendLine("	(@consultant_id")
                 .AppendLine("	,@good_company")
                 .AppendLine("	,@good_seq")
                 .AppendLine("	,@good_name")
                 .AppendLine("	,@contract_name")
                 .AppendLine("	,@insured_seq")
                 .AppendLine("	,@premium")
                 .AppendLine("	,@contract_date")
                 .AppendLine("	,@pay_term")
                 .AppendLine("	,@expire_year")
                 .AppendLine("	,@interest_rate")
                 .AppendLine("	,@pay_end_year")
                 .AppendLine("	,'Y'")
                 .AppendLine("	,@proposal")
                 .AppendLine("	,@gasul_product_seq")
                 .AppendLine("	,GETDATE())")
                 .AppendLine("SELECT @@IDENTITY AS 'contract_seq'");

                arParams = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@consultant_id", ct.consultant_id),
                    new System.Data.SqlClient.SqlParameter("@good_company", ct.good_company),
                    new System.Data.SqlClient.SqlParameter("@good_seq", ct.good_seq),
                    new System.Data.SqlClient.SqlParameter("@good_name", ct.good_name),
                    new System.Data.SqlClient.SqlParameter("@contract_name", ct.contract_name),
                    new System.Data.SqlClient.SqlParameter("@insured_seq", ct.insured_seq),
                    new System.Data.SqlClient.SqlParameter("@premium", ct.premium),
                    new System.Data.SqlClient.SqlParameter("@contract_date", ct.contract_date),
                    new System.Data.SqlClient.SqlParameter("@pay_term", ct.pay_term),
                    new System.Data.SqlClient.SqlParameter("@expire_year", ct.expire_year),
                    new System.Data.SqlClient.SqlParameter("@interest_rate", ct.interest_rate),
                    new System.Data.SqlClient.SqlParameter("@pay_end_year", ct.pay_end_year),
                    new System.Data.SqlClient.SqlParameter("@proposal", ct.proposal),
                    new System.Data.SqlClient.SqlParameter("@gasul_product_seq", ct.gasul_product_seq)
                };

                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Common.Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];
                    contract_seq = Convert.ToInt32(dt.Rows[0][0]);
                }
                catch (Exception e)
                {
                    throw e;
                }


            }
            else //타사인경우
            {

                sb.AppendLine("SET NOCOUNT ON")
                    .AppendLine("INSERT INTO [tb_contract]")
                    .AppendLine("	([consultant_id]")
                    .AppendLine("	,[good_company]")
                    .AppendLine("	,[good_seq]")
                    .AppendLine("	,[good_name]")
                    .AppendLine("	,[contract_name]")
                    .AppendLine("	,[insured_seq]")
                    .AppendLine("	,[premium]")
                    .AppendLine("	,[contract_date]")
                    .AppendLine("	,[pay_term]")
                    .AppendLine("	,[expire_year]")
                    .AppendLine("	,[interest_rate]")
                    .AppendLine("	,[pay_end_year]")
                    .AppendLine("	,[keep]")
                    .AppendLine("	,[proposal]")
                    .AppendLine("	,[in_date])")
                    .AppendLine("VALUES")
                    .AppendLine("	(@consultant_id")
                    .AppendLine("	,@good_company")
                    .AppendLine("	,@good_seq")
                    .AppendLine("	,@good_name")
                    .AppendLine("	,@contract_name")
                    .AppendLine("	,@insured_seq")
                    .AppendLine("	,@premium")
                    .AppendLine("	,@contract_date")
                    .AppendLine("	,@pay_term")
                    .AppendLine("	,@expire_year")
                    .AppendLine("	,@interest_rate")
                    .AppendLine("	,@pay_end_year")
                    .AppendLine("	,'Y'")
                    .AppendLine("	,@proposal")
                    .AppendLine("	,GETDATE())")
                    .AppendLine("SELECT @@IDENTITY AS 'contract_seq'");

                arParams = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@consultant_id", ct.consultant_id),
                    new System.Data.SqlClient.SqlParameter("@good_company", ct.good_company),
                    new System.Data.SqlClient.SqlParameter("@good_seq", ct.good_seq),
                    new System.Data.SqlClient.SqlParameter("@good_name", ct.good_name),
                    new System.Data.SqlClient.SqlParameter("@contract_name", ct.contract_name),
                    new System.Data.SqlClient.SqlParameter("@insured_seq", ct.insured_seq),
                    new System.Data.SqlClient.SqlParameter("@premium", ct.premium),
                    new System.Data.SqlClient.SqlParameter("@contract_date", ct.contract_date),
                    new System.Data.SqlClient.SqlParameter("@pay_term", ct.pay_term),
                    new System.Data.SqlClient.SqlParameter("@expire_year", ct.expire_year),
                    new System.Data.SqlClient.SqlParameter("@interest_rate", ct.interest_rate),
                    new System.Data.SqlClient.SqlParameter("@pay_end_year", ct.pay_end_year),
                    new System.Data.SqlClient.SqlParameter("@proposal", ct.proposal)
                };

                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Common.Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];
                    contract_seq = Convert.ToInt32(dt.Rows[0][0]);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            //주/특약 사항을 저장합니다.
            foreach (ContractDetail cd in ctd)
            {
                cd.contract_seq = contract_seq;
                setContractDetail(cd);
            }

            //contract_seq를 리턴합니다.
            return contract_seq;
        }

        //기계약의 주/특약을 저장합니다
        private void setContractDetail(ContractDetail cd)
        {
            StringBuilder sb = new StringBuilder();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            //contract_seq가 있는지 확인합니다.
            if (cd.contract_seq < 0)
            {
                throw new Exception("contract_seq가 설정되어 있지 않습니다.");
            }


            if (cd.renewal_yn.Equals("True") == true)
            {
                cd.renewal_yn = "Y";
            }
            else
            {
                cd.renewal_yn = "N";
            }


            if (cd.gasul_product_seq != 0)  //표준가설인경우 
            {


                sb.AppendLine("INSERT INTO [tb_contract_detail]")
                    .AppendLine("	([contract_seq]")
                    .AppendLine("	,[good_item_dtal]")
                    .AppendLine("	,[good_item_item]")
                    .AppendLine("	,[jgyty_code]")
                    .AppendLine("	,[insured_seq]")
                    .AppendLine("	,[contract_amount]")
                    .AppendLine("	,[expire_year]")
                    .AppendLine("	,[gasul_rider_premium]")
                    .AppendLine("	,[gasul_product_seq]")
                    .AppendLine("	,[renewal_yn]")
                    .AppendLine("	,[in_date])")
                    .AppendLine("VALUES")
                    .AppendLine("	(@contract_seq")
                    .AppendLine("	,@good_item_dtal")
                    .AppendLine("	,@good_item_item")
                    .AppendLine("	,@jgyty_code")
                    .AppendLine("	,@insured_seq")
                    .AppendLine("	,@contract_amount")
                    .AppendLine("	,@expire_year")
                    .AppendLine("	,@gasul_rider_premium")
                    .AppendLine("	,@gasul_product_seq")
                    .AppendLine("	,@renewal_yn")
                    .AppendLine("	,GETDATE())");

                arParams = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@contract_seq", cd.contract_seq),
                    new System.Data.SqlClient.SqlParameter("@good_item_dtal", cd.good_item_dtal),
                    new System.Data.SqlClient.SqlParameter("@good_item_item", cd.good_item_item),
                    new System.Data.SqlClient.SqlParameter("@jgyty_code", cd.jgyty_code),
                    new System.Data.SqlClient.SqlParameter("@insured_seq", cd.insured_seq),
                    new System.Data.SqlClient.SqlParameter("@contract_amount", cd.contract_amount),
                    new System.Data.SqlClient.SqlParameter("@expire_year", cd.expire_year),
                    new System.Data.SqlClient.SqlParameter("@gasul_rider_premium", cd.gasul_rider_premium),  //보험료
                    new System.Data.SqlClient.SqlParameter("@gasul_product_seq", cd.gasul_product_seq),  //표준가설 상품번호
                    new System.Data.SqlClient.SqlParameter("@renewal_yn", cd.renewal_yn)  //갱신형 여부(화면에서 무조건 N 넘김)     

                };

            }
            else  //기계약 혹은 일반가설 인경우
            {

                sb.AppendLine("INSERT INTO [tb_contract_detail]")
                  .AppendLine("	([contract_seq]")
                  .AppendLine("	,[good_item_dtal]")
                  .AppendLine("	,[good_item_item]")
                  .AppendLine("	,[jgyty_code]")
                  .AppendLine("	,[insured_seq]")
                  .AppendLine("	,[contract_amount]")
                  .AppendLine("	,[expire_year]")
                  .AppendLine("	,[gasul_rider_premium]")
                  .AppendLine("	,[renewal_yn]")
                  .AppendLine("	,[in_date])")
                  .AppendLine("VALUES")
                  .AppendLine("	(@contract_seq")
                  .AppendLine("	,@good_item_dtal")
                  .AppendLine("	,@good_item_item")
                  .AppendLine("	,@jgyty_code")
                  .AppendLine("	,@insured_seq")
                  .AppendLine("	,@contract_amount")
                  .AppendLine("	,@expire_year")
                  .AppendLine("	,@gasul_rider_premium")
                  .AppendLine("	,@renewal_yn")
                  .AppendLine("	,GETDATE())");

                arParams = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@contract_seq", cd.contract_seq),
                    new System.Data.SqlClient.SqlParameter("@good_item_dtal", cd.good_item_dtal),
                    new System.Data.SqlClient.SqlParameter("@good_item_item", cd.good_item_item),
                    new System.Data.SqlClient.SqlParameter("@jgyty_code", cd.jgyty_code),
                    new System.Data.SqlClient.SqlParameter("@insured_seq", cd.insured_seq),
                    new System.Data.SqlClient.SqlParameter("@contract_amount", cd.contract_amount),
                    new System.Data.SqlClient.SqlParameter("@expire_year", cd.expire_year),
                    new System.Data.SqlClient.SqlParameter("@gasul_rider_premium", cd.gasul_rider_premium), //보험료
                    new System.Data.SqlClient.SqlParameter("@renewal_yn", cd.renewal_yn) //갱신형 여부(화면에서 Y 혹은 N 넘김)
                };

            }



            try
            {
                SqlHelper.ExecuteNonQuery(Common.Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
