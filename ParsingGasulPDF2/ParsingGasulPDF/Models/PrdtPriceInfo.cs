using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingGasulPDF.Models
{
    class PrdtPriceInfo
    {
        public PrdtPriceInfo()
        {

        }

        public String compy_cd { get; set; }

        public String prdt_cd { get; set; }

        public String insur_cd { get; set; }

        public String insur_nm { get; set; }

        public String sex { get; set; }

        public int age { get; set; }

        public String pay_term { get; set; }

        public String expiration { get; set; }

        public int std_contract_amt { get; set; }

        public Double premium { get; set; }

        public String renewal_yn {get;set;}

        public int renewal_pd { get; set; }

        public String notice_yn { get; set; }
    }
}
