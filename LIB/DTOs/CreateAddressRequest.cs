using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.DTOs
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
