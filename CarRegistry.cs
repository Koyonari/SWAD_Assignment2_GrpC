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
        private readonly HashSet<string> _validMakes;
        private readonly HashSet<string> _validModels;
        private readonly string _carsFilePath;

        public CarRegistry(string makesFilePath, string modelsFilePath, string carsFilePath)
        {
            _validMakes = LoadValidMakes(makesFilePath);
            _validModels = LoadValidModels(modelsFilePath);
            _carsFilePath = carsFilePath;
            LoadCars();
        }

        public void AddCar(Car car)
        {
            _cars.Add(car);
            SaveCars();
        }

        public bool IsValidMake(string make)
        {
            return _validMakes.Contains(make);
        }

        public bool IsValidModel(string model)
        {
            return _validModels.Contains(model);
        }

        public List<Car> GetAllCars()
        {
            return _cars;
        }

        private HashSet<string> LoadValidMakes(string filePath)
        {
            var makes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    makes.Add(line.Trim());
                }
            }
            else
            {
                Console.WriteLine("Makes file not found.");
            }
            return makes;
        }

        private HashSet<string> LoadValidModels(string filePath)
        {
            var models = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    models.Add(line.Trim());
                }
            }
            else
            {
                Console.WriteLine("Models file not found.");
            }
            return models;
        }

        private void SaveCars()
        {
            var lines = new List<string>();
            foreach (var car in _cars)
            {
                var line = $"{car.Id}|{car.Model}|{car.Make}|{car.Year}|{car.Status}|{car.Mileage}|{car.ListingName}|{car.LicensePlateNumber}|{car.InsuranceStatus}|{car.Description}|{car.RentalRate}";
                lines.Add(line);
            }
            File.WriteAllLines(_carsFilePath, lines);
        }

        private void LoadCars()
        {
            if (File.Exists(_carsFilePath))
            {
                var lines = File.ReadAllLines(_carsFilePath);
                _cars.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 11)
                    {
                        var car = new Car(
                            int.Parse(parts[0]),
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            parts[4],
                            int.Parse(parts[5]),
                            parts[6],
                            parts[7],
                            bool.Parse(parts[8]),
                            parts[9],
                            float.Parse(parts[10])
                        );
                        _cars.Add(car);
                    }
                }
            }
            else
            {
                Console.WriteLine("Cars file not found. Starting with an empty list.");
            }
        }
    }

}

