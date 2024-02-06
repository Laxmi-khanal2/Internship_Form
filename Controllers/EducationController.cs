using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InternshipForm.Controllers
{
    public class EducationController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EducationController(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public IActionResult Education()
        {
            InternshipFormViewModel model = new InternshipFormViewModel();
            model.Education = new List<Education> { new Education() }; 


            return View(model);
        }
        [HttpPost]
        public IActionResult Education(InternshipFormViewModel model)
        {
            if (ModelState.IsValid)
            {   
                foreach (var educationItem in model.Education)
                { 
                    _context.Education.Add(educationItem);
                }

                _context.SaveChanges();

               
                return RedirectToAction("Index"); 
            }

            return View(model);
        }

    }

}
