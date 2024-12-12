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
        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _statusRepository.GetAllAsync();
        }
        public async Task<Status> GetByIdAsync(int id)
        {
            return await _statusRepository.GetByIdAsync(id);
        }
    }
}
