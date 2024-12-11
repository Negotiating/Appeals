using Microsoft.AspNetCore.Mvc;
using Appeals.Interfaces;
using Appeals.Models;
using Appeals.Mappers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Appeals.Services;

namespace Appeals.Controllers
{
    public class AppealsController : Controller
    {
        private readonly AppealAgregatorService _appealAgregatorService;
        public AppealsController(AppealAgregatorService appealAgregatorService)
        {
            _appealAgregatorService = appealAgregatorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppeals()
        {
            var appeals = await _appealAgregatorService.GetAllAppealsAsync();
            return Json(appeals);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAppealById(int id)
        //{
        //    var appeal = await _appealService.GetByIdAsync(id);
        //    return Json(appeal);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddAppeal([FromBody] Appeal appeal)
        //{
        //    await _appealService.AddAsync(appeal);
        //    return Json(new { success = true });
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateAppeal([FromBody] Appeal appeal)
        //{
        //    await _appealService.UpdateAsync(appeal);
        //    return Json(new { success = true });
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteAppeal(int id)
        //{
        //    await _appealService.DeleteAsync(id);
        //    return Json(new { success = true });
        //}
    }
}
