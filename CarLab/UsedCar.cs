using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLab
{
    public class UsedCar : Car
    {
        public double Mileage { get; set; }
        public UsedCar(string Make, string Model, int Year, decimal Price, double Mileage) : base(Make, Model, Year, Price)
        {
            this.Make = Make;
            this.Model = Model;
            this.Year = Year;
            this.Price = Price;
            this.Mileage = Mileage;
        }
        public override string ToString()
        {
            return base.ToString() + "  (Used) " + Mileage + " miles";
        }
    }
}
