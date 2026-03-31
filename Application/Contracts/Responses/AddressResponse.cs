using Domain.Enums;

namespace Application.Contracts.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public SideType Side { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Image { get; set; }
    }
}
