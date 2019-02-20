using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace ParsingGasulPDF.Services
{
    class CommonService
    {
        public static DataTable get손보회사들()
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine("  select th_code as 회사코드,sm as 회사명  from TB_MATLPCDP ")
                .AppendLine("  where th_code between '0170' and '0178' or th_code = '0184' ")
                .AppendLine("  order by 회사명 ");

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


        public static DataTable get플랜()
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine("  select PlanID as cd, attr2 + '-' + PlanName as name  from TB_TIC_PlanM ")
                .AppendLine("  where use_yn = 'Y' ")
                .AppendLine("  order by PlanID ");

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

        public static DataTable get상품들(String s상품구분)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine("  select compy_cd + prdt_cd as cd, compy_cd + prdt_nm as name from TB_PARSING_PRDT ")
                .AppendLine("  where use_yn = 'Y' ")
                .AppendLine("  and SUBSTRING(prdt_cd, 5, 2) = @prdt_gb ");

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@prdt_gb", s상품구분)
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

        public static DataTable get담보들(String s회사코드,String s상품코드)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine("  select insur_cd , insur_nm from TB_TIC_PRDT_D ")
                .AppendLine("  where use_yn = 'Y' ")
                .AppendLine("  and compy_cd =  @compy_cd ")
               .AppendLine("  and prdt_cd =  @prdt_cd ");
            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@compy_cd", s회사코드),
                    new System.Data.SqlClient.SqlParameter("@prdt_cd", s상품코드),
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


        public static DataTable get상품들by플랜(String s플랜)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
                .AppendLine("SET NOCOUNT ON")
                .AppendLine("  select compy_cd + prdt_cd as cd, compy_cd + prdt_name as name  from TB_TIC_Plan_PRDT ")
                .AppendLine("  where use_yn = 'Y' ")
                .AppendLine("  and PlanID = @planid ");

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@planid", s플랜)
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


        public static DataTable get상품파싱정보(String compy_cd, String prdt_cd,String gender,int age,String s삭제구분)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            sb.AppendLine("SET NOCOUNT ON");
            sb.AppendLine("  select  ");
            sb.AppendLine("  	a.compy_cd,a.prdt_cd,a.insur_cd, a.insur_nm,a.insur_disp_nm, notice_yn,embed_string, ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.st_string else  c.st_string end as st_string, ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.ed_string else  c.ed_string end as ed_string, ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.parsing_type else c.parsing_type end as parsing_type, ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.contract_amt_st_str else  c.contract_amt_st_str end as contract_amt_st_str, ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.premium_st_str else  c.premium_st_str end as premium_st_str, ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.except_string else  c.except_string end as except_string,  ");
            sb.AppendLine("  	case when c.insur_cd is null  then a.del_string else  c.del_string end as del_string,  ");
            sb.AppendLine("  	case when d.insur_cd is null  then a.renewal_yn else  d.renewal_yn end as renewal_yn, ");
            sb.AppendLine("  	case when d.insur_cd is null  then a.renewal_pd else  d.renewal_pd end as renewal_pd, ");
            sb.AppendLine("  	isnull(b.cnt,0) as plancnt ");
            sb.AppendLine("  from  ");
            sb.AppendLine("  	TB_PARSING_PRDT_DETAIL  a ");
            sb.AppendLine("  	left join ( ");
            sb.AppendLine("  		select compy_cd,prdt_cd,insur_cd,min(PlanID) as min_planid,count(insur_cd)  as cnt ");
            sb.AppendLine("  		from TB_TIC_Plan_PRDT_D ");
            sb.AppendLine("  		where  compy_cd = @compy_cd and prdt_cd  = @prdt_cd ");
            sb.AppendLine(gender.Equals("X") ? " and sex in ( @sex ) " : " and sex in ( @sex,'MF') ");
            sb.AppendLine("  			   and  (st_age <= @age and @age <= ed_age) ");
            sb.AppendLine("  		group by compy_cd,prdt_cd,insur_cd ");
            sb.AppendLine("  	) as b ");
            sb.AppendLine("  	on a.compy_cd = b.compy_cd ");
            sb.AppendLine("  	and a.prdt_cd = b.prdt_cd ");
            sb.AppendLine("  	and a.insur_cd = b.insur_cd ");
            sb.AppendLine("  	left join ( ");
            sb.AppendLine("  		select compy_cd,prdt_cd,insur_cd, st_string,ed_string,parsing_type,renewal_yn,renewal_pd ,contract_amt_st_str ,premium_st_str ,except_string,del_string  ");
            sb.AppendLine("  		from TB_PARSING_PRDT_DETAIL_EXT ");
            sb.AppendLine("  		where  compy_cd = @compy_cd and prdt_cd  = @prdt_cd ");
            sb.AppendLine(gender.Equals("X") ? " and sex in ( @sex ) " : " and sex in ( @sex,'MF') ");
            sb.AppendLine("  			   and  (st_age <= @age and @age <= ed_age) ");
            sb.AppendLine("  	) as c ");
            sb.AppendLine("  	on a.compy_cd = c.compy_cd ");
            sb.AppendLine("  	and a.prdt_cd = c.prdt_cd ");
            sb.AppendLine("  	and a.insur_cd = c.insur_cd ");
            sb.AppendLine("  	left join ( ");
            sb.AppendLine("  		select compy_cd,prdt_cd,insur_cd, renewal_yn,renewal_pd  ");
            sb.AppendLine("  		from TB_PARSING_PRDT_DETAIL_RENEWAL ");
            sb.AppendLine("  		where  compy_cd = @compy_cd and prdt_cd  = @prdt_cd ");
            sb.AppendLine("  			   and  (st_age <= @age and @age <= ed_age) ");
            sb.AppendLine("  	) as d ");
            sb.AppendLine("  	on a.compy_cd = d.compy_cd ");
            sb.AppendLine("  	and a.prdt_cd = d.prdt_cd ");
            sb.AppendLine("  	and a.insur_cd = d.insur_cd ");
            sb.AppendLine("  where a.compy_cd = @compy_cd and a.prdt_cd  = @prdt_cd "); 

            if (s삭제구분.Equals("부분삭제")){
                sb.AppendLine(" and input_gb = @input_gb");
            }
              

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@compy_cd", compy_cd),
                    new System.Data.SqlClient.SqlParameter("@prdt_cd", prdt_cd),
                    new System.Data.SqlClient.SqlParameter("@sex", gender),
                    new System.Data.SqlClient.SqlParameter("@age", age),
                    new System.Data.SqlClient.SqlParameter("@input_gb", s삭제구분)
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



        public static DataTable get추출담보정보(String compy_cd, String prdt_cd, String gender, int age)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            sb.AppendLine("  select   ");
            sb.AppendLine("  	  a.insur_cd as 담보코드,  ");
            sb.AppendLine("  	  a.insur_nm as 담보명,  ");
            sb.AppendLine("  	  pay_term as 납기,  ");
            sb.AppendLine("  	  expiration as  만기,  ");
            sb.AppendLine("  	  std_contract_amt as 가입금액,  ");
            sb.AppendLine("  	  premium as 보험료,  ");
            sb.AppendLine("  	  renewal_yn as 갱신여부,  ");
            sb.AppendLine("  	  renewal_pd as 갱신주기,  ");
            sb.AppendLine("  	  c.attr2 as 상품속성2,  ");
            sb.AppendLine("  	  c.seq,  ");
            sb.AppendLine("  	 (select CD_NAME from  TB_TIC_COMMCD where cd = a.compy_cd) as 회사명 ,  ");
            sb.AppendLine("  	  b.prdt_name  as 상품명 ,  ");
            sb.AppendLine("  	  a.sex as 성별,  ");
            sb.AppendLine("  	  a.age as 나이  ");
            sb.AppendLine("  from   ");
            sb.AppendLine("  TB_TIC_PRDT_PRICE a  ");
            sb.AppendLine("  join TB_TIC_PRDT b  ");
            sb.AppendLine("  	on a.compy_cd = b.compy_cd  ");
            sb.AppendLine("  	and a.prdt_cd = b.prdt_cd  ");
            sb.AppendLine("  left join  TB_TIC_PRDT_D c  ");
            sb.AppendLine("  	on a.compy_cd = c.compy_cd  ");
            sb.AppendLine("  	and a.prdt_cd = c.prdt_cd  ");
            sb.AppendLine("  	and a.insur_cd = c.insur_cd  ");
            sb.AppendLine("  where 1=1  ");
            sb.AppendLine(gender.Equals("X") ? " and sex in ( @sex ) " : " and a.sex in ( @sex,'MF') ");
            sb.AppendLine("  		and a.compy_cd = @compy_cd  ");
            sb.AppendLine("  		and a.prdt_cd = @prdt_cd  ");
            sb.AppendLine("  		and a.age = @age  ");
            sb.AppendLine("  		and b.use_yn = 'Y'   ");
            sb.AppendLine("  order by seq  "); 

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@compy_cd", compy_cd),
                    new System.Data.SqlClient.SqlParameter("@prdt_cd", prdt_cd),
                    new System.Data.SqlClient.SqlParameter("@sex", gender),
                    new System.Data.SqlClient.SqlParameter("@age", age)
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


        public static DataTable get연령별추출담보정보(String compy_cd, String prdt_cd, String insur_cd, String gender )
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            sb.AppendLine("  select   ");
            sb.AppendLine("  	  a.age as 나이,  ");
            sb.AppendLine("  	  premium as 보험료,  ");
            sb.AppendLine("  	  std_contract_amt as 가입금액,  ");
            sb.AppendLine("  	  expiration as  만기,  ");
            sb.AppendLine("  	  pay_term as 납기,  ");
            sb.AppendLine("  	  renewal_yn as 갱신여부,  ");
            sb.AppendLine("  	  renewal_pd as 갱신주기  ");
            sb.AppendLine("  from   ");
            sb.AppendLine("  TB_TIC_PRDT_PRICE a  ");
            sb.AppendLine("  join TB_TIC_PRDT b  ");
            sb.AppendLine("  	on a.compy_cd = b.compy_cd  ");
            sb.AppendLine("  	and a.prdt_cd = b.prdt_cd  ");
            sb.AppendLine("  left join  TB_TIC_PRDT_D c  ");
            sb.AppendLine("  	on a.compy_cd = c.compy_cd  ");
            sb.AppendLine("  	and a.prdt_cd = c.prdt_cd  ");
            sb.AppendLine("  	and a.insur_cd = c.insur_cd  ");
            sb.AppendLine("  where 1=1  ");
            sb.AppendLine(gender.Equals("X") ? " and sex in ( @sex ) " : " and a.sex in ( @sex,'MF') ");
            sb.AppendLine("  		and a.compy_cd = @compy_cd  ");
            sb.AppendLine("  		and a.prdt_cd = @prdt_cd  ");
            sb.AppendLine("  		and a.insur_cd = @insur_cd  ");
            sb.AppendLine("  		and b.use_yn = 'Y'   ");
            sb.AppendLine("  order by age  ");

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@compy_cd", compy_cd),
                    new System.Data.SqlClient.SqlParameter("@prdt_cd", prdt_cd),
                    new System.Data.SqlClient.SqlParameter("@sex", gender),
                    new System.Data.SqlClient.SqlParameter("@insur_cd", insur_cd)
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


        public static DataTable get플랜보험료비교(String plan_id,String compy_cd, String prdt_cd, String gender, int age)
        {
            //데이터를 읽어 온다.

            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] arParams = new System.Data.SqlClient.SqlParameter[] { };

            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            sb.AppendLine("   select  ");
            sb.AppendLine("   	  a.insur_cd as	 담보코드, ");
            sb.AppendLine("   	  isnull(b.insur_nm,'')  as  담보명, ");
            sb.AppendLine("   	  isnull(b.pay_term,'')  as 납기,");
            sb.AppendLine(" 	  isnull(b.expiration,'') as 만기 , ");
            sb.AppendLine("   	  a.st_contr_amt as 플랜가입금액, ");
            sb.AppendLine("   	  case when isnull(b.std_contract_amt,0) = 0 then 0 else  (a.st_contr_amt  * b.premium) / b.std_contract_amt end as 플랜보험금, ");
            sb.AppendLine(" 	  isnull(b.std_contract_amt,0) as 추출가입금액,");
            sb.AppendLine(" 	  isnull(b.premium,0)          as 추출보험료,");
            sb.AppendLine(" 	  a.st_contr_amt - isnull(b.std_contract_amt,0) as 플랜_추출가입금차액,");
            sb.AppendLine("  case when isnull(b.std_contract_amt,0) = 0 then  isnull(b.std_contract_amt,0) - isnull(b.premium,0)  else  ((a.st_contr_amt  * b.premium) / b.std_contract_amt)  - b.premium end  as 추출보험료차액, ");
            sb.AppendLine("   	  isnull(b.renewal_yn,'') as 갱신, ");
            sb.AppendLine("   	  isnull(b.renewal_pd,'') as 갱신기간, ");
            sb.AppendLine("   	  c.seq as 순번, ");
            sb.AppendLine("   	  isnull(c.attr2, '') as 예정");
            sb.AppendLine("  from ");
            sb.AppendLine("  	TB_TIC_Plan_PRDT_D a");
            sb.AppendLine("  	left join (");
            sb.AppendLine("  		select  compy_cd,prdt_cd,insur_cd,insur_nm,age,sex,pay_term,expiration,std_contract_amt,premium,renewal_yn,renewal_pd,notice_yn");
            sb.AppendLine("  		from TB_TIC_PRDT_PRICE");
            sb.AppendLine("  		where ");
            sb.AppendLine("  			 compy_cd = @compy_cd");
            sb.AppendLine("  			and prdt_cd = @prdt_cd");
            sb.AppendLine("  			and age = @age");
            sb.AppendLine(gender.Equals("X") ? " and sex in ( @sex ) " : " and sex in ( @sex,'MF') ");
            sb.AppendLine("  	) b");
            sb.AppendLine("  	on a.compy_cd = b.compy_cd");
            sb.AppendLine("  	and a.prdt_cd = b.prdt_cd");
            sb.AppendLine("  	and a.insur_cd = b.insur_cd");
            sb.AppendLine("  	left join TB_TIC_PRDT_D c");
            sb.AppendLine("  	on a.compy_cd = c.compy_cd");
            sb.AppendLine("  	and a.prdt_cd = c.prdt_cd");
            sb.AppendLine("  	and a.insur_cd = c.insur_cd");
            sb.AppendLine("   where 1=1 ");
            sb.AppendLine(gender.Equals("X") ? " and a.sex in ( @sex ) " : " and a.sex in ( @sex,'MF') ");
            sb.AppendLine("           and  (a.st_age <= @age and @age <= ed_age) ");
            sb.AppendLine("           and a.planid = @planId  ");
            sb.AppendLine("           and a.compy_cd = @compy_cd and a.prdt_cd = @prdt_cd ");
            sb.AppendLine("     order by 담보코드");

            arParams = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@planId", plan_id),
                    new System.Data.SqlClient.SqlParameter("@compy_cd", compy_cd),
                    new System.Data.SqlClient.SqlParameter("@prdt_cd", prdt_cd),
                    new System.Data.SqlClient.SqlParameter("@sex", gender),
                    new System.Data.SqlClient.SqlParameter("@age", age)
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
