using Application.Contracts.Requests.Address;
using Application.Contracts.Responses;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController, Route("api/[controller]")]
    public class AddressController : Controller {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService) {
            this._addressService = addressService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AddressResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await this._addressService.GetById(id));

        [HttpGet]
        [ProducesResponseType(typeof(List<AddressResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index()
            => Ok(await this._addressService.GetAll());

        [HttpPost]
        [ProducesResponseType(typeof(AddressResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateAddressRequest request) {
            AddressResponse addressResponse = await this._addressService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = addressResponse.Id}, addressResponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AddressResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateAddressRequest request)
            => Ok(await this._addressService.Update(request));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id) {
            await this._addressService.Delete(id);
            return NoContent();
        }
    }
}
