using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<IEnumerable<StatusDTO>> GetAllAsync()
        {
            var statuses = await _statusRepository.GetAllAsync();
            return statuses.Select(status => new StatusDTO { Id = status.Id, Name = status.Name });
        }
        public async Task<Status> GetByIdAsync(int id)
        {
            return await _statusRepository.GetByIdAsync(id);
        }
    }
}
