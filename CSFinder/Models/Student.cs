using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CSFinder.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public string SID { get; set; }
        [Required(ErrorMessage = "Please enter Firstname and Lastname")]
        [RegularExpression(@"^[A-Za-z]+\s[A-Za-z]+$", ErrorMessage = "Name is not valid")]
        public string Name { get; set; }
       
        public string ID { get; set; }

        [Required(ErrorMessage = "Please enter a type of training")]
        public Types Type { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number can be only 0-9")]
        public string Phone { get; set; }
#nullable enable
        public string? Detail { get; set; }
#nullable disable
        public string Status { get; set; } = "Waiting for matching";
        public string ImgProfile { get; set; } = "https://www.w3schools.com/howto/img_avatar.png";
#nullable enable
        public string? ImgTranscript { get; set; }
        public string? ImgResume { get; set; }

        public string? Rank1 { get; set; } 
        public string? Rank2 { get; set; } 
        public string? Rank3 { get; set; } 
#nullable disable
        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }
    }


}


