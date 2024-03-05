using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class OfficalUse
    {
        public int Id { get; set; }
        [Display (Name ="Internship For : ")]
        [Required(ErrorMessage = "Internship Topic Is Necessary")]
        public string? InternshipFor { get; set; }

        
        [Display(Name = "Other Training, Certification or Licience held:")]
        public string? certilice { get; set; }
        [ForeignKey(nameof(PersonalInformation))]
        public int InternId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
