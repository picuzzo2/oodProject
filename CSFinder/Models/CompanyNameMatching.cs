using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class CompanyNameMatching
    {
        public Matching matching { get; set; }
        public string r1Name { get; set; }
        public string r2Name { get; set; }
        public string r3Name { get; set; }
        public string resultName { get; set; }
        public CompanyNameMatching(Matching m, string r1, string r2, string r3, string rtName)
        {
            matching = m;
            r1Name = r1;
            r2Name = r2;
            r3Name = r3;
            resultName = rtName;
        }
    }
}

