
using System;
namespace IRAP.DataMigration {
     class TransferEntity {
        public decimal TRANSACTNO { get; set; }
        public string GRACNTNO { get; set; }
        public string SRCDWACNTNO { get; set; }
        public string DSTDWACNTNO { get; set; }
        public string REMARK { get; set; }
    }

    class DrawEntity {
        public decimal TRANSACTNO { get; set; }
        public string GRACNTNO { get; set; }
        public long GFLXID { get; set; }
        public string CONTRACTNO { get; set; }
        public string BANKCODE { get; set; }
        public string CARDNO { get; set; }
        public string GRNAME { get; set; }
        public string MOBILENO { get; set; }
        public string REMARK { get; set; }
        public double BJAMOUNT { get; set; }
        public double LXAMOUNT { get; set; } 
    }

    class DrawLoanContractEntity {
        public decimal TRANSACTNO { get; set; }
        public string LOANACNTNO { get; set; }
        public string GRACNTNO { get; set; } 
        public string REMARK { get; set; } 
    }

    class DrawLoanEntity {
        public decimal TRANSACTNO { get; set; }
        public string GRACNTNO { get; set; }
        public string LOANACNTNO { get; set; }
        public decimal PAYAMOUNT { get; set; }
        public long CHANGETYPE { get; set; }
        public decimal MONTHPAY { get; set; }
        public long RESUIDMONTHS { get; set; }
        public string REMARK { get; set; }
    }

    class LoanEntity {
        public decimal TRANSACTNO { get; set; }
        public string GRACNTNO { get; set; }
        public string GRNAME { get; set; }
        public string CITIZENIDNO { get; set; }
        public string HYID { get; set; }
        public string HK { get; set; }
        public string DWNAME { get; set; }
        public string DEPT { get; set; }
        public string ADDRESS { get; set; }
        public string POSTCODE { get; set; }
        public decimal JOBID { get; set; }
        public decimal ZCID { get; set; }
        public string HOMEADDRESS { get; set; }
        public string HOMEPOSTCODE { get; set; }
        public double SALARY { get; set; }
        public long PEOPLEOFFAMILY { get; set; }
        public double DEBIT { get; set; }
        public string MOBILENO { get; set; }
        public string GRACNTNO2 { get; set; }
        public string GRNAME2 { get; set; }
        public string CITIZENIDNO2 { get; set; }
        public string HK2 { get; set; }
        public string DWNAME2 { get; set; }
        public string DEPT2 { get; set; }
        public string ADDRESS2 { get; set; }
        public string POSTCODE2 { get; set; }
        public decimal JOBID2 { get; set; }
        public decimal ZCID2 { get; set; }
        public string MOBILENO2 { get; set; }
        public decimal GFLXID { get; set; }
        public string CONTRACTNO { get; set; }
        public string SELLER { get; set; }
        public string HOUSEADDRESS { get; set; }
        public double BUILDAREA { get; set; }
        public double PAYMONEY { get; set; }
        public double HOUSEAMOUNT { get; set; }
        public double HOUSEPRICE { get; set; }
        public string CERTNO { get; set; }
        public string CONTRACTDATE { get; set; }
        public decimal DKTYPE { get; set; }
        public double LOANAMOUNT { get; set; }
        public decimal LOANMONTHS { get; set; }
        public decimal PAYMETHOD { get; set; }
        public decimal ISBIZLOAN { get; set; }
        public double BIZLOANAMOUNT { get; set; }
        public double BIZLOANMONTH { get; set; }
        public decimal ISMERGE { get; set; }
        public double MERGEAMOUNT { get; set; }
        public decimal BUYHOUSETYPE { get; set; }
        public string GJJLOANINFO { get; set; }
        public decimal DYTYPE { get; set; }
        public string BDCQZ { get; set; }
        public string DYHOUSEADDRESS { get; set; }
        public double DYBUILDAREA { get; set; }
        public string DYBELONG { get; set; }
        public string PAYBANKCODE { get; set; }
        public string PAYACNTNAME { get; set; }
        public string PAYACNTNO { get; set; }
        public string REMARK { get; set; }
        public DateTime BIZTIME { get; set; }
        public long LEAFID { get; set; }
        public string ACCESSTOKEN { get; set; }
       
    }
    
}
