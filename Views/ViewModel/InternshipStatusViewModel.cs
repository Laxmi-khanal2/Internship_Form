using InternshipForm.Models;

namespace InternshipForm.Views.ViewModel
{
    public class InternshipStatusViewModel

    { 
        public int Id { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public CreateInternship CreateInternship { get; set; }
        public AppliedInternships AppliedInternships { get; set;}
    }
}
