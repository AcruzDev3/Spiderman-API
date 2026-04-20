using Application.Contracts.Requests.Address;
using Application.Contracts.Responses;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AddressController : Controller {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService) {
            this._addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._addressService.GetById(id));

        [HttpGet]
        public async Task<IActionResult> Index()
            => Ok(await this._addressService.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressRequest request) {
            AddressResponse addressResponse = await this._addressService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = addressResponse.Id}, addressResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAddressRequest request)
            => Ok(await this._addressService.Update(request));

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await this._addressService.Delete(id);
            return NoContent();
        }
    }
}
