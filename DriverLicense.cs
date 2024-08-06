using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class DriverLicense
    {
        private int licenseId;
        private string name;
        private DateTime dateOfBirth;
        private DateTime issueDate;
        private DateTime expirationDate;
        private string licenseType;
        private string address;
        private string status;
        private int demeritPoints;

        // Properties
        public int LicenseId
        {
            get { return licenseId; }
            set { licenseId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public DateTime IssueDate
        {
            get { return issueDate; }
            set { issueDate = value; }
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        public string LicenseType
        {
            get { return licenseType; }
            set { licenseType = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int DemeritPoints
        {
            get { return demeritPoints; }
            set { demeritPoints = value; }
        }

        // Constructor
        public DriverLicense(int licenseId, string name, DateTime dateOfBirth, DateTime issueDate, DateTime expirationDate, string licenseType, string address, string status, int demeritPoints)
        {
            this.licenseId = licenseId;
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.issueDate = issueDate;
            this.expirationDate = expirationDate;
            this.licenseType = licenseType;
            this.address = address;
            this.status = status;
            this.demeritPoints = demeritPoints;
        }
    }
}
