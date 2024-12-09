using Appeals.Interfaces;
using Appeals.Models;
using Microsoft.EntityFrameworkCore;
using AppealsDbContext = Appeals.Data.AppealsDbContext;

namespace Appeals.Repositories
{
    public class AddressRepository : IAdressRepository
    {
        private readonly AppealsDbContext _context;

        public AddressRepository(AppealsDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }
        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }
        public async Task AddAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            _context.Addresses.Remove(_context.Addresses.Find(id));
            await _context.SaveChangesAsync();
        }
    }
}