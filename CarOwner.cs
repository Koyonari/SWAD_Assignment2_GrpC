using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{ 

    public class CarOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Car> RegisteredCars { get; private set; } = new List<Car>();

        public void AddCar(Car car)
        {
            RegisteredCars.Add(car);
        }
    }


}
