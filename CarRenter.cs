using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class CarRenter
    {
        private bool primeStatus;
        private bool eligibility;
        private DateTime dateOfBirth;
        private DriverLicense driversLicense; // Reference to DriverLicense
        private float monthlyExpenditure;

        // Properties
        public bool PrimeStatus
        {
            get { return primeStatus; }
            set { primeStatus = value; }
        }

        public bool Eligibility
        {
            get { return eligibility; }
            set { eligibility = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public DriverLicense DriversLicense
        {
            get { return driversLicense; }
            set { driversLicense = value; }
        }

        public float MonthlyExpenditure
        {
            get { return monthlyExpenditure; }
            set { monthlyExpenditure = value; }
        }

        // Constructor
        public CarRenter(bool primeStatus, bool eligibility, DateTime dateOfBirth, DriverLicense driversLicense, float monthlyExpenditure)
        {
            this.primeStatus = primeStatus;
            this.eligibility = eligibility;
            this.dateOfBirth = dateOfBirth;
            this.driversLicense = driversLicense;
            this.monthlyExpenditure = monthlyExpenditure;
        }
    }
}