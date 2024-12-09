using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Repositories
{
    public class AppealRepository : IAppealRepository
    {
        private readonly AppealsDbContext _context;

        public AppealRepository(AppealsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appeal>> GetAllAsync()
        {
            return await _context.Appeals.ToListAsync();
        }

        public async Task<Appeal> GetByIdAsync(int id)
        {
            return await _context.Appeals.FindAsync(id);
        }

        public async Task AddAsync(Appeal appeal)
        {
            _context.Appeals.Add(appeal);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Appeal appeal)
        {
            _context.Appeals.Update(appeal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appeal = await _context.Appeals.FindAsync(id);
            if (appeal != null) 
            { 
                _context.Appeals.Remove(appeal);
                await _context.SaveChangesAsync();
            }
        }
    }
}