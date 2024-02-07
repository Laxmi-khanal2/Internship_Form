﻿using InternshipForm.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "School/College Name")]
        [Required(ErrorMessage =" Required")]
        public string ? SchoolorCollegeName { get; set; }
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Required")]
        public string? Location { get; set; }
        [Display(Name = "Start Year")]
       

        public DateTime? StartYear { get; set; }
        [Display(Name = "Completion Year")]
        

        public DateTime?  CompletionYear { get; set; }
        [Display(Name = "Major")]
        [Required(ErrorMessage = "  Required")]

        public string?  Major { get; set; }
  

        //[ForeignKey(nameof(PersonalInformation))]
        //public int PersonalID { get; set; }
        //public PersonalInformation PersonalInformation { get; set; }

    }
}
