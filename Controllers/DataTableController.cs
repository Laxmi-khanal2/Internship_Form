using Microsoft.AspNetCore.Mvc;

namespace InternshipForm.Controllers
{
    public class DataTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeList()
        {
            var data = EmployeeData.GiveMeData();
            return Json(new { data = data });
        }
    }
}
