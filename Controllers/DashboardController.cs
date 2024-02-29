using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternshipForm.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var FilterOptions = new List<SelectListItem>
            {
                new SelectListItem{Value ="student", Text=" Register as Student"},
                new SelectListItem{Value ="college", Text=" Register as College"},
                new SelectListItem{Value ="company", Text="Register as Company"},
            };
            ViewData["Filter"] = FilterOptions;
            return View();
        }
    }
}
