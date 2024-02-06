using InternshipForm.Data;
using InternshipForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



            return Json(response);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try

            {
                PersonalInformation personal = new PersonalInformation();

                personal = (from PersonalInformation in _context.PersonalInformation where PersonalInformation.Id == id select PersonalInformation).FirstOrDefault();
                return View(personal);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PersonalInformation personal)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    _context.Entry(personal).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound();
                }

                return View(personal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                PersonalInformation personal = _context.PersonalInformation.FirstOrDefault(p => p.Id == id);

                if (personal == null)
                {
                    return NotFound(); // or handle not found scenario as per your requirement
                }

                return View(personal);
            }
            catch (Exception)
            {
                throw;
            }
        }
    
      [HttpPost]
        public IActionResult Delete(PersonalInformation personal)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    _context.Entry(personal).State = EntityState.Deleted;
                    _context.SaveChanges();

                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound();
                }

                return View(personal);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index");


        }
    }
}
