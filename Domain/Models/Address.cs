using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Address
    {
        private int _id;
        private int _number;
        private SideType _side;
        private string _zipCode;
        private string _street;
        private string _image;

        public int Id { get => this._id; private  set => _id = value; }
        public int Number { get => this._number; private set => this._number = value; }
        public SideType Side { get => this._side; private set => this._side = value; }
        public string ZipCode { get => this._zipCode; private set => this._zipCode = value; }
        public string Street { get => this._street; private set => this._street = value; }
        public string Image { get => this._image; private set => this._image = value; }

        public Address(int id, int number, SideType side, string zipCode, string street, string image)
        {
            this.Id = id;
            this.Number = number;
            this.Side = side;
            this.ZipCode = zipCode;
            this.Street = street;
            this.Image = image;
        }
    }
}
