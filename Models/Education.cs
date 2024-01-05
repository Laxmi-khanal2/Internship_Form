using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "School/College Name")]
        public string  School_CollegeName { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Start Year")]
        public int StartYear { get; set; }
        [Display(Name = "Completion Year")]
        public int CompletionYear { get; set; }
        [Display(Name = "Major")]
        public string Major { get; set; }

        [ForeignKey(nameof(PersonalInformation))]
        public int PersonalID { get; set; }
        public PersonalInformation PersonalInformation { get; set; }

    }
}
