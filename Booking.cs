using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class Booking
    {
        private string id;
        private string renterName;
        private DateTime startBookingPeriod;
        private DateTime endBookingPeriod;
        private string ownerName;
        private string paymentMethod;
        private string bookingStatus;
        private float payment;
        private Car chosenCar; // Reference to Car
        private BookingLocation bookingLocations; // Reference to BookingLocation

        // Properties
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string RenterName
        {
            get { return renterName; }
            set { renterName = value; }
        }

        public DateTime StartBookingPeriod
        {
            get { return startBookingPeriod; }
            set { startBookingPeriod = value; }
        }

        public DateTime EndBookingPeriod
        {
            get { return endBookingPeriod; }
            set { endBookingPeriod = value; }
        }

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        public string PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }

        public string BookingStatus
        {
            get { return bookingStatus; }
            set { bookingStatus = value; }
        }

        public float Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public Car ChosenCar
        {
            get { return chosenCar; }
            set { chosenCar = value; }
        }

        public BookingLocation BookingLocations
        {
            get { return bookingLocations; }
            set { bookingLocations = value; }
        }

        // Constructor
        public Booking(string id, string renterName, DateTime startBookingPeriod, DateTime endBookingPeriod, string ownerName, string paymentMethod, string bookingStatus, float payment, Car chosenCar, BookingLocation bookingLocations)
        {
            this.id = id;
            this.renterName = renterName;
            this.startBookingPeriod = startBookingPeriod;
            this.endBookingPeriod = endBookingPeriod;
            this.ownerName = ownerName;
            this.paymentMethod = paymentMethod;
            this.bookingStatus = bookingStatus;
            this.payment = payment;
            this.chosenCar = chosenCar;
            this.bookingLocations = bookingLocations;
        }       
    }
}
