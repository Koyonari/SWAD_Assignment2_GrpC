namespace SWAD_Assignment2_GrpC
{
    public class Booking
    {
        // Properties
        public int Id { get; set; }

        public DateTime StartBookingPeriod { get; set; }

        public DateTime EndBookingPeriod { get; set; }

        public string PaymentMethod { get; set; }

        public string BookingStatus { get; set; }

        public float Payment { get; set; }

        public Car ChosenCar { get; set; }

        public BookingLocation BookingLocations { get; set; }

        public Booking() { }

        // Constructor
        public Booking(int id, DateTime startBookingPeriod, DateTime endBookingPeriod, string paymentMethod, string bookingStatus, float payment, Car chosenCar, BookingLocation bookingLocations)
        {
            Id = id;
            StartBookingPeriod = startBookingPeriod;
            EndBookingPeriod = endBookingPeriod;
            PaymentMethod = paymentMethod;
            BookingStatus = bookingStatus;
            Payment = payment;
            ChosenCar = chosenCar;
            BookingLocations = bookingLocations;
        }

        // Aaron's Methods
        // -----------------------------------------------------------------------------------------------
        // Method to verify the start and end booking times
        public void AddCar(Car car)
        {
            this.ChosenCar = car;
        }

        // Method to add the start and end booking times to booking
        public void AddBookingDetails(DateTime startPeriod, DateTime endPeriod)
        {
            this.StartBookingPeriod = startPeriod;
            this.EndBookingPeriod = endPeriod;
        }

        // Method to add the pickup location details to booking
        public void AddBookingLocationDetails(BookingLocation chosenBookingLocation)
        {
            this.BookingLocations = chosenBookingLocation;
        }

        // Method to update the status of the booking
        public void UpdateBookingStatus()
        {
            BookingStatus = "Pending Payment";
        }
        // -----------------------------------------------------------------------------------------------
        // End of Aaron's methods

        // Start of Yong Shyan's methods
        // -----------------------------------------------------------------------------------------------
        public float GetRentedHrs()
        {
            TimeSpan difference = EndBookingPeriod - StartBookingPeriod;
            double hrs = difference.TotalHours;
            float diff_hrs = (float) hrs;
            return diff_hrs;
        }

        public void EmailReceipt(bool paymentStatus, float paymentAmount)
        {
            if (paymentStatus == true)
            {
                Console.WriteLine("\n------ Email Receipt ------\nBooking made successfully! Payment of $" + paymentAmount + " has been successful.");
            }
        }
        // -----------------------------------------------------------------------------------------------
        // End of Yong Shyan's methods

        public void SaveBookingDetailsToFile()
        {
            string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";

            // Combine the base directory with the relative path to form the full path
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Booking Details:");
                writer.WriteLine($"Start Booking Period: {StartBookingPeriod.ToString("yyyy-MM-dd HH:mm")}");
                writer.WriteLine($"End Booking Period: {EndBookingPeriod.ToString("yyyy-MM-dd HH:mm")}");
                writer.WriteLine($"Car Make: {ChosenCar.Make}");
                writer.WriteLine($"Car Model: {ChosenCar.Model}");
                writer.WriteLine($"Pickup Location: {BookingLocations.PickupLocation}");
                writer.WriteLine($"Additional Payment: ${BookingLocations.AdditionalPayment}");
                writer.WriteLine("--------------------------------------------");
            }

            Console.WriteLine("Booking details saved to BookingDetails.txt.");
        }
    }
}