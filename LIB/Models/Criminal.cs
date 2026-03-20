using System;
using System.Collections.Generic;
using LIB.ViewModels;

namespace LIB.Models;

public partial class Criminal
{
    public int CriminalId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int RiskId { get; set; }

    public string? Image { get; set; }

    public DateTime CriminalSince { get; set; }

    public virtual CriminalRiskLevel Risk { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();

    public Criminal(CriminalViewModel viewModel, CriminalRiskLevel risk) { 
        this.Name = viewModel.Name;
        this.Description = viewModel.Description;
        this.RiskId = risk.CriminalRiskLevelId;
        this.RiskId = risk.CriminalRiskLevelId;
        this.Image = viewModel.Image;
        this.CriminalSince = viewModel.Since;
    }
}
