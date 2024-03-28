using Azure.Core;
using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;
using InternshipForm.Service.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;


namespace InternshipForm.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IStudentService _student;

        // creating the constructor for class form controller

        public StudentController(ApplicationDBContext context, IStudentService student)
        {
            _context = context;
            _student = student;
        }
        public IActionResult Index()
        {
            return View();
        }

        //controller for contact us
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(ContactUs contactUs)
        {
            var result = _student.SaveContactUs(contactUs);
            return RedirectToAction("Index");
        }

        //view companies
        public IActionResult viewCompanyProfile()
        {
            List<CompanyProfile> companyProfiles = _context.CompanyProfile.ToList();
            return View(companyProfiles);
        }
        //View Internship
        public IActionResult ViewInternship()
            {
            CreateInternship createInternship = new CreateInternship();
            
            List<CreateInternship> Internship = _context.CreateInternship.ToList();

            return View(Internship);
            
            
        }
        public IActionResult GetInternshipForm(CreateInternship createInternship)
        {
          
            return View(createInternship);
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
            if (ModelState.IsValid)
            {
               // var s = _student.saveStudentRecord();
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
        public IActionResult Create(int? Id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
           
                InternshipFormViewModel model = new InternshipFormViewModel();
            model.PersonalInformation = new PersonalInformation();
            model.Education = new List<Education> { new Education() };
            model.References = new References();
            model.OfficalUse = new OfficalUse();
            model.GuardianDetails = new GuardianDetails();
                if (Id.HasValue && Id.Value > 0)
            {
                model = _student.getStudentRecord(Id.Value);
            }
            
            return View(model);
        }

        public IActionResult Apply(int internshipId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
          
            // Check if the user has already applied for this internship
            var appliedInternshipIds = _student.GetAppliedInternshipId(userId.Value).ToList();

            if (appliedInternshipIds.Contains(internshipId))
            {
                TempData["Message"] = "You have already applied for this Internship.";
                TempData["ShowMessage"] = true;
            }
            else
            {
                try
                {

                    InternshipFormViewModel model = new InternshipFormViewModel();
                    model.PersonalInformation = new PersonalInformation();
                    model.Education = new List<Education> { new Education() };
                    model.References = new References();
                    model.OfficalUse = new OfficalUse();
                    model.GuardianDetails = new GuardianDetails();
                    int internId = _student.GetInternId(userId.Value);
                    if (internId > 0)
                    {


                        // Add logic to store the application in the database
                        AppliedInternships application = new AppliedInternships();

                        application.RegisterUserId = userId.Value;
                        application.InternshipId = internshipId;



                        _context.AppliedInternships.Add(application);

                        _context.SaveChanges();

                        TempData["Message"] = "You have successfully applied for this internship.";
                        TempData["ShowMessage"] = true;
                    }
                    else
                    {
                        return View("Create",model);
                    }
                }
                catch (Exception ex)
                {

                    TempData["Message"] = "An error occurred while applying for the internship.";
                    TempData["ShowMessage"] = true;

                }
            }

            return RedirectToAction("ViewInternship");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(InternshipFormViewModel model)
        {

            //if (ModelState.IsValid)
            {

                var userId = HttpContext.Session.GetInt32("UserId");
                model.PersonalInformation.RegisterUserId = userId.Value;
              
                var result = _student.saveStudentRecord(model);

                if(result > 0)
                {
                    return RedirectToAction("ViewInternship");
                }
               
              
            }
                return View(model);
            
        }

        
        //  action controller datatable
        [HttpGet]
        public IActionResult Details()
        {
            var FilterOptions = new List<SelectListItem>
            {
                new SelectListItem{ Value = "firstName", Text="FirstName"},
                new SelectListItem{ Value = "schoolOrCollegeName", Text="SchoolOrCollegeName"},
                new SelectListItem{ Value = "email", Text="Email"},
                new SelectListItem{Value ="internshipFor",Text="InternshipFor"},
            };
            ViewData["Filter"] = FilterOptions;
            return View();
        }
        [HttpPost]
        public IActionResult GetData(int draw, int start, int length, string filterValue, string filterType)
        {
            var query = from PI in _context.PersonalInformation
                        join E in _context.Education on PI.InternId equals E.InternId
                        join GD in _context.GuardianDetails on PI.InternId equals GD.InternId
                        join R in _context.References on PI.InternId equals R.InternId
                        join O in _context.OfficalUse on PI.InternId equals O.InternId
                        select new
                        {
                            InternId = PI.InternId,
                            FirstName = PI.FirstName,
                            LastName = PI.LastName,
                            Mobile = PI.Mobile,
                            Email = PI.Email,
                            InternshipFor = O.InternshipFor,
                            HasLicence = PI.HasLicence,
                            SchoolOrCollegeName = E.SchoolOrCollegeName,
                            Major = E.Major,
                            Address = GD.Address,
                            CollegeorCompany = R.CollegeorCompany
                        };

            //filter by name,mobile and email
            if (!string.IsNullOrEmpty(filterValue) && !string.IsNullOrEmpty(filterType))
            {
                if (filterType == "firstName")
                {
                    query = query.Where(item => item.FirstName.Contains(filterValue));
                }
                else if (filterType == "email")
                {
                    query = query.Where(item => item.Email.Contains(filterValue));
                }
                if (filterType == "schoolOrCollegeName")
                {
                    query = query.Where(item => item.SchoolOrCollegeName.Contains(filterValue));
                }
                if (filterType == "internshipFor")
                {
                    query = query.Where(item => item.InternshipFor.Contains(filterValue));
                }
            }



            // Count total records before applying pagination
            int recordsTotal = query.Count();

            // Apply pagination
            var data = query.Skip(start).Take(length).ToList();

            // Prepare the response
            var response = new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,

                data = data
            };

            return Json(response);
        }






        [HttpGet]
        public IActionResult Delete(int Id)
        {

            try
            {
                InternshipFormViewModel model = new InternshipFormViewModel();
                model.Education = new List<Education> { new Education() };

                if (Id > 0)
                {

                    model.PersonalInformation = (from PersonalInformation in _context.PersonalInformation where PersonalInformation.InternId == Id select PersonalInformation).FirstOrDefault();

                    model.Education = _context.Education.Where(p => p.InternId == Id).ToList();
                    if (model.Education.Count == 0)
                    {
                        model.Education = new List<Education> { new Education() };
                    }
                    model.GuardianDetails = (from GuardianDetails in _context.GuardianDetails where GuardianDetails.InternId == Id select GuardianDetails).FirstOrDefault();
                    model.OfficalUse = (from OfficalUse in _context.OfficalUse where OfficalUse.InternId == Id select OfficalUse).FirstOrDefault();
                    model.References = (from References in _context.References where References.InternId == Id select References).FirstOrDefault();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(InternshipFormViewModel model)
        {

            _context.Entry(model.PersonalInformation).State = EntityState.Deleted;
            _context.SaveChanges();

            foreach (var education in model.Education)
            {

                education.InternId = model.PersonalInformation.InternId;


                _context.Entry(education).State = EntityState.Deleted;

            }
            _context.SaveChanges();


            // Update Guardian Details

            model.GuardianDetails.InternId = model.PersonalInformation.InternId;
            _context.Entry(model.GuardianDetails).State = EntityState.Deleted;
            _context.SaveChanges(true);

            model.OfficalUse.InternId = model.PersonalInformation.InternId;
            _context.Entry(model.OfficalUse).State = EntityState.Deleted;
            _context.SaveChanges();
            //UPDATE rEFERENCES

            model.References.InternId = model.PersonalInformation.InternId;
            _context.Entry(model.References).State = EntityState.Deleted;


            // Save changes
            _context.SaveChanges();

            return RedirectToAction("Details");

            return View(model);
        }



        public IActionResult DetailsPost()
        {
            InternshipFormViewModel model = new InternshipFormViewModel();
            model.OfficalUse = new OfficalUse();

            model.OfficalUse.InternshipFor = "Dot Net";
            //model.OfficalUse.Intern_Id = 0;
            //model.OfficalUse.Duration_of_Internship = " 3 Months";
            //model.OfficalUse.Authorized_Signature = "";



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

            //model.certiLice = new CertiLice();
            //model.certiLice.certilice = "";


            return View(model);
        }



    }
}










