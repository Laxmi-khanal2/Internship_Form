using InternshipForm.Service.Interface;
using InternshipForm.Models;
using InternshipForm.Data;
using InternshipForm.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using InternshipForm.Views.ViewModel;
namespace InternshipForm.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDBContext _context;

        public StudentService(ApplicationDBContext context)
        {
            _context = context;
        }
        public int GetInternId(int userId)
        {
            return _context.PersonalInformation
                                           .Where(a => a.RegisterUserId == userId)
                                           .Select(a => a.InternId).FirstOrDefault();
                                           

        }


        public List<int> GetAppliedInternshipId(int userId)
        {
            return _context.AppliedInternships
                                           .Where(a => a.RegisterUserId == userId)
                                           .Select(a => a.InternshipId)
                                           .ToList();

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
                model.OfficalUse = _context.OfficalUse.FirstOrDefault(p => p.InternId == id);
                model.References = _context.References.FirstOrDefault(p => p.InternId == id);
            }

            return model;
        }

        public bool HasFilledForm(int userId)
        {
            var hasFilledForm = _context.PersonalInformation.Any(p => p.RegisterUserId == userId);
            return hasFilledForm;
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

                    model.OfficalUse.InternId = personalinformation.Entity.InternId;
                    _context.OfficalUse.Add(model.OfficalUse);
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

                    model.OfficalUse.InternId = model.PersonalInformation.InternId;
                    _context.Entry(model.OfficalUse).State = EntityState.Modified;

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

        public int SaveContactUs(ContactUs contactUs)
        {

            try
            {
                var addedContactUs = _context.ContactUs.Add(contactUs);
                _context.SaveChanges();
                return addedContactUs.Entity.Id;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                // Logging, error handling, etc.
                throw ex;
            }
            return contactUs.Id;
        }
        //public InternshipStatusViewModel GetApplicationStatus(int internId)
        //{
        //    // Retrieve personal information
        //    var personalInfo = _context.PersonalInformation
        //        .FirstOrDefault(pi => pi.InternId == internId);

        //    // Retrieve applied internships
        //    var appliedInternships = _context.AppliedInternships
        //        .Where(ai => ai.RegisterUserId == internId)
        //        .ToList();

        //    // Retrieve internship details for applied internships
        //    var internshipDetails = appliedInternships
        //        .Select(ai => _context.CreateInternship.FirstOrDefault(ci => ci.Id == ai.InternshipId))
        //        .ToList();

        //    // Create ViewModel
           

        //    return viewModel;
        
    }

}
