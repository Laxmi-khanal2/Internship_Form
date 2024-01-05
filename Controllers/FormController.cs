using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InternshipForm.Controllers
{
    public class FormController : Controller
    {
        private readonly ApplicationDBContext _context;
        public FormController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreatePost()
        {
            InternshipFormViewModel model = new InternshipFormViewModel();
            model.Education = new List<Education> { new Education() };
            return View(model);
        }

        [HttpGet]
        public IActionResult DetailsPost()
        {
            InternshipFormViewModel model = new InternshipFormViewModel();
            model.OfficalUse = new OfficalUse();

            model.OfficalUse.Internship_For = "Dot Net";
            model.OfficalUse.Intern_Id = 0;
            model.OfficalUse.Duration_of_Internship = " 3 Months";
            model.OfficalUse.Authorized_Signature = "";
          


            model.PersonalInformation = new PersonalInformation();

            model.PersonalInformation.FirstName = "Laxmi ";
            model.PersonalInformation.MiddleName = " ";
            model.PersonalInformation.LastName = " Khanal";
            model.PersonalInformation.ProvienceId = "Bagamati";
            model.PersonalInformation.DistrictId = "Kathnmandu";
            model.PersonalInformation.MuniId = "Kageshwori Manohara";
            model.PersonalInformation.Ward = 8;
            model.PersonalInformation.HomePhoneNumber = 9852364;
            model.PersonalInformation.Mobile = 982287564;
            model.PersonalInformation.CitizenshipNo = "abc";
            model.PersonalInformation.HasLicence = false;
            


            model.Education = new List<Education>
            {
                new Education() {School_CollegeName="Jaya Multiple Campus", Location="Makalbari",StartYear= 2018 , CompletionYear =2020  , Major="Management"},
                new Education(){School_CollegeName ="Texas College of Management and It", Location ="Shipal", StartYear = 2021, CompletionYear = 2024, Major="BIT"},


            };
            model.GuardianDetails=new GuardianDetails();

            model.GuardianDetails.Name = "Bhuwan Khanal";
            model.GuardianDetails.Relation = "Father";
            model.GuardianDetails.Address = "Dakshindhoka";
            model.GuardianDetails.PhoneNumber = 986069014;
            model.GuardianDetails.Signature = "";

            model.References = new References();

            model.References.Name = "References";
            model.References.Positions = "";
            model.References.College_Company = "Texas College of Management and IT";
            model.References.Phone = 98235696;
            model.References.Signature = "Texas College";



            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreatePost(InternshipFormViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       var personalInfo = _context.PersonalInformation.Add(model.PersonalInformation);
        //        _context.SaveChanges();
        //    //    var PersonalId = personalInfo.Entity.Id;
        //    //    model.Education.PersonalID = PersonalId;
        //    //    model.GuardianDetails.PersonalID = PersonalId;
        //    //    model.References.PersonalID = PersonalId;
        //    //    SaveEducation(model.Education);
        //    }
        //    return View(model);
        //}

        //private Education SaveEducation(Education model)
        //{
        //    _context.Education.Add(model);
        //    _context.SaveChanges();
        //    return model;
        //}
        //private Education SaveGuardian(Education model)
        //{
        //    _context.Education.Add(model);
        //    _context.SaveChanges();
        //    return model;
        //}
    }

}
