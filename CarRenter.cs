namespace SWAD_Assignment2_GrpC
{
    public class CarRenter : User
    {
        public bool PrimeStatus { get; set; }
        public bool Eligibility { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool DriverLicense { get; set; }
        public float MonthlyExpenditure { get; set; }

        public CarRenter(int id, string address, string email, string username, int contactNumber, string name, DateTime dateJoined)
            : base(id, address, email, username, contactNumber, name, dateJoined)
        {
            // Initialize CarRenter specific properties
            PrimeStatus = false;
            Eligibility = true;
            DateOfBirth = DateTime.Now.AddYears(-25);
            DriverLicense = true;
            MonthlyExpenditure = 0;
        }

        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public void DisplayInformation()
        {
            Console.WriteLine($"Renter Information:\nName: {Name}\nEmail: {Email}\nPrime Status: {PrimeStatus}\nEligibility: {Eligibility}");
        }

        public void DisplayAppealHistory()
        {
            Console.WriteLine($"Appeal History for {Name}:");
            // In a real system, you would fetch and display the actual history
            Console.WriteLine("No previous appeals found.");
        }
        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods
    }
}