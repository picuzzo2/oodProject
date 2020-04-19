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

        [StringLength(65535,ErrorMessage ="Post body can be up to 65535 characters")]
        [Required(ErrorMessage = "Please enter post body")]
        public string Detail { get; set; }

        public string ImgLink { get; set; }

    }
}
