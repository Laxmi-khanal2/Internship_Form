using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class References
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(PersonalInformation))]
        public int InternId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public string Name { get; set; }
        public string Positions { get; set; }
        public string CollegeorCompany { get; set; }
        public int Phone { get; set; }
        public string Signature { get; set; }
    }
}
