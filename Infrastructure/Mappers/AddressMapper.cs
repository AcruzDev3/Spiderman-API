using Domain.Enums;
using Domain.Models;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Mappers
{
    public static class AddressMapper
    {
        public static Address ToDomain(AddressEntity entity) {
             return new Address(
                entity.AddressId,
                entity.Number,
                Enum.Parse<SideType>(entity.Side),
                entity.ZipCode,
                entity.Street,
                entity.Image
            );
        }

        public static AddressEntity ToEntity(Address domain) {
            return new AddressEntity {
                AddressId = domain.Id,
                Number = domain.Number,
                Side = domain.Side.ToString(),
                ZipCode = domain.ZipCode,
                Street = domain.Street,
                Image = domain.Image
            };
        }
    }
}
