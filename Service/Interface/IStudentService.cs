using InternshipForm.ViewModel;

namespace InternshipForm.Service.Interface
{
    public interface IStudentService
    {
        public InternshipFormViewModel getStudentRecord(int id);
        public int saveStudentRecord(InternshipFormViewModel model);

        public bool HasFilledForm(int userId);
        public List<int> GetAppliedInternshipId(int userId);
        public int GetInternId(int userId);
    }
}
