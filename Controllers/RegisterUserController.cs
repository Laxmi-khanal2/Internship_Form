using InternshipForm.Data;
using InternshipForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace InternshipForm.Controllers
{
    public class RegisterUserController : Controller

    {
        private readonly ApplicationDBContext _context;
        public RegisterUserController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserRegister()
        {
           
            RegisterUser registerUser = new RegisterUser();
            return View(registerUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserRegister(RegisterUser registerUser)
        {


           // if (ModelState.IsValid)
            {
                _context.RegisterUser.Add(registerUser);
                _context.SaveChanges();
                ViewBag.Message = "User Details Saved";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(RegisterUser registerUser)
        {


            var user = _context.RegisterUser.FirstOrDefault(x => x.Email == registerUser.Email && x.Password == registerUser.Password);
           
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }


            ModelState.AddModelError("", "Invalid email and password");

              if(user.Email == null)
            {
                return RedirectToAction("Email Address is Necessary");
            }
            return View(registerUser);
        }


        [HttpGet]
        public IActionResult CompanyRegister()

        {
            RegisterCompany registerCompany = new RegisterCompany();
            return View(registerCompany);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompanyRegister(RegisterCompany registerCompany)
        {


           if (ModelState.IsValid)
            {
                _context.RegisterCompany.Add(registerCompany);
                _context.SaveChanges();
                return RedirectToAction("Index", "Company");
            }
            return View();
        }

        
        
        [HttpGet]
        public IActionResult CompanyLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompanyLogin(RegisterCompany registerCompany)
        {


            var user = _context.RegisterCompany.FirstOrDefault(x => x.Email == registerCompany.Email && x.Password == registerCompany.Password);

            if (user != null)
            {
                return RedirectToAction("Index", "Company");
            }


            ModelState.AddModelError("", "Invalid email and password");

            if (user.Email == null)
            {
                return RedirectToAction("Email Address is Necessary");
            }
            return View(registerCompany);
        }

    }
}
