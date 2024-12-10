using Microsoft.AspNetCore.Mvc;

namespace Appeals.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
