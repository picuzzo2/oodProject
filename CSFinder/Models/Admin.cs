﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public string ID { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public string University { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ImgProfile { get; set; }

    }
}
