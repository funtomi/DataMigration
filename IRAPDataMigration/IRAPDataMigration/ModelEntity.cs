
namespace IRAP.DataMigration {
    internal class TransferEntity {
        public decimal TRANSACTNO { get; set; }
        public string GRACNTNO { get; set; }
        public string SRCDWACNTNO { get; set; }
        public string DSTDWACNTNO { get; set; }
        public string REMARK { get; set; }
    }

    internal class DrawEntity {
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
}
