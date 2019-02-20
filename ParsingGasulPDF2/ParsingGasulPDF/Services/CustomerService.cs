using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using ParsingGasulPDF.Common;
using ParsingGasulPDF.Models;

namespace ParsingGasulPDF.Services
{
        public class CustomerService
        {
            //고객 목록을 구합니다.
            public DataTable getList(String consultant_id, String customer_name)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT")
                    .AppendLine("	[customer_seq]")
                    .AppendLine("	,[consultant_id]")
                    .AppendLine("	,[family_seq]")
                    .AppendLine("	,[customer_name]")
                    .AppendLine("	,[birthday]")
                    .AppendLine("	,[insu_age]=-1") //보험나이(최초에는 -1로 계산전)
                    .AppendLine("	,[insu_birthday]='2000-01-01'") //상년월(최초 2000-01-01 설정)
                    .AppendLine("	,[gender]")
                    .AppendLine("	,[mobile_tel]")
                    .AppendLine("	,[email]")
                    .AppendLine("	,[relation]")
                    .AppendLine("	,[in_date]")
                    .AppendLine("FROM")
                    .AppendLine("	[tb_customer]")
                    .AppendLine("WHERE")
                    .AppendLine("	[consultant_id] = @consultant_id");

                if (false == (customer_name == null)) //고객명이 있으면 이름 검색 실시
                {
                    sb.AppendLine("	AND [customer_name] LIKE '%'+@customer_name+'%'");
                }
                sb.AppendLine("ORDER BY customer_name ASC");

                List<System.Data.SqlClient.SqlParameter> arParams = new List<System.Data.SqlClient.SqlParameter>();
                arParams.Add(new System.Data.SqlClient.SqlParameter("@consultant_id", consultant_id));

                if (false == (customer_name == null))
                {
                    arParams.Add(new System.Data.SqlClient.SqlParameter("@customer_name", customer_name));
                }

                DataTable dt = new DataTable();
                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams.ToArray());
                    dt = ds.Tables[0];


                    //결과값 dt에 보험나이 추가해서 반환하기
                    getListByFamilySeqAddInsuAge(dt);
                }
                catch (Exception e)
                {
                    throw e;
                }

                return dt;
            }

            //가족 목록을 구합니다.
            public DataTable getListByFamilySeq(String consultant_id, int family_seq)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT")
                    .AppendLine("	[customer_seq]")
                    .AppendLine("	,[consultant_id]")
                    .AppendLine("	,[family_seq]")
                    .AppendLine("	,[customer_name]")
                    .AppendLine("	,[birthday]")
                    .AppendLine("	,[insu_age]=-1") //보험나이(최초에는 -1로 계산전)
                    .AppendLine("	,[insu_birthday]='2000-01-01'") //상년월(최초 2000-01-01 설정)
                    .AppendLine("	,[gender]")
                    .AppendLine("	,[mobile_tel]")
                    .AppendLine("	,[email]")
                    .AppendLine("	,[relation]")
                    .AppendLine("	,[in_date]")
                    .AppendLine("FROM")
                    .AppendLine("	[tb_customer]")
                    .AppendLine("WHERE")
                    .AppendLine("	[consultant_id] = @consultant_id")
                    .AppendLine("	AND [family_seq] = @family_seq")
                    .AppendLine("ORDER BY [relation] ASC,birthday ");


                System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[]
                {
                new System.Data.SqlClient.SqlParameter("@consultant_id", consultant_id),
                new System.Data.SqlClient.SqlParameter("@family_seq", family_seq)
                };

                DataTable dt = new DataTable();
                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];

                    //결과값 dt에 보험나이 추가해서 반환하기
                    getListByFamilySeqAddInsuAge(dt);


                }
                catch (Exception e)
                {
                    throw e;
                }

                return dt;
            }

            //가족정보에 보험나이를 포함시킴
            public DataTable getListByFamilySeqAddInsuAge(DataTable dt)
            {
                List<Customer> family = new List<Customer>();

                //보험나이를 세팅시킴
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Customer cus = new Customer();
                            cus.customer_seq = int.Parse(dt.Rows[i]["customer_seq"].ToString().Trim());
                            cus.consultant_id = dt.Rows[i]["consultant_id"].ToString().Trim();
                            cus.family_seq = int.Parse(dt.Rows[i]["family_seq"].ToString().Trim());
                            cus.customer_name = dt.Rows[i]["customer_name"].ToString().Trim();
                            cus.birthday = DateTime.ParseExact(dt.Rows[i]["birthday"].ToString().Trim().Replace("-", "").Substring(0, 8), "yyyyMMdd", null);
                            cus.gender = dt.Rows[i]["gender"].ToString().Trim();
                            cus.mobile_tel = dt.Rows[i]["mobile_tel"].ToString().Trim();
                            cus.email = dt.Rows[i]["email"].ToString().Trim();
                            cus.relation = dt.Rows[i]["relation"].ToString().Trim();
                            cus.in_date = dt.Rows[i]["in_date"].ToString().Trim();
                            family.Add(cus);
                        }

                        int k = 0;
                        foreach (Customer c in family)
                        {
                            dt.Rows[k]["insu_age"] = c.getInsuAgeByDate(DateTime.Now);  //보험나이
                            dt.Rows[k]["insu_birthday"] = c.getInsuBirthDay().ToString("MM월 dd일");       //상년월
                            k = k + 1;
                        }
                    }
                }
                return dt;
            }

            //고객의 상세 정보를 구합니다.
            public Customer getDetail(int customer_seq)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT")
                    .AppendLine("	[customer_seq]")
                    .AppendLine("	,[consultant_id]")
                    .AppendLine("	,[family_seq]")
                    .AppendLine("	,[customer_name]")
                    .AppendLine("	,[birthday]")
                    .AppendLine("	,[insu_age]=-1") //보험나이(최초에는 -1로 계산전)
                    .AppendLine("	,[insu_birthday]='2000-01-01'") //상년월(최초 2000-01-01 설정)
                    .AppendLine("	,[gender]")
                    .AppendLine("	,[mobile_tel]")
                    .AppendLine("	,[email]")
                    .AppendLine("	,[relation]")
                    .AppendLine("	,[in_date]")
                    .AppendLine("FROM")
                    .AppendLine("	[tb_customer]")
                    .AppendLine("WHERE")
                    .AppendLine("	[customer_seq] = @customer_seq");

                System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[]
                {
                new System.Data.SqlClient.SqlParameter("@customer_seq", customer_seq)
                };

                DataTable dt = new DataTable();
                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];

                    //결과값 dt에 보험나이 추가해서 반환하기
                    getListByFamilySeqAddInsuAge(dt);
                }
                catch (Exception e)
                {
                    throw e;
                }
                Customer c = Customer.buildInsuFromDataRow(dt.Rows[0]);
                return c;
            }

            //가족을 저장합니다.(n명의 고객)
            public void setFamily(List<Customer> family)
            {
                int family_seq = 0;
                foreach (Models.Customer customer in family) //본인 먼저 저장하여 family_seq를 구한다.
                {
                    if (customer.relation.Equals("1")) //본인이면
                    {
                        family_seq = setCustomer(customer);
                    }
                }
                foreach (Models.Customer customer in family)
                {
                    if (!customer.relation.Equals("1")) //본인이 아니면
                    {
                        customer.family_seq = family_seq;
                        setCustomer(customer);
                    }
                }
            }

            //고객을 저장합니다.
            public int setCustomer(Customer customer)
            {
                StringBuilder sb = new StringBuilder();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

                if (customer.relation.Equals("1")) //본인인 경우
                {
                    sb.AppendLine("DECLARE @family_seq int;")
                        .AppendLine("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE")
                        .AppendLine("BEGIN TRAN")
                        .AppendLine("	SET NOCOUNT ON")
                        .AppendLine("	SELECT")
                        .AppendLine("		@family_seq = isnull(MAX([family_seq]),0) + 1")
                        .AppendLine("	FROM")
                        .AppendLine("		[tb_customer]")
                        .AppendLine("   INSERT INTO [tb_customer]")
                        .AppendLine("	    ([consultant_id]")
                        .AppendLine("   	,[family_seq]")
                        .AppendLine("	    ,[customer_name]")
                        .AppendLine("	    ,[birthday]")
                        .AppendLine("	    ,[gender]")
                        .AppendLine("	    ,[mobile_tel]")
                        .AppendLine("	    ,[email]")
                        .AppendLine("	    ,[relation])")
                        .AppendLine("   VALUES")
                        .AppendLine("   	(@consultant_id")
                        .AppendLine("	    ,@family_seq")
                        .AppendLine("	    ,@customer_name")
                        .AppendLine("	    ,@birthday")
                        .AppendLine("	    ,@gender")
                        .AppendLine("   	,@mobile_tel")
                        .AppendLine("	    ,@email")
                        .AppendLine("	    ,@relation)")
                        .AppendLine("	SELECT @family_seq")
                        .AppendLine("IF @@ERROR = 0")
                        .AppendLine("	COMMIT TRAN")
                        .AppendLine("ELSE")
                        .AppendLine("	ROLLBACK");

                    arParams = new System.Data.SqlClient.SqlParameter[]
                    {
                    new System.Data.SqlClient.SqlParameter("@consultant_id", customer.consultant_id),
                    //new System.Data.SqlClient.SqlParameter("@family_seq", customer.family_seq),
                    new System.Data.SqlClient.SqlParameter("@customer_name", customer.customer_name),
                    new System.Data.SqlClient.SqlParameter("@birthday", customer.birthday),
                    new System.Data.SqlClient.SqlParameter("@gender", customer.gender),
                    new System.Data.SqlClient.SqlParameter("@mobile_tel", customer.mobile_tel),
                    new System.Data.SqlClient.SqlParameter("@email", customer.email),
                    new System.Data.SqlClient.SqlParameter("@relation", customer.relation)
                    };
                }
                else //본인 이외의 구성원인 경우
                {
                    sb.AppendLine("   INSERT INTO [tb_customer]")
                        .AppendLine("    ([consultant_id]")
                        .AppendLine("  	,[family_seq]")
                        .AppendLine("    ,[customer_name]")
                        .AppendLine("    ,[birthday]")
                        .AppendLine("    ,[gender]")
                        .AppendLine("    ,[mobile_tel]")
                        .AppendLine("    ,[email]")
                        .AppendLine("    ,[relation])")
                        .AppendLine("  VALUES")
                        .AppendLine("  	(@consultant_id")
                        .AppendLine("    ,@family_seq")
                        .AppendLine("    ,@customer_name")
                        .AppendLine("    ,@birthday")
                        .AppendLine("    ,@gender")
                        .AppendLine("  	 ,@mobile_tel")
                        .AppendLine("    ,@email")
                        .AppendLine("    ,@relation)")
                        .AppendLine("	SELECT @family_seq");

                    arParams = new System.Data.SqlClient.SqlParameter[]
                    {
                    new System.Data.SqlClient.SqlParameter("@consultant_id", customer.consultant_id),
                    new System.Data.SqlClient.SqlParameter("@family_seq", customer.family_seq),
                    new System.Data.SqlClient.SqlParameter("@customer_name", customer.customer_name),
                    new System.Data.SqlClient.SqlParameter("@birthday", customer.birthday),
                    new System.Data.SqlClient.SqlParameter("@gender", customer.gender),
                    new System.Data.SqlClient.SqlParameter("@mobile_tel", customer.mobile_tel),
                    new System.Data.SqlClient.SqlParameter("@email", customer.email),
                    new System.Data.SqlClient.SqlParameter("@relation", customer.relation)
                    };
                }

                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];
                    //SqlHelper.ExecuteNonQuery(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                }
                catch (Exception e)
                {
                    throw e;
                }
                return Convert.ToInt32(dt.Rows[0][0]);
            }

            //고객을 저장 합니다...중복된 기능이라 필요 없을것 처럼 보이지만, 고객 수정시 신규 생성된 고객들을 처리하기 위해서 반드시 필요함 메서드임...
            public void setCustomers(List<Customer> family)
            {
                foreach (Models.Customer customer in family)
                {
                    setCustomer(customer);
                }
            }

            //고객을 수정 합니다.
            public void editCustomer(Customer customer)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("UPDATE [tb_customer]")
                    .AppendLine("   SET [customer_name] = @customer_name")
                    .AppendLine("      ,[birthday] = @birthday")
                    .AppendLine("      ,[gender] = @gender")
                    .AppendLine("      ,[mobile_tel] = @mobile_tel")
                    .AppendLine("      ,[email] = @email")
                    .AppendLine("      ,[relation] = @relation")
                    .AppendLine("WHERE")
                    .AppendLine("	[customer_seq] = @customer_seq");

                System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[]
                {
                new System.Data.SqlClient.SqlParameter("@customer_name", customer.customer_name),
                new System.Data.SqlClient.SqlParameter("@birthday", customer.birthday),
                new System.Data.SqlClient.SqlParameter("@gender", customer.gender),
                new System.Data.SqlClient.SqlParameter("@mobile_tel", customer.mobile_tel),
                new System.Data.SqlClient.SqlParameter("@email", customer.email),
                new System.Data.SqlClient.SqlParameter("@relation", customer.relation),
                new System.Data.SqlClient.SqlParameter("@customer_seq", customer.customer_seq)
                };

                try
                {
                    SqlHelper.ExecuteNonQuery(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //고객들을 수정 합니다.
            public void editCustomers(List<Customer> family)
            {
                StringBuilder sb = new StringBuilder();

                foreach (Models.Customer customer in family)
                {
                    sb.AppendLine("UPDATE [tb_customer]")
                        .AppendLine("   SET [customer_name] = @customer_name")
                        .AppendLine("      ,[birthday] = @birthday")
                        .AppendLine("      ,[gender] = @gender")
                        .AppendLine("      ,[mobile_tel] = @mobile_tel")
                        .AppendLine("      ,[email] = @email")
                        .AppendLine("      ,[relation] = @relation")
                        //.AppendLine("      ,[in_date] = GETDATE()")
                        .AppendLine("WHERE")
                        .AppendLine("	[customer_seq] = @customer_seq");

                    System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[]
                    {
                    new System.Data.SqlClient.SqlParameter("@customer_name", customer.customer_name),
                    new System.Data.SqlClient.SqlParameter("@birthday", customer.birthday),
                    new System.Data.SqlClient.SqlParameter("@gender", customer.gender),
                    new System.Data.SqlClient.SqlParameter("@mobile_tel", customer.mobile_tel),
                    new System.Data.SqlClient.SqlParameter("@email", customer.email),
                    new System.Data.SqlClient.SqlParameter("@relation", customer.relation),
                    new System.Data.SqlClient.SqlParameter("@customer_seq", customer.customer_seq)
                    };

                    try
                    {
                        SqlHelper.ExecuteNonQuery(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            //가족을 삭제 합니다.
            public void delFamily(String consultant_id, int family_seq)
            {
                DataTable dt = getListByFamilySeq(consultant_id, family_seq);
                foreach (DataRow dr in dt.Rows)
                {
                    delCustomer(Customer.buildFromDataRow(dr));
                }
            }

            //고객을 삭제 합니다.
            public void delCustomer(Customer customer)
            {
                StringBuilder sb = new StringBuilder();
                //1. 보장내역 삭제
                //2. 가입한 특약 삭제
                //3. 주피로 되어 있는 증권의 상세 주/특약 삭제
                //4. 주피로 되어 있는 증권의 기본 정보 삭제
                //5. 미등록 타사 삭제
                //6. 고객 정보 삭제
                sb.AppendLine("DELETE FROM [tb_coverage] WHERE customer_seq = @customer_seq;")
                    .AppendLine("DELETE FROM [tb_contract_detail] WHERE insured_seq = @customer_seq;")
                    .AppendLine("DELETE FROM [tb_contract_detail] WHERE contract_seq IN (SELECT contract_seq FROM [tb_contract] WHERE insured_seq = @customer_seq);")
                    .AppendLine("DELETE FROM [tb_contract] WHERE insured_seq = @customer_seq;")
                    .AppendLine("DELETE FROM [tb_contract_unregistered] WHERE [insured_seq] = @customer_seq;")
                    .AppendLine("DELETE FROM [tb_contract_detail] WHERE insured_seq = @customer_seq;")
                    .AppendLine("DELETE FROM [tb_customer] WHERE [customer_seq] = @customer_seq;");

                System.Data.SqlClient.SqlParameter[] arParams1 = new System.Data.SqlClient.SqlParameter[]
                {
                new System.Data.SqlClient.SqlParameter("@customer_seq", customer.customer_seq)
                };

                try
                {
                    SqlHelper.ExecuteNonQuery(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams1);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //고객들을 삭제 합니다.
            public void delCustomers(List<Customer> family)
            {
                foreach (Models.Customer customer in family)
                {
                    delCustomer(customer);
                }
            }

            //계약/상세계약 정보가 있어서 삭제 불가능한 고객인지 확인합니다.
            public Boolean isCanDeleteCustomer(int customer_seq)
            {
                //해당고객의 계약/상세계약 정보가 있는지 파악합니다.
                StringBuilder sb = new StringBuilder();
                DataTable dt = new DataTable();
                Boolean hasContractData = true;

                sb.AppendLine("SELECT")
                    .AppendLine("	COUNT(*)")
                    .AppendLine("FROM")
                    .AppendLine("	[tb_contract_detail]")
                    .AppendLine("WHERE")
                    .AppendLine("	[insured_seq] = @customer_seq")
                    .AppendLine("")
                    .AppendLine("SELECT")
                    .AppendLine("	COUNT(*)")
                    .AppendLine("FROM")
                    .AppendLine("	[tb_contract]")
                    .AppendLine("WHERE")
                    .AppendLine("	[insured_seq] = @customer_seq");

                System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[]
                {
                new System.Data.SqlClient.SqlParameter("@customer_seq", customer_seq)
                };

                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];
                    if (0 < Convert.ToInt32(ds.Tables[0].Rows[0][0]) || 0 < Convert.ToInt32(ds.Tables[1].Rows[0][0]))
                    {
                        hasContractData = true; //종속된 데이터가 존재하면
                    }
                    {
                        hasContractData = false; //종속된 데이터가 없으면
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return hasContractData;
            }

            public Customer getCustomerByCustomerSeq(int customer_seq)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT")
                    .AppendLine("	[customer_seq]")
                    .AppendLine("	,[consultant_id]")
                    .AppendLine("	,[family_seq]")
                    .AppendLine("	,[customer_name]")
                    .AppendLine("	,[birthday]")
                    .AppendLine("	,[insu_age]=-1") //보험나이(최초에는 -1로 계산전)
                    .AppendLine("	,[insu_birthday]='2000-01-01'") //상년월(최초 2000-01-01 설정)
                    .AppendLine("	,[gender]")
                    .AppendLine("	,[mobile_tel]")
                    .AppendLine("	,[email]")
                    .AppendLine("	,[relation]")
                    .AppendLine("	,[in_date]")
                    .AppendLine("FROM")
                    .AppendLine("	[tb_customer]")
                    .AppendLine("WHERE")
                    .AppendLine("	[customer_seq] = @customer_seq");

                System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[]
                {
                new System.Data.SqlClient.SqlParameter("@customer_seq", customer_seq)
                };

                DataTable dt = new DataTable();
                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Core.DBConnectionString, CommandType.Text, sb.ToString(), arParams);
                    dt = ds.Tables[0];

                    //결과값 dt에 보험나이 추가해서 반환하기
                    getListByFamilySeqAddInsuAge(dt);

                }
                catch (Exception e)
                {
                    throw e;
                }
                Customer c = Customer.buildInsuFromDataRow(dt.Rows[0]);
                return c;
            }


        }

}
