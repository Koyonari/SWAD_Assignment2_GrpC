namespace SWAD_Assignment2_GrpC
{
    public class Booking
    {
        // Properties
        public int Id { get; set; }

        public string RenterName { get; set; }

        public string ListingName { get; set; }

        public DateTime StartBookingPeriod { get; set; }

        public DateTime EndBookingPeriod { get; set; }

        public string OwnerName { get; set; }

        public string PaymentMethod { get; set; }

        public string BookingStatus { get; set; }

        public float Payment { get; set; }

        public Car ChosenCar { get; set; }

        public BookingLocation BookingLocations { get; set; }

        public Booking() { }

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

        // Aaron's Methods
        // -----------------------------------------------------------------------------------------------
        // Method to prompt for start date and time
        public void PromptStartDateTime() // Method by Aaron
        {
            Console.WriteLine("Enter the start date and time of the booking period (yyyy-MM-dd HH:mm):");
        }

        // Method to choose the start date and time
        public void ChooseStartDateTime() // Method by Aaron
        {
            PromptStartDateTime();
            StartBookingPeriod = DateTime.Parse(Console.ReadLine());
        }

        // Method to prompt for end date and time
        public void PromptEndDateTime() // Method by Aaron
        {
            Console.WriteLine("Enter the end date and time of the booking period (yyyy-MM-dd HH:mm):");
        }

        // Method to choose the end date and time
        public void ChooseEndDateTime() // Method by Aaron
        {
            PromptEndDateTime();
            EndBookingPeriod = DateTime.Parse(Console.ReadLine());
        }

        // Method to prompt for car make
        public void PromptCarMake() // Method by Aaron
        {
            Console.WriteLine("Enter the make of the chosen car:");
        }

        // Method to enter the car make
        public void EnterCarMake() // Method by Aaron
        {
            PromptCarMake();
            ChosenCar.Make = Console.ReadLine();
        }

        // Method to prompt for car model
        public void PromptCarModel() // Method by Aaron
        {
            Console.WriteLine("Enter the model of the chosen car:");
        }

        // Method to enter the car model
        public void EnterCarModel() // Method by Aaron
        {
            PromptCarModel();
            ChosenCar.Model = Console.ReadLine();
        }

        // Method to verify car status
        public void VerifyBookingDetails() // Method by Aaron
        {
            while (true)
            {
                bool isValid = true;

                if (StartBookingPeriod == default)
                {
                    ChooseStartDateTime();
                    isValid = false;
                }
                if (EndBookingPeriod == default)
                {
                    ChooseEndDateTime();
                    isValid = false;
                }
                if (string.IsNullOrEmpty(ChosenCar.Make))
                {
                    EnterCarMake();
                    isValid = false;
                }
                if (string.IsNullOrEmpty(ChosenCar.Model))
                {
                    EnterCarModel();
                    isValid = false;
                }
                if (EndBookingPeriod <= StartBookingPeriod)
                {
                    Console.WriteLine("End date and time must be after start date and time. Please re-enter the dates.");
                    ChooseStartDateTime();
                    ChooseEndDateTime();
                    isValid = false;
                }

                if (isValid)
                {
                    break;
                }
            }
        }

        // Method to display pickup locations
        public void DisplayPickupLocations() // Method by Aaron
        {
            Console.WriteLine("Available Pickup Locations:");
            Console.WriteLine("[1] Downtown iCar Station");
            Console.WriteLine("[2] Changi iCar Station");
            Console.WriteLine("[3] Marina Bay iCar Station");
            Console.WriteLine("[4] Orchard iCar Station");
            Console.WriteLine("[5] Sentosa iCar Station");
            Console.WriteLine("[6] East Coast iCar Station");
        }

        // Method to prompt for pickup location
        public void PromptPickupLocation() // Method by Aaron
        {
            Console.WriteLine("Select your pickup location (enter the number): ");
        }

        // Method to enter the pickup location
        public void EnterPickupLocation() // Method by Aaron
        {
            PromptPickupLocation();
            string input = Console.ReadLine();

            // Set the pickup location
            if (input == "1")
            {
                BookingLocations.PickupLocation = "Downtown iCar Station";
            }
            else if (input == "2")
            {
                BookingLocations.PickupLocation = "Changi iCar Station";
            }
            else if (input == "3")
            {
                BookingLocations.PickupLocation = "Marina Bay iCar Station";
            }
            else if (input == "4")
            {
                BookingLocations.PickupLocation = "Orchard iCar Station";
            }
            else if (input == "5")
            {
                BookingLocations.PickupLocation = "Sentosa iCar Station";
            }
            else if (input == "6")
            {
                BookingLocations.PickupLocation = "East Coast iCar Station";
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose a valid pickup location.");
                return;
            }
        }

        public void VerifyPickupLocation() // Method by Aaron
        {
            while (BookingLocations == null || string.IsNullOrEmpty(BookingLocations.PickupLocation))
            {
                DisplayPickupLocations();
                EnterPickupLocation();
            }
        }

        public void Delivery() // Method by Aaron
        {
            Console.WriteLine("You have chosen the Doorstep Delivery option.");
            Console.WriteLine("A small fee will be added based in accordance to the selected location");
        }

        public void PromptDeliveryAddress() // Method by Aaron
        {
            Console.WriteLine("Enter your chosen delivery address: ");
        }

        public void EnterDeliveryAddress() // Method by Aaron
        {
            PromptDeliveryAddress();
            BookingLocations.PickupLocation = Console.ReadLine();
        }

        // Method to update the status of the chosen car
        public void VerifyDeliveryAddress() // Method by Aaron
        {
            while (BookingLocations == null || string.IsNullOrEmpty(BookingLocations.PickupLocation))
            {
                EnterDeliveryAddress();
            }
        }

        // Method to update the status of delivery in Booking
        public void UpdateDeliveryStatus() // Method by Aaron
        {
            BookingLocations.Delivery = true;
            Console.WriteLine("Your Delivery has successfully been recorded. ");
        }

        // Method to display reservation details
        public void DisplayReservationDetails() // Method by Aaron
        {
            Console.WriteLine("Reservation Details:");
            Console.WriteLine($"Car Make: {ChosenCar.Make}");
            Console.WriteLine($"Car Model: {ChosenCar.Model}");
            Console.WriteLine($"Car Year: {ChosenCar.Year}");
            Console.WriteLine($"Rental Rate: ${ChosenCar.RentalRate:F2} per day");
            Console.WriteLine($"Start Date: {StartBookingPeriod.ToString("yyyy-MM-dd HH:mm")}");
            Console.WriteLine($"End Date: {EndBookingPeriod.ToString("yyyy-MM-dd HH:mm")}");
            Console.WriteLine($"Pickup Location: {BookingLocations.PickupLocation}");

            Console.WriteLine("--------------------------------------------");
        }

        // Method to prompt for car model
        public void PromptReservationConfirmation() // Method by Aaron
        {
            Console.WriteLine("Confirm All Reservation Details [Y/N]: ");
        }

        // Method to enter the car model
        public void ConfirmReservation() // Method by Aaron
        {
            PromptReservationConfirmation();
            string confirmation = Console.ReadLine();
            if (confirmation.ToUpper() == "Y")
            {
                updateCarStatus();
                updateBookingStatus();
                Console.WriteLine("Reservation confirmed. Thank you for using our services!");
                Console.WriteLine("Returning to Main Menu");
                return;
            }
            else
            {
                Console.WriteLine("Reservation not confirmed.");
            }
        }

        // Method to update the status of the chosen car
        private void updateCarStatus() // Method by Aaron
        {
            ChosenCar.Status = "Reserved";
            Console.WriteLine($"Car status updated to 'Reserved' under your booking.");
        }

        // Method to update the status of the booking
        private void updateBookingStatus() // Method by Aaron
        {
            BookingStatus = "Confirmed";
            Console.WriteLine($"Booking status updated to 'Booked'.");
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
        // -----------------------------------------------------------------------------------------------
        // End of Yong Shyan's methods
    }
}