using Microsoft.AspNetCore.Mvc;

namespace InternshipForm.Controllers
{
    public class DataTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EmployeeList(int draw, int start, int length)
        {
            var allData = EmployeeData.GiveMeData();
            var data = allData.Skip(start).Take(length).ToList();

            var response = new
            {

                draw = draw,
                recordsTotal = allData.Count,
                recordsFiltered = allData.Count,
                data = data
            };

         return Json( response );
        }
      
    }
}
