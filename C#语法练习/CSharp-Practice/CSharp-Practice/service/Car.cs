using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Practice.service
{
    internal class Car : IEquatable<Car>
    {
        public string? Make {  get; set; }
        public string? Model { get; set; }
        public string? Year {  get; set; }
        public bool Equals(Car? car) => 
            car is not null && 
            (Make,Model,Year) == (car.Make,car.Model,car.Year);
    }
}
