using Microsoft.AspNetCore.Mvc;

namespace Appeals.Controllers
{
    public class AppealsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
