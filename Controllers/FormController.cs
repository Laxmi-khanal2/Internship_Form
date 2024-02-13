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
        public IActionResult Create(int Id)
        {
            InternshipFormViewModel model = new InternshipFormViewModel();
            model.Education = new List<Education> { new Education() };

            if (Id > 0)
            {
                model.PersonalInformation = _context.PersonalInformation.FirstOrDefault(p => p.InternId == Id);
                model.Education = _context.Education.Where(p => p.InternId == Id).ToList();
                if (model.Education.Count == 0)
                {
                    model.Education = new List<Education> { new Education() };
                }
                model.GuardianDetails = _context.GuardianDetails.FirstOrDefault(p => p.InternId == Id);
                model.References = _context.References.FirstOrDefault(p => p.InternId == Id);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InternshipFormViewModel model)
        {
            if (ModelState.IsValid)

            {
                if (model.PersonalInformation.InternId == 0)
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

                    model.GuardianDetails.InternId = personalinformation.Entity.InternId;
                    _context.GuardianDetails.Add(model.GuardianDetails);

                    _context.SaveChanges();
                    //sql code of references model

                    model.References.InternId = personalinformation.Entity.InternId;
                    _context.References.Add(model.References);
                    _context.SaveChanges();
                }
                else
                {
                    ////var existingPersonalInformation = _context.PersonalInformation.Find(model.PersonalInformation.InternId);
                    //if (existingPersonalInformation != null)
                    //{
                    _context.PersonalInformation.Update(model.PersonalInformation);
                    //}

                    //Update Education

                    foreach (var education in model.Education)
                    {

                        education.InternId = model.PersonalInformation.InternId;


                        _context.Education.Update(education);

                    }

                    // Update Guardian Details


                    _context.GuardianDetails.Update(model.GuardianDetails);

                    //UPDATE rEFERENCES


                    _context.References.Update(model.References);


                    // Save changes
                    _context.SaveChanges();
                }

                return RedirectToAction("Details");


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

    public IActionResult GetData(int draw, int start, int length)
    {
        var allData = (from PI in _context.PersonalInformation
                            join E in _context.Education
                           on PI.InternId equals E.InternId
                       join GD in _context.GuardianDetails
                       on PI.InternId equals GD.InternId
                       join R in _context.References
                       on PI.InternId equals R.InternId
                       select new
                       {
                           InternId = PI.InternId,
                           FirstName = PI.FirstName,
                           LastName = PI.LastName,
                           Mobile = PI.Mobile,
                           Email = PI.Email,
                           HasLicence = PI.HasLicence,
                           SchoolOrCollegeName = E.SchoolOrCollegeName,
                           Major = E.Major,
                           Address = GD.Address,
                           CollegeorCompany = R.CollegeorCompany,

                       }).ToList();
        var data = allData.Skip(start).Take(length).ToList();




        // Prepare the response
        var response = new
        {
            draw = draw,
            recordsTotal = allData.Count,
            recordsFiltered = allData.Count,

            data = data
        };

        return Json(response);
    }



    [HttpGet]
    public IActionResult Edit(int id)

    {
        try

        {


            var data = (from PI in _context.PersonalInformation
                        join E in _context.Education
                        on PI.InternId equals E.InternId
                        join GD in _context.GuardianDetails
                        on PI.InternId equals GD.InternId
                        join R in _context.References
                        on PI.InternId equals R.InternId
                        where PI.InternId == id
                        select new
                        {
                            PersonalInformation = PI,
                            Education = E,
                            GuardianDetails = GD,
                            References = R,
                        }).FirstOrDefault();

            if (data != null)
            {
                return NotFound();
            }
            return View(data);

        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(InternshipFormViewModel model)
    {
        if (ModelState.IsValid)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
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


