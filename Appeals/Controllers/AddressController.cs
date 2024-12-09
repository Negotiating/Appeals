using Appeals.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Appeals.Models;

namespace Appeals.Controllers
{
    [Route("[Address]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> Addresses()
        {
            return Ok(await _addressService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Address(int id)
        {
            return Ok(await _addressService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Address>> Create(Address address)
        {
            await _addressService.AddAsync(address);
            return CreatedAtAction(nameof(Address), new { id = address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Address address)
        {
            if (id != address.Id)
                return BadRequest();
            await _addressService.UpdateAsync(address);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _addressService.DeleteAsync(id);
            return NoContent();
        }
    }
}