using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.Service.Interface;
using InternshipForm.Views.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternshipForm.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDBContext _context;
            private readonly ICompanyService _companyService;



        public IActionResult AppliedStudent(int internshipId)
        {
            var appliedStudents = _companyService.GetAppliedStudents(internshipId);

            //var viewModel = new CompanyFormViewModel
            //{
            //     // Assuming AppliedInternships is not relevant here
            //    PersonalInformation = appliedStudents
            //};

            return View(appliedStudents);
        }


        public CompanyController(ApplicationDBContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }



        public IActionResult Index()
        {
            return View();

        }

        public IActionResult UpdateApplicationStatus(int InternId, string status)
        {
            var student = _context.AppliedInternships.Find(InternId);
            if (student != null)
            {
                student.Status = status;
                _context.SaveChanges();
                TempData["Message"] = "Application status updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Student not found";
            }

            return RedirectToAction("Index");
        }

        public IActionResult ViewCreatedInternship()
        {
            CreateInternship createInternship = new CreateInternship();
            List<CreateInternship> Internship= _context.CreateInternship.ToList();
            return View(Internship);
        }


      
        [HttpGet]
        public IActionResult CreateProfile(int Id)
        {
            CompanyProfile companyProfile = new CompanyProfile();
            return View(companyProfile);
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProfile(CompanyProfile company)
        {
            //if(ModelState.IsValid)
            {
                var result = _companyService.saveCreateCompanyProfile(company);
                return RedirectToAction("Index");
               
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
            if (result != null) 
            {
                return RedirectToAction("CreatedInternship", new { Id = result});
            }
        
            return View();
        }
    }  
}




 
