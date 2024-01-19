using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class PersonalInformation
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="First Name")]
        [Required (ErrorMessage ="First Name is Required")] 
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Provience")]
        public string ProvienceId { get; set; }
        [Display(Name = "District")]
        public string DistrictId{ get; set; }
        [Display(Name = "Muni")]
        public string MuniId { get; set; }
        [Display(Name = "Tole")]
        public string Tole { get; set; }
        [Display(Name = "Ward")]
        public int Ward { get; set; }
        [Display(Name = "Home Phone")]
        public int HomePhoneNumber { get; set; }
        [Display(Name = "Mobile")]
        public int Mobile { get; set; }
        [Display(Name = "CitizenshipNo")]
        public string CitizenshipNo { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Do you have Driving  Licience")]
        public bool HasLicence { get; set; }
        



    }
}
