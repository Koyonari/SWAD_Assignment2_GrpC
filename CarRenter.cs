namespace SWAD_Assignment2_GrpC
{
    internal class CarRenter
    {
        // Properties
        public bool PrimeStatus { get; set; }

        public bool Eligibility { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DriverLicense DriversLicense { get; set; } // Reference to DriverLicense

        public float MonthlyExpenditure { get; set; }

        // Constructor
        public CarRenter(bool primeStatus, bool eligibility, DateTime dateOfBirth, DriverLicense driversLicense, float monthlyExpenditure)
        {
            PrimeStatus = primeStatus;
            Eligibility = eligibility;
            DateOfBirth = dateOfBirth;
            DriversLicense = driversLicense;
            MonthlyExpenditure = monthlyExpenditure;
        }
    }
}