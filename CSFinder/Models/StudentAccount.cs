using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSFinder.Models
{
    public class StudentAccount
    {
        [Required(ErrorMessage ="Please enter username")]
        [RegularExpression(@"^[A-Za-z0-9_\-\.]+$", ErrorMessage = "Username is not valid")]
        public string ID { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Password is not valid")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter an Student ID")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Student ID must be 10 digits")]
        public string SID { get; set; }

        [Required(ErrorMessage = "Please enter Firstname and Lastname")]
        [RegularExpression(@"^[A-Za-z]+\s[A-Za-z]+$", ErrorMessage = "Name is not valid")]
        public string Name { get; set; }
#nullable enable
        public int? Type { get; set; }
#nullable disable
        [Required(ErrorMessage = "Please enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [StringLength(10,MinimumLength =10, ErrorMessage = "Phone number must be 10 digits")]
        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"^[0-9]+$",ErrorMessage = "Phone number can be only 0-9")]
        public string Phone { get; set; }
#nullable enable
        [DisplayName("Detail (Optional)")]
        public string? Detail { get; set; }
#nullable disable
        public string Status { get; set; }
        public string Address { get; set; }
    }
}
