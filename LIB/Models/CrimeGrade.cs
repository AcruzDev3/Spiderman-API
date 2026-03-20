using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class CrimeGrade
{
    public int CrimeGradeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();
}
