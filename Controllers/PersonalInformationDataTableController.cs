using InternshipForm.Data;
using InternshipForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternshipForm.Controllers
{
    public class PersonalInformationDataTableController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PersonalInformationDataTableController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult PersonalInformationDataTable()
        {
            return View();
        }

        public IActionResult LoadData(int draw, int start, int length)
        
        {
   
            var allData = _context.PersonalInformation.ToList();
            var data = allData.Skip(start).Take(length).ToList();

            var response = new
            {
                draw = draw,
                recordsTotal = allData.Count,
                recordsFiltered = allData.Count,
                data = data,

            };



                return Json( response);
            
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var allData = (from PersonalInformation in _context.PersonalInformation where PersonalInformation.Id == id select PersonalInformation).FirstOrDefault();
                return View(PersonalInformationDataTable);

            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
