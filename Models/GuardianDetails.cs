using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class GuardianDetails
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Signature { get; set; }

        [ForeignKey(nameof(PersonalInformation))]
        public int PersonalID { get; set; }
        public PersonalInformation PersonalInformation { get; set; }

    }
}
