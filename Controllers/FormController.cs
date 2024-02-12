using Azure.Core;
using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternshipForm.Controllers
{
    public class FormController : Controller
    {
        private readonly ApplicationDBContext _context;
        // creating the constructor for class form controller
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(InternshipFormViewModel model)
        {
            //if (ModelState.IsValid)
            {
                var personalinformation = _context.PersonalInformation.Add(model.PersonalInformation);
                _context.SaveChanges();
                // SQL code of education model
                foreach (var education in model.Education)
                {
                    education.InternId = personalinformation.Entity.InternId;
                    _context.Education.Add(education);
                }
                _context.SaveChanges();
                //sql code of guardian details model
                var guardiandetails = _context.GuardianDetails.Add(model.GuardianDetails);

                guardiandetails.Entity.InternId = personalinformation.Entity.InternId;
                _context.SaveChanges();
                //sql code of references model
                var references = _context.References.Add(model.References);
                references.Entity.InternId = personalinformation.Entity.InternId;
                _context.SaveChanges();

                return RedirectToAction("CreatePost");
            }

            return View(model);
        }
        //action method for create page 
        [HttpGet]
        public IActionResult Create()
        {
            InternshipFormViewModel model = new InternshipFormViewModel();
            model.Education = new List<Education> { new Education() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InternshipFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var personalinformation = _context.PersonalInformation.Add(model.PersonalInformation);
                _context.SaveChanges();
                // SQL code of education model
                foreach (var education in model.Education)
                {
                    education.InternId = personalinformation.Entity.InternId;
                    _context.Education.Add(education);
                }
                _context.SaveChanges();
                //sql code of guardian details model
                var guardiandetails = _context.GuardianDetails.Add(model.GuardianDetails);

                guardiandetails.Entity.InternId = personalinformation.Entity.InternId;
                _context.SaveChanges();
                //sql code of references model
                var references = _context.References.Add(model.References);
                references.Entity.InternId = personalinformation.Entity.InternId;
                _context.SaveChanges();

                return RedirectToAction("Create");
            }

            return View(model);
        }

        //  action controller datatable
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoadData(int draw, int start, int length)
        {
            var query = _context.PersonalInformation.Skip(start).Take(length);
            var data = query.ToList();

            // Total record count for pagination
            var recordsTotal = _context.PersonalInformation.Count();

            // Filtered record count for custom filtering (if required)
            var recordsFiltered = recordsTotal; // You might need to adjust this based on your filter criteria

            // Prepare the response
            var response = new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            };

            return Json(response);
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
                new Education() {SchoolOrCollegeName="Jaya Multiple Campus", Address="Makalbari",StartYear= new DateTime(1/1/2018) , CompletionYear = new DateTime(1/1/2018)   , Major="Management"},
                new Education(){SchoolOrCollegeName ="Texas College of Management and It", Address ="Shipal", StartYear =  new DateTime(1/1/2018) , CompletionYear =  new DateTime(1/1/2018) , Major="BIT"},


            };
            model.GuardianDetails = new GuardianDetails();

            model.GuardianDetails.Name = "Bhuwan Khanal";
            model.GuardianDetails.Relation = "Father";
            model.GuardianDetails.Address = "Dakshindhoka";
            model.GuardianDetails.PhoneNumber = 986069014;
            model.GuardianDetails.Signature = "";

            model.References = new References();

            model.References.Name = "References";
            model.References.Positions = "";
            model.References.CollegeorCompany = "Texas College of Management and IT";
            model.References.Phone = 98235696;
            model.References.Signature = "Texas College";

            model.certiLice = new CertiLice();
            model.certiLice.certilice = "";


            return View(model);
        }



    }
    }





    

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult  Create(InternshipFormViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var personalInfo = _context.PersonalInformation.Add(model.PersonalInformation);
    //            _context.SaveChanges();
    //            //    var PersonalId = personalInfo.Entity.Id;
    //            //    model.Education.PersonalID = PersonalId;
    //            //    model.GuardianDetails.PersonalID = PersonalId;
    //            //    model.References.PersonalID = PersonalId;
    //            //    SaveEducation(model.Education);
    //        }
    //        return View(model);
    //    }

    //    private Education SaveEducation(Education model)
    //    {
    //        _context.Education.Add(model);
    //        _context.SaveChanges();
    //        return model;
    //    }
    //    private Education SaveGuardian(Education model)
    //    {
    //        _context.Education.Add(model);
    //        _context.SaveChanges();
    //        return model;
    //    }
    //}


