using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class RegisterCompany
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Name Of Company")]
        public string? Name { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter Your Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password \"{0}\" must have {2} characters", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters, at least 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number, and 1 Special Character")]
        public string Password { get; set; }



        //[ForeignKey(nameof(PersonalInformation))]
        //public int InternId { get; set; }
        //public PersonalInformation PersonalInformation { get; set; }


    }
}


