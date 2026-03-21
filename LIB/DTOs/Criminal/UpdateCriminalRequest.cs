using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.DTOs.Criminal
{
    public class UpdateCriminalRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Risk { get; set; }
        public string? Image { get; set; }
        public DateTime Since { get; set; }
    }
}
