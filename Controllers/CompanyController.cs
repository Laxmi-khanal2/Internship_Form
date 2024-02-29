using Microsoft.AspNetCore.Mvc;

namespace InternshipForm.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
