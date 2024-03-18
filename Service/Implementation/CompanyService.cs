using InternshipForm.Controllers;
using InternshipForm.Data;

using InternshipForm.Models;
using InternshipForm.Service.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InternshipForm.Service.Implementation
{
    public class CompanyService: ICompanyService
    {
        private readonly ApplicationDBContext _context;
       



    public CompanyService(ApplicationDBContext context)
        {
            _context = context;
        }

        public int getInternshipForm(int Id)
        {
            throw new NotImplementedException();
        }

        public int saveCreateCompanyProfile(CompanyProfile company)
        {
            try
            {
                var addedCompany = _context.CompanyProfile.Add(company);
                _context.SaveChanges();
                return addedCompany.Entity.Id;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                // Logging, error handling, etc.
                throw ex;
            }
            return company.Id;
        }
        public List<PersonalInformation> GetAppliedStudents(int internshipId)
        {
            var appliedStudents = (from ai in _context.AppliedInternships
                                   join pi in _context.PersonalInformation on ai.RegisterUserId equals pi.RegisterUserId
                                   where ai.InternshipId == internshipId
                                   select new PersonalInformation
                                   {
                                       InternId = pi.InternId,
                                       FirstName = pi.FirstName,
                                       Email = pi.Email
                                   })
                                   .ToList();

            return appliedStudents;
        }

        public int saveCreateInternship(CreateInternship create)
        {
            try
            {
                var addedInternship = _context.CreateInternship.Add(create);
                _context.SaveChanges();
                return addedInternship.Entity.Id;
            }
           catch (Exception ex)
            {
                // Handle exception appropriately
                // Logging, error handling, etc.
                throw ex;
            }
            return create.Id;
        }
    }
}
