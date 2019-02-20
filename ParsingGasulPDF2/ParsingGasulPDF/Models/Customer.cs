using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ParsingGasulPDF.Common;

namespace ParsingGasulPDF.Models
{
    public class Customer
    {
        public int customer_seq { get; set; }

        public String consultant_id { get; set; }

        public int family_seq { get; set; }

        public String customer_name { get; set; }

        public DateTime birthday { get; set; }

        public int insu_age { get; set; }

        public String insu_birthday { get; set; }

        public String gender { get; set; }

        public String mobile_tel { get; set; }

        public String email { get; set; }

        public String relation { get; set; }

        public String in_date { get; set; }

        public static Customer buildFromDataRow(DataRow dr)
        {
            Customer c = new Customer();
            c.customer_seq = Convert.ToInt32(dr["customer_seq"]);
            c.consultant_id = Convert.ToString(dr["consultant_id"]);
            c.family_seq = Convert.ToInt32(dr["family_seq"]);
            c.customer_name = Convert.ToString(dr["customer_name"]);
            c.birthday = Convert.ToDateTime(dr["birthday"]);
            c.gender = Convert.ToString(dr["gender"]);
            c.mobile_tel = Convert.ToString(dr["mobile_tel"]);
            c.email = Convert.ToString(dr["email"]);
            c.relation = Convert.ToString(dr["relation"]);
            //c.in_date = Convert.ToDateTime(dr["in_date"]);  2012-11-16 재민 변경
            c.in_date = Convert.ToString(dr["in_date"]);
            return c;
        }

        //보험나이와 상령일 포함
        public static Customer buildInsuFromDataRow(DataRow dr)
        {
            Customer c = new Customer();
            c.customer_seq = Convert.ToInt32(dr["customer_seq"]);
            c.consultant_id = Convert.ToString(dr["consultant_id"]);
            c.family_seq = Convert.ToInt32(dr["family_seq"]);
            c.customer_name = Convert.ToString(dr["customer_name"]);
            c.birthday = Convert.ToDateTime(dr["birthday"]);
            c.insu_age = Convert.ToInt32(dr["insu_age"]);
            c.insu_birthday = Convert.ToString(dr["insu_birthday"]);
            c.gender = Convert.ToString(dr["gender"]);
            c.mobile_tel = Convert.ToString(dr["mobile_tel"]);
            c.email = Convert.ToString(dr["email"]);
            c.relation = Convert.ToString(dr["relation"]);
            //c.in_date = Convert.ToDateTime(dr["in_date"]);  2012-11-16 재민 변경
            c.in_date = Convert.ToString(dr["in_date"]);
            return c;
        }

        public static String getRelationText(String relationCode)
        {
            String[] relationText = new String[] { "본인", "배우자", "자녀", "부", "모", "조부", "조모" };
            return relationText[Convert.ToInt16(relationCode) - 1];
        }

        public static String getGenderText(String gender)
        {
            return (gender.ToLower() == "m") ? "남자" : "여자";
        }

        //상령월을 구합니다.
        public DateTime getInsuBirthDay()
        {
            DateTime insuBirthdate = this.birthday.AddMonths(-6);
            //생일이 29,30,31일이고 6개월 이전의 달이 해당일이 존재하지 않으면 무조건 다음달 1일이 상령일임
            if (this.birthday.Day != insuBirthdate.Day)
            {
                insuBirthdate = insuBirthdate.AddDays(1);
            }
            return insuBirthdate;
        }

        //보험나이를 구합니다.
        public int getInsuAgeByDate(DateTime nowDate)
        {
            //최초 상령일을 구합니다.
            DateTime insuBirthdate = this.getInsuBirthDay();

            //년도차이를 구합니다.
            int age = nowDate.Year - insuBirthdate.Year;

            //해당년도 상령일을 구합니다.
            DateTime criteriaDate = Common.Utility.getSafeDate(nowDate.Year, insuBirthdate);

            if (nowDate < criteriaDate) //기준일이 상령일 이전이면 12개월을 채우지 못했으므로 1살을 뺍니다.
            {
                age--;
            }
            return age;
        }

        //보험나이로 해당년을 구합니다.
        public int getYearByInsuAge(DateTime nowDate, int insuAge)
        {
            //현재월기준 보험나이를 구합니다.
            int nowInsuAge = this.getInsuAgeByDate(nowDate);
            int gap = insuAge - nowInsuAge;
            return nowDate.Year + gap;
        }
        //현재나이를 구한다.
        public int getAgeByDate(DateTime nowDate)
        {
            int age = nowDate.Year - this.birthday.Year;

            return age;
        }

        //본인 또는 배우자를 구합니다.
        public static Customer getCustomerByRelation(List<Customer> customers, String relation)
        {
            foreach (Customer c in customers)
            {
                if (c.relation == relation)
                    return c;
            }
            return null;
        }

        //가족형일경우 본인 개인형 일경우 자신을 구합니다.
        public static Customer getHimself(List<Customer> customers)
        {
            Customer himself = null;
            if (customers.Count == 1)
            {
                himself = customers[0];
            }
            else
            {
                foreach (Customer c in customers)
                {
                    if (c.relation == "1")
                        himself = c;
                }
            }
            return himself;
        }

        //연령대를 구합니다.
        public int getAgeTerm(DateTime nowDate)
        {
            int InsuAge = getInsuAgeByDate(nowDate);
            InsuAge = InsuAge / 10;
            return InsuAge * 10;
        }

        //부부가 존재하는 가족인지 확인합니다.
        public static Boolean isCouple(List<Customer> customers)
        {
            Boolean isCouple = false;
            foreach (var item in customers)
            {
                if (item.relation == "2")
                    isCouple = true;
            }
            return isCouple;
        }
    }
}
