using Appeals.Data;
using Appeals.Interfaces;
using Appeals.Models;
using Microsoft.EntityFrameworkCore;

namespace Appeals.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _context.Topic.ToListAsync();
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _context.Topic.FindAsync(id);
        }

    }
}
