using InternshipForm.ViewModel;

namespace InternshipForm.Service.Interface
{
    public interface IStudentService
    {
        public InternshipFormViewModel getStudentRecord(int id);
        public int saveStudentRecord(InternshipFormViewModel model);


    }
}
