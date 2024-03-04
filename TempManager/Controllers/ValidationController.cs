using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TempManager.Models;

namespace TempManager.Controllers
{
    public class ValidationController : Controller
    {
        private readonly TempManagerContext _context;
        
        public ValidationController(TempManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult CheckDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                var tempWithDate = _context.Temps.FirstOrDefault(t => t.Date == parsedDate);
                if (tempWithDate == null)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, errorMessage = "A Temp object with this date already exists." });
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Invalid date format." });
            }
        }
    }
}
