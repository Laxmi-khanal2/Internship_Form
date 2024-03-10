using Humanizer;
using InternshipForm.Data;
using InternshipForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userRole))
            {
                if (userRole == "Company")
                {
                    var company = _context.RegisterCompany.FirstOrDefault(c => c.Email == userEmail);
                    if (company != null)
                    {
                        return RedirectToAction("Index", "Company");
                    }
                }
                else if (userRole == "User")
                {
                    var user = _context.RegisterUser.FirstOrDefault(u => u.Email == userEmail);
                    if (user != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // If user email or role is not found, or if there's no matching user or company in the database, redirect to login page
            return RedirectToAction("UserLogin");
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

                //  Set session variables to indicate user authentication
                HttpContext.Session.SetString("UserEmail", registerUser.Email);
                HttpContext.Session.SetString("UserRole", "User");
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
                //check if the user is already registered
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", "User");
                return RedirectToAction("Index", "Home");
            }


            ModelState.AddModelError("", "Invalid email and password");

            return View("UserLogin",registerUser);
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
                ViewBag.Message = "Company Details Saved";

                // Set session variables to indicate user authentication
                HttpContext.Session.SetString("UserEmail", registerCompany.Email);
                HttpContext.Session.SetString("UserRole", "Company");

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
            var company = _context.RegisterCompany.FirstOrDefault(x => x.Email == registerCompany.Email && x.Password == registerCompany.Password);

            if (company != null)
            {
                // Set session variables to indicate user authentication
                HttpContext.Session.SetString("UserEmail", company.Email);
                HttpContext.Session.SetString("UserRole", "Company");

                return RedirectToAction("Index", "Company");
            }

            ModelState.AddModelError("", "Invalid email and password");
            return View("CompanyLogin",registerCompany);
        }

    }
}
