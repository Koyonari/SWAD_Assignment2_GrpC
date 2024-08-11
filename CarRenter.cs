using System;

/*namespace SWAD_Assignment2_GrpC
{
    public class CarRenter : User
    {
        public bool PrimeStatus { get; set; }
        public bool Eligibility { get; set; }
        public int Points { get; set; }

        public CarRenter(int id, string address, string email, string username, int contactNumber, string name, DateTime dateJoined)
            : base(id, address, email, username, contactNumber, name, dateJoined)
        {
            PrimeStatus = false;
            Eligibility = true;
            Points = 0;
        }

        public void DisplayInformation()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Prime Status: {PrimeStatus}");
            Console.WriteLine($"Eligibility: {Eligibility}");
            Console.WriteLine("Account: Active");
            Console.WriteLine($"Points: {Points}");
        }
    }
}*/

namespace SWAD_Assignment2_GrpC
{
    public class CarRenter : User
    {
        public bool PrimeStatus { get; set; }
        public bool Eligibility { get; set; }
        public bool DriverLicense { get; set; }
        public int Points { get; set; }
        public float MonthlyExpenditure { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public CarRenter(int id, string address, string email, string username, int contactNumber, string name, DateTime dateJoined)
            : base(id, address, email, username, contactNumber, name, dateJoined)
        {
            // Initialize CarRenter specific properties
            PrimeStatus = false;
            Eligibility = true;
            DriverLicense = true;
            MonthlyExpenditure = 0;
            Points = 9;
        }

        // Aaron's Methods
        // -----------------------------------------------------------------------------------------------
        // Method to add a booking
        public void AddBooking(Booking booking)
        {
            Bookings.Add(booking);
        }
        // -----------------------------------------------------------------------------------------------
        // End of Aaron's methods

        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public void DisplayInformation()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Prime Status: {PrimeStatus}");
            Console.WriteLine($"Eligibility: {Eligibility}");
            Console.WriteLine("Account: Active");
            Console.WriteLine($"Points: {Points}");
        }
        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods
    }
}