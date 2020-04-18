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
        [Required(ErrorMessage = "Please enter Company's name")]
        [RegularExpression(@"^[a-zA-Z0-9_\-\.\s\@]+$", ErrorMessage = "Company's name is not valid")]
        public string Name { get; set; }
        public string ID { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number can be only 0-9")]
        public string Phone { get; set; }
#nullable enable
        public string? Detail { get; set; }
#nullable disable
        [Required(ErrorMessage = "Please enter a number of trainnee needed")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Can be only number and the number can be only positive")]
        public int TrainneeNeed { get; set; } = 0;
        [Required(ErrorMessage = "Please enter a number of cooporative students need")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Can be only number and the number can be only positive")]
        public int CoopNeed { get; set; } = 0;

        public string ImgProfile { get; set; } = "https://www.w3schools.com/howto/img_avatar.png";
        public string Address { get; set; }
        public int TrainneeGot { get; set; } = 0;
        public int CoopGot { get; set; } = 0;

    }
}
