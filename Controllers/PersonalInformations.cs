using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InternshipForm.Controllers
{
    public class PersonalInformations : Controller
    {
        private readonly ApplicationDBContext _context;
        //this is the constructor for class personalinformations
        public PersonalInformations(ApplicationDBContext context)
        {
            _context = context;
        }

        // this is method of the class personalInformation which ddefine how the object behaves
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PersonalInformationDetails()
        {
            return View();
        }
       //[ HttpPost]  
       //  public ActionResult PersonalInformationDetails(PersonalInformation pi)
       // { 
       //     //Write your database insert code / activities  
       //     return RedirectToAction("PersonalInformationDetails");
       // }


        [HttpPost]
        public IActionResult PersonalInformationDetails(PersonalInformation personalInformation)
         {
            if (ModelState.IsValid)
            {
                return RedirectToAction("PersonalInformationsDetails");
            }
        
            return View(personalInformation);

        }

    }
}
