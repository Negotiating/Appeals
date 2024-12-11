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
        private readonly IAppealService _appealService;
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;
        private readonly IStatusService _statusService;
       // private readonly IAdressService _adressService;
       // private readonly IPlotService _plotService;

        public AppealsController(IAppealService appealService,
                                 IUserService userService,
                                 ITopicService topicService,
                                 IStatusService statusService//,
                               //  IAdressService adressService,
                               //  IPlotService plotService
                                 )
        {
            _appealService = appealService;
            _topicService = topicService;
            _statusService = statusService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppeals()
        {
            var appeals = await _appealService.GetAllAsync();
            var topics = await _topicService.GetAllAsync();
            var statuses = await _statusService.GetAllAsync();

            foreach (var appeal in appeals)
            {
                var a = topics.Where(x => x.Id == appeal.Topic.Id).FirstOrDefault();
                appeal.Topic = a;
                var b= statuses.Where(x => x.Id == appeal.Status.Id).FirstOrDefault();
                appeal.Status = b;
            }

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
