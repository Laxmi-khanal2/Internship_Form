using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class AppliedInternships
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(RegisterUser))]
       
        public int RegisterUserId { get; set; }
        public RegisterUser RegisterUser { get; set; }

        [ForeignKey(nameof(CreateInternship))]
        public int InternshipId { get; set; }
        public CreateInternship CreateInternship { get; set; }

        [ForeignKey(nameof(PersonalInformation))]
        public int InternId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }

        public string Status { get; set; }

    }
}
