using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class CriminalRiskLevel
{
    public int CriminalRiskLevelId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Criminal> Criminals { get; set; } = new List<Criminal>();
}
