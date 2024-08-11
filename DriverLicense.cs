namespace SWAD_Assignment2_GrpC
{
    internal class DriverLicense
    {
        // Properties
        public int LicenseId { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string LicenseType { get; set; }

        public string Status { get; set; }

        public int DemeritPoints { get; set; }

        // Constructor
        public DriverLicense(int licenseId, string name, DateTime dateOfBirth, DateTime issueDate, DateTime expirationDate, string licenseType, string status, int demeritPoints)
        {
            LicenseId = licenseId;
            Name = name;
            DateOfBirth = dateOfBirth;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            LicenseType = licenseType;
            Status = status;
            DemeritPoints = demeritPoints;
        }
    }
}
