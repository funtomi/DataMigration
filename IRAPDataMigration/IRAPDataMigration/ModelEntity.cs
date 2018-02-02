using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRAP.DataMigration {
    public class TransferEntity {
        public decimal TRANSACTNO { get; set; }
        public string GRACNTNO { get; set; }
        public string SRCDWACNTNO { get; set; }
        public string DSTDWACNTNO { get; set; }
        public string REMARK { get; set; }
    }
}
