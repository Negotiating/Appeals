using Microsoft.AspNetCore.Mvc;
using Appeals.Services;
using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Controllers
{
    public class AppealsController : Controller
    {
        private readonly IAppealService _appealService;

        public AppealsController(IAppealService appealService)
        {
            _appealService = appealService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppeals()
        {
            var appeals = await _appealService.GetAllAsync();
            return Json(appeals);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppealById(int id)
        {
            var appeal = await _appealService.GetByIdAsync(id);
            return Json(appeal);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppeal([FromBody] Appeal appeal)
        {
            await _appealService.AddAsync(appeal);
            return Json(new { success = true });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppeal([FromBody] Appeal appeal)
        {
            await _appealService.UpdateAsync(appeal);
            return Json(new { success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppeal(int id)
        {
            await _appealService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
