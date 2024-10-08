﻿namespace SWAD_Assignment2_GrpC
{
    public class CarOwner : User
    {
        public List<Car> Listings { get; set; }

        public CarOwner(int id, string name, string email, string username, int contactNumber, string address, DateTime dateJoined)
            : base(id, name, email, username, contactNumber, address, dateJoined)
        {
            Listings = new List<Car>();
        }


        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public void DisplayInformation()
        {
            Console.WriteLine($"Car Owner ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Contact Number: {ContactNumber}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Date Joined: {DateJoined}");
            Console.WriteLine($"Number of Listings: {Listings.Count}");
        }

        public void DisplayAppealHistory()
        {
            Console.WriteLine("Car Owner Appeal History:");
        }

        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods

        // Start of Daniel's methods
        // -----------------------------------------------------------------------------------------------
        public void RegisterCar(Car car)
        {
            Listings.Add(car);
        }

        public void UpdateCar(Car updatedCar)
        {
            var index = Listings.FindIndex(c => c.Id == updatedCar.Id);
            if (index >= 0)
            {
                Listings[index] = updatedCar;
            }
        }
        // -----------------------------------------------------------------------------------------------
        // End of Daniel's methods
    }
}
