using LIB.ViewModels;
using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int Number { get; set; }

    public string Side { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();


    public Address(AddressViewModel viewModel) {
        if(viewModel == null) 
            throw new Exception("La vista de la direccion no puede ser nulo");

        this.Number = viewModel.Number;
        this.Side = viewModel.Side.ToString();
        this.ZipCode = viewModel.ZipCode;
        this.Street = viewModel.Street;
        this.Image = viewModel.Image;
    }

}
