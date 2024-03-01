﻿using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternshipForm.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDBContext _context;
            private readonly ICompanyService _companyService;

        

        public CompanyController(ApplicationDBContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult CreateProfile(int Id)
        {
            CompanyProfile company = new CompanyProfile();

            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProfile(CompanyProfile company)
        {
            //if(ModelState.IsValid)
            {
                var result = _companyService.saveCreateCompanyProfile(company);
              //  return RedirectToAction("");
            }
            return View();
        }


        [HttpGet]
        public IActionResult CreateInternship()
        {
            CreateInternship model = new CreateInternship();
    

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInternship(CreateInternship create)
        {
            var result = _companyService.saveCreateInternship(create);
            return View();
        }
    }  
}




 
