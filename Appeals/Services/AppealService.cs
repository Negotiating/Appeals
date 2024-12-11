using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Services
{
    public class AppealService : IAppealService
    {
        private readonly IAppealRepository _appealRepository;

        public AppealService(IAppealRepository appealRepository)
        {
            _appealRepository = appealRepository;
        }

        public async Task<IEnumerable<AppealDTO>> GetAllAsync()
        {
            var appeals = await _appealRepository.GetAllAsync();
            return appeals.Select(appeal => new AppealDTO
            {
                Id = appeal.Id,
                Title = appeal.Title,
                Text = appeal.Text,
                CreationDate = appeal.CreationDate,
                DecisionDate = appeal.DecisionDate,
                Status = new StatusDTO { Id = appeal.IdStatus},
                Topic = new TopicDTO { Id = appeal.IdTopic }
            });
        }
        public async Task<Appeal> GetByIdAsync(int id)
        {
            return await _appealRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(Appeal appeal)
        {
            await _appealRepository.AddAsync(appeal);
        }
        public async Task UpdateAsync(Appeal appeal)
        {
            await _appealRepository.UpdateAsync(appeal);
        }
        public async Task DeleteAsync(int id)
        {
            await _appealRepository.DeleteAsync(id);
        }
        
    }
}