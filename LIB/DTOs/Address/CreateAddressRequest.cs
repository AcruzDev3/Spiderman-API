namespace LIB.DTOs.Address
{
    public class CreateAddressRequest
    {
        public int Number { get; set; }
        public string? Side { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Image { get; set; }
    }
}
