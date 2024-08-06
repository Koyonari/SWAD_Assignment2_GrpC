using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class Car
    {
        private string id;
        private string model;
        private string make;
        private int year;
        private string status;
        private int mileage;
        private string listingName;
        private string licensePlateNumber;
        private bool insuranceStatus;
        private string description;
        private float rentalRate;

        // Properties
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Make
        {
            get { return make; }
            set { make = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Mileage
        {
            get { return mileage; }
            set { mileage = value; }
        }

        public string ListingName
        {
            get { return listingName; }
            set { listingName = value; }
        }

        public string LicensePlateNumber
        {
            get { return licensePlateNumber; }
            set { licensePlateNumber = value; }
        }

        public bool InsuranceStatus
        {
            get { return insuranceStatus; }
            set { insuranceStatus = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public float RentalRate
        {
            get { return rentalRate; }
            set { rentalRate = value; }
        }

        // Constructor
        public Car(string id, string model, string make, int year, string status, int mileage, string listingName, string licensePlateNumber, bool insuranceStatus, string description, float rentalRate)
        {
            this.id = id;
            this.model = model;
            this.make = make;
            this.year = year;
            this.status = status;
            this.mileage = mileage;
            this.listingName = listingName;
            this.licensePlateNumber = licensePlateNumber;
            this.insuranceStatus = insuranceStatus;
            this.description = description;
            this.rentalRate = rentalRate;
        }
    }
}
