﻿using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class PersonalInformation
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="First Name")]

        [Required (ErrorMessage ="First Name is Required")] 
        public string? FirstName { get; set; }
        
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }
        [Display(Name = "Last Name")]

        [Required (ErrorMessage = "Last Name is Required")]
        public string? LastName { get; set; }
        [Display(Name = "Provience")]
        [Required (ErrorMessage ="Provience Name is Required")]
        public string? ProvienceId { get; set; }
        [Display(Name = "District")]
        [Required(ErrorMessage = "District Name is Required")]
        public string? DistrictId{ get; set; }
        [Display(Name = "Muni")]
        [Required(ErrorMessage = "Municipality Name is Required")]
        public string? MuniId { get; set; }
        [Display(Name = "Tole")]
        [Required(ErrorMessage = "Tole Name is Required")]
        public string? Tole { get; set; }
        [Display(Name = "Ward")]
        public int Ward { get; set; }
        
        [Display(Name = "Home Phone")]
        [Required(ErrorMessage = "Mobile Number is Required")]
       
        public long HomePhoneNumber { get; set; }


        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Mobile Number is Required")]
       
        public long Mobile { get; set; }


        [Display(Name = "CitizenshipNo")]
        [Required(ErrorMessage = "Citizenship Number  is Required")]
        public string? CitizenshipNo { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address  is Required")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }
        [Display(Name = "Do you have Driving  Licience")]
        public bool HasLicence { get; set; }
        



    }
}
