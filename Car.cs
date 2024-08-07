using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    public class Car
    {
        private string model;
        private string make;
        private int year;
        private string status;
        private int mileage;
        private string listingName;
        private string licensePlateNumber;
        private bool insuranceStatus;
        private string description;
        private RentalRates rentalRates; // Updated to RentalRates

        // Properties
        public int Id { get; set; }

        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public int Mileage { get; set; }
        public string ListingName { get; set; }
        public string LicensePlateNumber { get; set; }
        public bool InsuranceStatus { get; set; }
        public string Description { get; set; }
        public RentalRates { get; set; } // Updated to RentalRates

        // Constructor
        public Car(int id, string model, string make, int year, string status, int mileage, string listingName, string licensePlateNumber, bool insuranceStatus, string description, RentalRates rentalRates)
        {
            Id = id;
            this.model = model;
            this.make = make;
            this.year = year;
            this.status = status;
            this.mileage = mileage;
            this.listingName = listingName;
            this.licensePlateNumber = licensePlateNumber;
            this.insuranceStatus = insuranceStatus;
            this.description = description;
            this.rentalRates = rentalRates;
        }

        // Aaron's Methods
        // -----------------------------------------------------------------------------------------------
        // Method to verify car status
        public bool VerifyCarStatus()
        {
            if (Status == "Reserved" || Status == "Unavailable")
            {
                Console.WriteLine("Car is unavailable for reservation. Please select a different car.");
                return false;
            }
            else
            {
                Console.WriteLine("The car is available for booking. You may proceed.");
                return true;
            }
        }
        // -----------------------------------------------------------------------------------------------
        // End of Aaron's methods
    }

}

}
