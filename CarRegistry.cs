using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    public class CarRegistry
    {
        private readonly List<Car> _cars = new List<Car>();
        private const string FilePath = "cars.txt";

        public CarRegistry()
        {
            LoadCars();
        }

        public void AddCar(Car car)
        {
            _cars.Add(car);
            SaveCars();
        }

        public List<Car> GetCarsByOwner(CarOwner owner)
        {
            return _cars.FindAll(car => car.OwnerId == owner.Id);
        }

        private void SaveCars()
        {
            try
            {
                using (var writer = new StreamWriter(FilePath))
                {
                    foreach (var car in _cars)
                    {
                        writer.WriteLine($"{car.LicensePlate}|{car.Make}|{car.Model}|{car.YearOfManufacture}|{car.Mileage}|{car.RentalRates.DailyRate}|{car.RentalRates.WeeklyRate}|{car.RentalRates.MonthlyRate}|{car.OwnerId}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving cars: {ex.Message}");
            }
        }

        private void LoadCars()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    using (var reader = new StreamReader(FilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 9)
                            {
                                var car = new Car
                                {
                                    LicensePlate = parts[0],
                                    Make = parts[1],
                                    Model = parts[2],
                                    YearOfManufacture = int.Parse(parts[3]),
                                    Mileage = int.Parse(parts[4]),
                                    RentalRates = new RentalRates
                                    {
                                        DailyRate = decimal.Parse(parts[5]),
                                        WeeklyRate = decimal.Parse(parts[6]),
                                        MonthlyRate = decimal.Parse(parts[7])
                                    },
                                    OwnerId = int.Parse(parts[8])
                                };
                                _cars.Add(car);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading cars: {ex.Message}");
                }
            }
        }
    }

}
