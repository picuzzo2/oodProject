using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public string ID { get; set; }
        public string Password { get; set; }
        public string IDtype { get; set; }
    }
}
