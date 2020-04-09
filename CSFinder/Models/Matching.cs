﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    [Table("Matching")]
    public class Matching
    {
        public DateTime Date { get; set; }
        [Key, Column(Order =1)]
        public string SID { get; set; }
        [Key, Column(Order =2)]
        public string CID { get; set; }
        public int Type { get; set; }
        public string Result { get; set; }
    }
}
