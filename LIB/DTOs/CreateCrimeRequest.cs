using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.DTOs
{
    public class CreateCrimeRequest
    {
        public string GradeName { get; set; }
        public string TypeName { get; set; }
        public string? Description { get; set; }
        public int AddressId { get; set; }
        public List<int> CriminalsIds { get; set; }
    }
}
