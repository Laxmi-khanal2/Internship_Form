using InternshipForm.Data;
using InternshipForm.Models;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InternshipForm.Controllers
{
    public class PersonalInformationsController : Controller
    {
        private readonly ApplicationDBContext _context;
        //this is the constructor for class personalinformations
        public PersonalInformationsController(ApplicationDBContext context)
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
            PersonalInformation personalInformation = new PersonalInformation();

            return View(personalInformation);
        }
      


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonalInformationDetails(PersonalInformation personalInformation)
         {
            if (ModelState.IsValid)
            {
                _context.PersonalInformation.Add(personalInformation);
              var result=  _context.SaveChanges();
                if(result!=null)
                {
                    personalInformation.isSubmitted = true;
                }
                //return RedirectToAction("PersonalInformationDetails");
             
            }
        
            return View(personalInformation);

        }

        [HttpGet]
        public IActionResult GuardianDetails() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuardianDetails(GuardianDetails guardianDetails)
        {
            if (ModelState.IsValid)

            {
                _context.GuardianDetails.Add(guardianDetails);
                _context.SaveChanges();
                return RedirectToAction("GuardianDetails");
            }
            return View(guardianDetails);
        }
        

        //Education Controller
        [HttpGet]
        public IActionResult Education()
        {

            var model = new List<Education> { new Education() };


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Education(List<Education> model)
        {
            if (ModelState.IsValid)
            {
                
                foreach (var education in model)
                {
                    _context.Education.Add(education);
                }
                _context.SaveChanges();

               


                return RedirectToAction("Education");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult References()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult References(References references)
        {
            if(ModelState.IsValid)
            {
                _context.References.Add(references);
                _context.SaveChanges();
                return RedirectToAction("References");
            }
            return View(references);
        }
    }
}
