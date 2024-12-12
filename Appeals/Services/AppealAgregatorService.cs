﻿using Appeals.Interfaces;
using Appeals.Models;
using Appeals.Mappers;

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

            var appealDtos = appeals.Select(appeal => Mapper.AppealToDTO(appeal, 
                                                                         statuses.FirstOrDefault(s => s.Id == appeal.IdStatus), 
                                                                         topics.FirstOrDefault(t => t.Id == appeal.IdTopic)))
                                    .ToList();
            return appealDtos;
        }
        public async Task<AppealDTO> GetAppealByIdAsync(int id)
        {
            var appeal = await _appealService.GetByIdAsync(id);
            var topic = await _topicService.GetByIdAsync(appeal.IdTopic);
            var status = await _statusService.GetByIdAsync(appeal.IdStatus);
            return Mapper.AppealToDTO(appeal, status, topic);
        }

        public async Task AddNewAppeal(AppealDTO appeal)
        {

        }
    }
}