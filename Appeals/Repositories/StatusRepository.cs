using Appeals.Data;
using Appeals.Interfaces;
using Appeals.Models;
using Microsoft.EntityFrameworkCore;

namespace Appeals.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;

        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _context.Statuses.ToListAsync();
        }
        public async Task<Status> GetByIdAsync(int id)
        {
            return await _context.Statuses.FindAsync(id);
        }
    }
}
