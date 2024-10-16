using Microsoft.AspNetCore.Mvc;

namespace WebAPIDMSPruebaTecnica.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
