using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class StudentAccount
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SID { get; set; }

        public string Name { get; set; }
#nullable enable
        public int? Type { get; set; }
#nullable disable
        public string Email { get; set; }

        public string Phone { get; set; }
#nullable enable
        public string? Detail { get; set; }
#nullable disable
        public string Status { get; set; }
    }
}
