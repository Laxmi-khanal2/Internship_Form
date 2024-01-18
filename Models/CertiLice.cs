using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class CertiLice
    {
        public int id { get; set; }
        [Display (Name = "Other Training, Certification or Licience held:")]
        public string certilice { get; set; }
    }
}
