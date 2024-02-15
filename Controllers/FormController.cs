﻿using Azure.Core;
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
                model.PersonalInformation = (from PersonalInformation in _context.PersonalInformation where PersonalInformation.InternId == Id select PersonalInformation).FirstOrDefault();

                model.Education = _context.Education.Where(p => p.InternId == Id).ToList();
                if (model.Education.Count == 0)
                {
                    model.Education = new List<Education> { new Education() };
                }
                model.GuardianDetails = (from GuardianDetails in _context.GuardianDetails where GuardianDetails.InternId == Id select GuardianDetails).FirstOrDefault();
                model.References = ( from References in  _context.References where References.InternId == Id select References ).FirstOrDefault();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InternshipFormViewModel model)
        {
            //  if (ModelState.IsValid)

           // {
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
                    _context.Entry(model.PersonalInformation).State = EntityState.Modified;
                   // _context.Update(model.PersonalInformation);
                    //}

                    //Update Education

                    foreach (var education in model.Education)
                    {

                        education.InternId = model.PersonalInformation.InternId;


                        _context.Entry(education).State = EntityState.Modified;

                    }


                // Update Guardian Details
                   model.GuardianDetails.InternId = model.PersonalInformation.InternId;

                    _context.Entry(model.GuardianDetails).State = EntityState.Modified;

                    //UPDATE rEFERENCES

                model.References.InternId = model.PersonalInformation.InternId;
                    _context.Entry(model.References).State = EntityState.Modified;


                    // Save changes
                    _context.SaveChanges();
                }

                return RedirectToAction("Details");


            }






            //   return View(model);
          //   }


            //  action controller datatable
            [HttpGet]
            public IActionResult Details()
            {
            var NameFilterOptions = new List<SelectListItem>
            {
                new SelectListItem{ Value = "firstName", Text="FirstName"},
                new SelectListItem{ Value = "middleName", Text="MiddleName"},
                new SelectListItem{ Value = "lastName", Text="LastName"},
            };
            ViewData["NameFilter"] = NameFilterOptions;
                return View();
            }
            [HttpPost]
            public IActionResult GetData(int draw, int start, int length)
            {
                var query = from PI in _context.PersonalInformation
                            join E in _context.Education on PI.InternId equals E.InternId
                            join GD in _context.GuardianDetails on PI.InternId equals GD.InternId
                            join R in _context.References on PI.InternId equals R.InternId
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
                                CollegeorCompany = R.CollegeorCompany
                            };

                // Count total records before applying pagination
                int recordsTotal = query.Count();

                // Apply pagination
                var data = query.Skip(start).Take(length).ToList();

                // Prepare the response
                var response = new
                {
                    draw = draw,
                    recordsTotal = recordsTotal,
                    recordsFiltered = recordsTotal, // No filtering applied in this example

                    data = data
                };

                return Json(response);
            }




        //[HttpGet]
        //public IActionResult Edit(int Id)
        //{ 
        //     var model = new InternshipFormViewModel();

        //    if (Id > 0)
        //    {
        //        model.PersonalInformation = _context.PersonalInformation.FirstOrDefault(p => p.InternId == Id);
        //        model.Education = _context.Education.Where(p => p.InternId == Id).ToList();
        //        if (model.Education.Count == 0)
        //        {
        //            model.Education = new List<Education> { new Education() };
        //        }
        //        model.GuardianDetails = _context.GuardianDetails.FirstOrDefault(p => p.InternId == Id);
        //        model.References = _context.References.FirstOrDefault(p => p.InternId == Id);
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(InternshipFormViewModel model)
        //{
        //    //if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(model.PersonalInformation);

        //            foreach (var education in model.Education)
        //            {
        //                _context.Update(education);
        //            }

        //            _context.Update(model.GuardianDetails);
        //            _context.Update(model.References);

        //            _context.SaveChanges();

        //            return RedirectToAction(nameof(Details));
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle any exceptions here
        //            ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
        //            return View(model);
        //        }
        //    }

        //    // If model state is not valid, return the view with validation errors
        //    return View(model);
        //}


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


