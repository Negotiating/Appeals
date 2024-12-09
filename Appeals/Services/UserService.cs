using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            await _repository.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
