using InternshipForm.Models;
using System.ComponentModel.DataAnnotations;

namespace InternshipForm.ViewModel
{
    public class InternshipFormViewModel
    {
        [Key]
        public int FormId { get; set; }
        public OfficalUse OfficalUse { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public References References { get; set; }
        public GuardianDetails GuardianDetails { get; set; }
        //public Education Education { get; set; }
        public List<Education> Education { get; set; }
        public CertiLice certiLice { get; set; }

     
    }
}
