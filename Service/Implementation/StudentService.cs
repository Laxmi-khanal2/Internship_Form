using InternshipForm.Service.Interface;
using InternshipForm.Models;
using InternshipForm.Data;
using InternshipForm.ViewModel;
using Microsoft.EntityFrameworkCore;
namespace InternshipForm.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDBContext _context;

        public StudentService(ApplicationDBContext context)
        {
            _context = context;
        }




        public InternshipFormViewModel getStudentRecord(int id)
        {
            InternshipFormViewModel model = new InternshipFormViewModel
            {
                Education = new List<Education> { new Education() }
            };

            if (id > 0)
            {
                model.PersonalInformation = _context.PersonalInformation.FirstOrDefault(p => p.InternId == id);

                model.Education = _context.Education.Where(p => p.InternId == id).ToList();
                if (model.Education.Count == 0)
                {
                    model.Education = new List<Education> { new Education() };
                }

                model.GuardianDetails = _context.GuardianDetails.FirstOrDefault(p => p.InternId == id);
                model.References = _context.References.FirstOrDefault(p => p.InternId == id);
            }

            return model ;
        }
    
    


        public int saveStudentRecord(InternshipFormViewModel model)
        {

            try
            {
                if (model.PersonalInformation.InternId == 0)
                {
                    var personalinformation = _context.PersonalInformation.Add(model.PersonalInformation);
                    _context.SaveChanges();

                    foreach (var education in model.Education)
                    {
                        education.InternId = personalinformation.Entity.InternId;
                        _context.Education.Add(education);
                    }

                    _context.SaveChanges();

                    model.GuardianDetails.InternId = personalinformation.Entity.InternId;
                    _context.GuardianDetails.Add(model.GuardianDetails);
                    _context.SaveChanges();

                    model.References.InternId = personalinformation.Entity.InternId;
                    _context.References.Add(model.References);
                    _context.SaveChanges();

                    return personalinformation.Entity.InternId;
                }
                else
                {
                    _context.Entry(model.PersonalInformation).State = EntityState.Modified;

                    foreach (var education in model.Education)
                    {
                        education.InternId = model.PersonalInformation.InternId;
                        _context.Entry(education).State = EntityState.Modified;
                    }

                    model.GuardianDetails.InternId = model.PersonalInformation.InternId;
                    _context.Entry(model.GuardianDetails).State = EntityState.Modified;

                    model.References.InternId = model.PersonalInformation.InternId;
                    _context.Entry(model.References).State = EntityState.Modified;

                    _context.SaveChanges();

                    return model.PersonalInformation.InternId;
                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                // Logging, error handling, etc.
                throw ex;
            }
        }
    }
    
}
