using System;
using System.Collections.Generic;
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

        public async Task<List<Appeal>> GetAll()
        {
            return await _context.Appeals.ToListAsync();
        }

        public async Task<Appeal> GetById(int id)
        {
            return await _context.Appeals.FindAsync(id);
        }

        public async Task Add(Appeal appeal)
        {
            _context.Appeals.Add(appeal);
            await _context.SaveChangesAsync();
        }
        
        public async Task Update(Appeal appeal)
        {
            _context.Appeals.Update(appeal);
            await _context.SaveChangesAsync();
        }

        public async Task<Appeal> Delete(int id)
        {
            var appeal = await _context.Appeals.FindAsync(id);
            if (appeal != null) 
            { 
                _context.Appeals.Remove(appeal);
                await _context.SaveChangesAsync();
            }
            return appeal;
        }
    }
}