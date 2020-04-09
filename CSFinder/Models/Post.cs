using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PID { get; set; }
        public string CID { get; set; }
        public string Detail { get; set; }
        public int TrainneeNeed { get; set; }
        public int CoopNeed { get; set; }

    }
}
