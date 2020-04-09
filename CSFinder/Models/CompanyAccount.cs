using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class CompanyAccount
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
#nullable enable
        public string? Detail { get; set; }
#nullable disable
        public int TrainneeNeed { get; set; }
        public int CoopNeed { get; set; }
    }
}
