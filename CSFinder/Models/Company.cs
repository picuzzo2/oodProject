using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        public string CID { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Phone { get; set; }
#nullable enable
        public string? Detail { get; set; }
#nullable disable
        public int TrainneeNeed { get; set; } = 0;
        public int CoopNeed { get; set; } = 0;

        public string ImgProfile { get; set; } = "https://www.w3schools.com/howto/img_avatar.png";
        public string Address { get; set; }
        public int TrainneeGot { get; set; } = 0;
        public int CoopGot { get; set; } = 0;

    }
}
