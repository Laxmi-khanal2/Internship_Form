using InternshipForm.Models;

namespace InternshipForm.Service.Interface
{
    public interface ICompanyService
    {
    
        public int saveCreateCompanyProfile(CompanyProfile company);
        public int getInternshipForm(int Id);
        public int saveCreateInternship(CreateInternship create);

    }
}
