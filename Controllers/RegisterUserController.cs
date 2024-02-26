using InternshipForm.Data;
using InternshipForm.Models;
using Microsoft.AspNetCore.Mvc;

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
           if(ModelState.IsValid)
            {
                _context.RegisterUser.Add(registerUser);
                _context.SaveChanges();
                return RedirectToAction("Index","Home");
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
                
            
            return View(registerUser);
        }
    }
}
