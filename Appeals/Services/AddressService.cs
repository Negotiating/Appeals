using Appeals.Interfaces;
using Appeals.Models;


namespace Appeals.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAdressRepository _addressRepository;

        public AddressService(IAdressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<Address>> GetAllAsync(){
            return await _addressRepository.GetAllAsync();
        }
        
        public async Task<Address> GetByIdAsync(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(Address address)
        {
            await _addressRepository.AddAsync(address);
        }    

        public async Task UpdateAsync(Address address)
        {
            await _addressRepository.UpdateAsync(address);
        }

        public async Task DeleteAsync(int id)
        {

        }
    }
}