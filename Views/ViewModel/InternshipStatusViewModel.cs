using InternshipForm.Models;

namespace InternshipForm.Views.ViewModel
{
    public class InternshipStatusViewModel

    { 
        public int Id { get; set; }
        public List<PersonalInformation> PersonalInformation { get; set; }
        public List<CreateInternship> CreateInternship { get; set; }
        public List<AppliedInternships> AppliedInternships { get; set;}
    }
}
