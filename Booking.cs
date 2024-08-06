namespace SWAD_Assignment2_GrpC
{
    public class Booking
    {
        // Properties
        public int Id { get; set; }

        public string RenterName { get;set; }

        public string ListingName { get; set; }

        public DateTime StartBookingPeriod { get;set; }

        public DateTime EndBookingPeriod { get;set; }

        public string OwnerName { get;set; }

        public string PaymentMethod { get;set; }

        public string BookingStatus { get;set; }

        public float Payment { get;set; }

        public Car ChosenCar { get;set; }

        public BookingLocation BookingLocations { get;set; }

        // Constructor
        public Booking(int id, string renterName, string listingName, DateTime startBookingPeriod, DateTime endBookingPeriod, string ownerName, string paymentMethod, string bookingStatus, float payment, Car chosenCar, BookingLocation bookingLocations)
        {
            Id = id;
            RenterName = renterName;
            ListingName = listingName;
            StartBookingPeriod = startBookingPeriod;
            EndBookingPeriod = endBookingPeriod;
            OwnerName = ownerName;
            PaymentMethod = paymentMethod;
            BookingStatus = bookingStatus;
            Payment = payment;
            ChosenCar = chosenCar;
            BookingLocations = bookingLocations;
        }       
    }
}
