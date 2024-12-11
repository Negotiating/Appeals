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

        public async Task<IEnumerable<Appeal>> GetAllAsync()
        {
            return await _appealRepository.GetAllAsync();
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