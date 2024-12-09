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

        public async Task<List<Appeal>> GetAll()
        {
            return await _appealRepository.GetAll();
        }
        public async Task<Appeal> GetById(int id)
        {
            return await _appealRepository.GetById(id);
        }
        public async Task Add(Appeal appeal)
        {
            await _appealRepository.Add(appeal);
        }
        public async Task Update(Appeal appeal)
        {
            await _appealRepository.Update(appeal);
        }
        public async Task Delete(int id)
        {
            await _appealRepository.Delete(id);
        }
        
    }
}