using Domain.Enums;

namespace Application.Contracts.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public SideType Side { get; set; }
        public string ZipCode { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
