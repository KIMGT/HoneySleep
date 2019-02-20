using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Threading.Tasks;

namespace ParsingGasulPDF.Models
{

    public class ContractDetail
    {
        public ContractDetail()
        {
        }

        public ContractDetail(ContractDetail other)
        {
            detail_seq = other.detail_seq;
            contract_seq = other.contract_seq;
            good_item_dtal = other.good_item_dtal;
            good_item_item = other.good_item_item;
            jgyty_code = other.jgyty_code;
            insured_seq = other.insured_seq;
            contract_amount = other.contract_amount;
            pay_end_year = other.pay_end_year;
            expire_year = other.expire_year;
            in_date = other.in_date;
            gasul_rider_premium = other.gasul_rider_premium;
            renewal_yn = other.renewal_yn;
        }


        public int detail_seq { get; set; }

        public int contract_seq { get; set; }

        public String good_item_dtal { get; set; }

        public String good_item_item { get; set; }

        public String jgyty_code { get; set; }

        public int insured_seq { get; set; }

        public Double contract_amount { get; set; }

        public String expire_year { get; set; }

        public String pay_end_year { get; set; }

        public DateTime in_date { get; set; }

        public Double gasul_rider_premium { get; set; }

        public int gasul_product_seq { get; set; }

        public String renewal_yn { get; set; }


        public void validate()
        {
            //필수값을 체크 합니다.
            if (good_item_dtal == null)
            {
                throw new Exception("good_item_dtal 이 null 입니다.");
            }
            if (good_item_item == null)
            {
                throw new Exception("good_item_item 이 null 입니다.");
            }
            if (jgyty_code == null)
            {
                throw new Exception("jgyty_code 이 null 입니다.");
            }
            if (expire_year == null)
            {
                throw new Exception("expire_year 이 null 입니다.");
            }
            if (renewal_yn == null)
            {
                throw new Exception("renewal_yn 이 null 입니다.");
            }
        }
    }
}
