using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Services
{
    public class AppealAgregatorService
    {
        private readonly IAppealService _appealService;
        private readonly IUserService _userService;
        private readonly ITopicService _topicService;
        private readonly IStatusService _statusService;

        public AppealAgregatorService(IAppealService appealService,
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
            _userService = userService;
        }

        public async Task<IEnumerable<AppealDTO>> GetAllAppealsAsync()
        {
            var appeals = await _appealService.GetAllAsync();
            var topics = await _topicService.GetAllAsync();
            var statuses = await _statusService.GetAllAsync();

            var appealDtos = appeals.Select(appeal => new AppealDTO
            {
                Id = appeal.Id,
                Title = appeal.Title,
                Text = appeal.Text,
                CreationDate = appeal.CreationDate,
                DecisionDate = appeal.DecisionDate,
                //IdStatus = appeal.IdStatus,
                //IdExecutor = appeal.IdExecutor,
                //IdResident = appeal.IdResident,
                //IdPlot = appeal.IdPlot,
                //IdTopic = appeal.IdTopic,
                //Grade = appeal.Grade,
                //DeletionDate = appeal.DeletionDate,
                Topic = topics.FirstOrDefault(t => t.Id == appeal.IdTopic),
                Status = statuses.FirstOrDefault(s => s.Id == appeal.IdStatus)
            }).ToList();

            return appealDtos;
        }
    }
}
