using InternshipForm.Models;
using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Views.ViewModel
{
    public class CompanyFormViewModel
    {
        [Key]
        public int Id { get; set; }
        public List<CreateInternship> CreateInternship { get; set; }
        public List<CompanyProfile> CompanyProfile { get; set; }
    }
}
