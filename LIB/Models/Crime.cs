using LIB.ViewModels;
using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class Crime
{
    public int CrimeId { get; set; }

    public int AddressId { get; set; }

    public int GradeId { get; set; }

    public int TypeId { get; set; }

    public string? Description { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public bool Status { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual CrimeGrade Grade { get; set; } = null!;

    public virtual CrimeType Type { get; set; } = null!;

    public virtual ICollection<Criminal> Criminals { get; set; } = new List<Criminal>();

    public virtual ICollection<User> Heroes { get; set; } = new List<User>();

    /// <summary>
    /// Constructor para crear un crimen en base de datos
    /// </summary>
    /// <param name="viewModel">CrimeViewModel</param>
    /// <param name="grade">CrimeGrade</param>
    /// <param name="type">CrimeType</param>
    /// <param name="addressViewModel">AddressViewModel</param>
    public Crime(CrimeViewModel viewModel, CrimeGrade grade, CrimeType type, AddressViewModel addressViewModel) {
        if(viewModel == null) throw new Exception("El modelo vista del crimen no es válido");
        if(grade == null) throw new Exception("El grado del crimen no es válido");
        if(type == null) throw new Exception("El tipo del crimen no es válido");
        if(addressViewModel == null) throw new Exception("El modelo vista de la dirección no es válido");

        this.AddressId = viewModel.Address.Id;
        this.Grade = grade; 
        this.Type = type;  
        this.Description = viewModel.Description;
        this.DateStart = DateTime.Now;
        this.DateEnd = null;
        this.Status = false;
    }
}

