using InternshipForm.Controllers;
using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.Service.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InternshipForm.Service.Implementation
{
    public class CompanyService: ICompanyService
    {
        private readonly ApplicationDBContext _context;

        public CompanyService(ApplicationDBContext context)
        {
            _context = context;
        }

        
        

        public int saveCreateCompanyProfile(CompanyProfile company)
        {
            try
            {
                var addedCompany = _context.CompanyProfile.Add(company);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                // Logging, error handling, etc.
                throw ex;
            }
            return company.Id;
        }

        
    }
}
