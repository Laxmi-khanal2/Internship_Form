using InternshipForm.Controllers;
using InternshipForm.Data;

using InternshipForm.Models;
using InternshipForm.Service.Interface;
using InternshipForm.Views.ViewModel;
using Microsoft.EntityFrameworkCore;
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


        // Assuming _context is your DbContext instance
        public List<CompanyFormViewModel> GetAppliedStudents(int internshipId)
        {
            var appliedStudents = (from ai in _context.AppliedInternships
                                   join pi in _context.PersonalInformation on ai.RegisterUserId equals pi.RegisterUserId
                                   join ci in _context.CreateInternship on ai.InternshipId equals ci.Id
                                   where ai.InternshipId == internshipId
                                   group new { ai, pi, ci } by ai.InternId into g
                                   select new CompanyFormViewModel
                                   {
                                       Id = g.Key,
                                       PersonalInformation = g.Select(x => new PersonalInformation
                                       {
                                           InternId = x.pi.InternId,
                                           FirstName = x.pi.FirstName,
                                           LastName = x.pi.LastName,
                                           Email = x.pi.Email,
                                           HomePhoneNumber = x.pi.HomePhoneNumber
                                       }).ToList(),
                                       CreateInternship = g.Select(x => new CreateInternship
                                       {
                                           Id = x.ci.Id,
                                           TitleOfInternship = x.ci.TitleOfInternship,
                                           NameOfCompany = x.ci.NameOfCompany,
                                           Address = x.ci.Address,
                                           SelectedType = x.ci.SelectedType,
                                           OfferedSalary = x.ci.OfferedSalary,
                                           Location = x.ci.Location,
                                           Level = x.ci.Level,
                                           Duration = x.ci.Duration,
                                           JobDescription = x.ci.JobDescription,
                                           Responsiblity = x.ci.Responsiblity
                                       }).ToList()
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
