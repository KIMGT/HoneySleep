using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ParsingGasulPDF.Models
{
    public class Contract
    {
        public int contract_seq { get; set; }

        public String consultant_id { get; set; }

        public String good_company { get; set; }

        public String good_seq { get; set; }

        public String good_name { get; set; }

        public String contract_name { get; set; }

        public int insured_seq { get; set; }

        public Int64 premium { get; set; }

        public DateTime contract_date { get; set; }

        public String pay_term { get; set; }

        public String expire_year { get; set; }

        public String interest_rate { get; set; }

        public String pay_end_year { get; set; }

        public String keep { get; set; }

        public String proposal { get; set; }

        public int gasul_product_seq { get; set; }

        public DateTime in_date { get; set; }

    }
}
