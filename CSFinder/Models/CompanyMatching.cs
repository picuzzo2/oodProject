using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class CompanyMatching
    {
        public string CID { get; set; }
        public int TrainneeNeed { get; set; }
        public int TrainneeGot { get; set; }
        public int CoopNeed { get; set; }
        public int CoopGot { get; set; }

    }
}
