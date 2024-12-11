using Microsoft.EntityFrameworkCore;
using Appeals.Interfaces;
using Appeals.Models;
using Appeals.Data;

namespace Appeals.Repositories
{
    public class AppealRepository : IAppealRepository
    {
        private readonly AppDbContext _context;

        public AppealRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appeal>> GetAllAsync()
        {
            return await _context.Appeal.ToListAsync();
        }

        public async Task<Appeal> GetByIdAsync(int id)
        {
            return await _context.Appeal.FindAsync(id);
        }

        public async Task AddAsync(Appeal appeal)
        {
            _context.Appeal.Add(appeal);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Appeal appeal)
        {
            _context.Appeal.Update(appeal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appeal = await _context.Appeal.FindAsync(id);
            if (appeal != null) 
            { 
                _context.Appeal.Remove(appeal);
                await _context.SaveChangesAsync();
            }
        }
    }
}