using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class CrimeType
{
    public int CrimeTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();
}
