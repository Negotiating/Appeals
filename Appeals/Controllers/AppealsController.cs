using Microsoft.AspNetCore.Mvc;
using Appeals.Interfaces;
using Appeals.Models;
using Appeals.Services;

namespace Appeals.Controllers
{
    public class AppealsController : Controller
    {
        private readonly AppealAgregatorService _appealAgregatorService;
        private readonly AppealService _appealService;
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

        [HttpGet]
        public async Task<IActionResult> GetAppealById(int id)
        {
            var appeal = await _appealAgregatorService.GetAppealByIdAsync(id);
            return Json(appeal);
        }

        [HttpPost]
        public async Task<IActionResult> AddAppeal([FromBody]AppealDTO appeal)
        {
            if (appeal == null)
            {
                return BadRequest("Appeal data is null");
            }
            try
            {
                await _appealAgregatorService.AddNewAppeal(appeal);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var topics = await _appealAgregatorService.GetAllTopicsAsync();
            return Json(topics);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var topics = await _appealAgregatorService.GetAllStatusesAsync();
            return Json(topics);
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateAppeal([FromBody] Appeal appeal)
        //{
        //    await _appealService.UpdateAsync(appeal);
        //    return Json(new { success = true });
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteAppeal(int id)
        {
            await _appealService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
