using Domain.Models;
using Application.Contracts.Responses;

namespace Application.Mappers
{
    public static class AddressMapper
    {
        public static AddressResponse ToResponse(Address address) {
            return new AddressResponse
            {
                Id = address.Id,
                Number = address.Number,
                Side = address.Side,
                ZipCode = address.ZipCode,
                Street = address.Street,
                Image = address.Image,
            };
        }
    }
}
