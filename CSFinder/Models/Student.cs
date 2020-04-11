using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CSFinder.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public string SID { get; set; }
        
        public string Name { get; set; }
       
        public string ID { get; set; }
#nullable enable
        public int? Type { get; set; }
#nullable disable
        public string Phone { get; set; }
#nullable enable
        public string? Detail { get; set; }
#nullable disable
        public string Status { get; set; } = "Waiting for matching";
        public string ImgProfile { get; set; } = "https://www.w3schools.com/howto/img_avatar.png";
#nullable enable
        public string? ImgTranscript { get; set; }
        public string? ImgResume { get; set; }

        public string? Rank1 { get; set; } = "";
        public string? Rank2 { get; set; } = "";
        public string? Rank3 { get; set; } = "";
#nullable disable
    }
}
