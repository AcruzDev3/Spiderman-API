using Domain.Models;
using Application.Contracts.Responses;
using Application.Contracts.Requests.Address;

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

        public static Address ToModel(CreateAddressRequest request, string image) {
            return new Address(
                number: request.Number,
                side: request.Side,
                zipCode: request.ZipCode,
                street: request.Street,
                image: image
            );
        }

        public static Address ToModel(UpdateAddressRequest request, string image) {
            return new Address(
                number: request.Number,
                side: request.Side,
                zipCode: request.ZipCode,
                street: request.Street,
                image: image
            );
        }
    }
}
