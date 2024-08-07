namespace SWAD_Assignment2_GrpC
{
    public class Car
    {
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

        public float RentalRate { get; set; }

        // Constructor
        public Car(int id, string model, string make, int year, string status, int mileage, string listingName, string licensePlateNumber, bool insuranceStatus, string description, float rentalRate)
        {
            Id = id;
            Model = model;
            Make = make;
            Year = year;
            Status = status;
            Mileage = mileage;
            ListingName = listingName;
            LicensePlateNumber = licensePlateNumber;
            InsuranceStatus = insuranceStatus;
            Description = description;
            RentalRate = rentalRate;
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

        // Start of Yong Shyan's methods
        // -----------------------------------------------------------------------------------------------
        public float GetRentalRate()
        {
            return RentalRate;
        }
        // -----------------------------------------------------------------------------------------------
        // End of Yong Shyan's methods
    }
}