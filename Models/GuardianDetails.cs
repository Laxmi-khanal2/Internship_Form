using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class GuardianDetails
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Relation { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Address { get; set; }
        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ?  Signature { get; set; }

        [ForeignKey(nameof(PersonalInformation))]
        public int InternId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }

    }
}
