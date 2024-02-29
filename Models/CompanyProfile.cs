using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class CompanyProfile
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Name of the Company")]
        public string Name { get; set; }
        public string Address {  get; set; }
        public int Contact {  get; set; }
        public string MissionsandValues { get; set; }
        public string Description { get; set; }
    }
}
