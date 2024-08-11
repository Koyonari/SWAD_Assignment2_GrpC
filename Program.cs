using Spectre.Console;
using SWAD_Assignment2_GrpC;
using System.Globalization;

class Program
{
    private static int nextBookingId = 1;

    // Directly store car objects
    static List<Car> carList = new List<Car>
    {
        new Car(1, "Model S", "Tesla", 2022, "Available", 12000, "Luxury Electric", "ABC1234", true, "Fast and efficient electric car", 150.00f),
        new Car(2, "Civic", "Honda", 2020, "Reserved", 30000, "Reliable Sedan", "XYZ5678", true, "Reliable and fuel-efficient", 80.00f),
        new Car(3, "Mustang", "Ford", 2021, "Unavailable", 5000, "Sporty Coupe", "LMN9101", true, "Powerful and stylish", 130.00f),
        new Car(4, "Corolla", "Toyota", 2019, "Available", 45000, "Compact Sedan", "DEF2345", true, "Affordable and reliable", 70.00f),
        new Car(5, "Model X", "Tesla", 2023, "Available", 8000, "Luxury SUV", "GHI6789", true, "Spacious and high-tech", 200.00f)
    };

    // Store Booking objects
    static List<Booking> bookings = new List<Booking>();

    static void Main(string[] args)
    {
        string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
        File.WriteAllText(filePath, string.Empty);
        DisplayWelcomeMessage();

        while (true)
        {
            int menu_opt = DisplayMenu();
            switch (menu_opt)
            {
                case 1:
                    Console.WriteLine("[1] Register Vehicle");
                    break;
                case 2:
                    Console.WriteLine("[2] Make Payment\n");
                    MakePayment();
                    break;
                case 3:
                    Console.WriteLine("[3] Reserve Vehicle");
                    ReserveVehicle();
                    break;
                case 4:
                    Console.WriteLine("[4] Register Renter");
                    // Add renter registration logic here
                    break;
                case 5:
                    Console.WriteLine("[5] Review Appeal");
                    DisplayPenaltyAppeals();
                    break;
                case 6:
                    Console.WriteLine("Exited...");
                    return; // Exits the loop and program
                default:
                    Console.WriteLine("Invalid option. Please select a valid menu item.");
                    break;
            }
        }
    }

    static void DisplayWelcomeMessage()
    {
        AnsiConsole.Write(
            new FigletText("Welcome to")
                .LeftJustified());
        AnsiConsole.Write(
            new FigletText("ICar")
                .Centered()
                .Color(Color.Aqua));
        AnsiConsole.Write(
            new FigletText("----------")
                .LeftJustified()
                .Color(Color.Teal));
    }

    static int DisplayMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nHello, what are we going to be [green]doing here[/] today?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                    "[[1]] Register Vehicle",
                    "[[2]] Make Payment",
                    "[[3]] Reserve Vehicle",
                    "[[4]] Register Renter",
                    "[[5]] Review Appeal",
                    "[[6]] Exit iCar"
                }));
        return choice[2] - '0';
    }

    static void DisplayCarList(List<Car> carList)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Make");
        table.AddColumn("Model");
        table.AddColumn("Year");
        table.AddColumn("Status");
        table.AddColumn("Mileage");
        table.AddColumn("Listing Name");
        table.AddColumn("License Plate");
        table.AddColumn("Insurance Status");
        table.AddColumn("Description");
        table.AddColumn("Rental Rate");

        foreach (var car in carList)
        {
            table.AddRow(
                car.Id.ToString(),
                car.Make,
                car.Model,
                car.Year.ToString(),
                car.Status,
                car.Mileage.ToString(),
                car.ListingName,
                car.LicensePlateNumber,
                car.InsuranceStatus ? "Valid" : "Invalid",
                car.Description,
                $"{car.RentalRate:F2}"
            );
        }

        AnsiConsole.Write(table);
    }

    // Yong Shyan's methods
    // -----------------------------------------------------------------------------------------------
    static void MakePayment()
    {
        if (bookings.Count > 0)
        {
            DisplayBookings();

            //Prompt user to choose which Booking to make payment
            Console.WriteLine("Select which booking to make payment");
            int opt = PayBooking();
            Console.WriteLine("You have selected option " + opt);
            Booking selectedBooking = bookings[opt-1];

            // Check for Additional Fee
            float additionalFee = selectedBooking.BookingLocations.CheckForAdditionalFee();

            // Calculate Current Payment
            float currentPayment = CalculateCurrentPayment(selectedBooking, additionalFee);
            Console.WriteLine("Your total current payment is S$" + currentPayment);

            bool paymentComplete = false;
            if (currentPayment > 0)
            {
                int paymentOpt = PaymentMethod();
                if (paymentOpt == 1)
                {
                    if (CreditCardPayment() == true)
                    {
                        paymentComplete = true;
                    };
                }
                else if (paymentOpt == 2)
                {
                    if (DigitalWalletPayment() == true)
                    {
                        paymentComplete= true;
                    };
                }
                else
                {
                    InvalidPayment();
                }
            }

            if (paymentComplete == true)
            {
                Console.WriteLine("Booking made successfully.");
                selectedBooking.EmailReceipt(paymentComplete, currentPayment);
            }
        }
        else
        {
            NoBookings();
        }
 
    }

    static void InvalidPayment()
    {
        Console.WriteLine("Invalid payment method.");
    }

    static void NoBookings()
    {
        Console.WriteLine("You have 0 current bookings");
    }

    static float CalculateCurrentPayment(Booking selectedBooking, float additionalFee)
    {
        float hrs = selectedBooking.GetRentedHrs();
        float dailyRate = selectedBooking.ChosenCar.GetRentalRate();
        float rate = dailyRate / 24;
        float currentPayment = additionalFee + hrs * rate;

        return currentPayment;
    }

    static void DisplayBookings()
    {
        var table = new Table();

        // Add columns
        table.AddColumn("No.");
        table.AddColumn("Start Booking Period");
        table.AddColumn("End Booking Period");
        table.AddColumn("Car Make");
        table.AddColumn("Car Model");
        table.AddColumn("Pickup Location");
        table.AddColumn("Additional Payment");

        // Read file
        string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
        string[] lines = File.ReadAllLines(filePath);
        int bookingNumber = 1;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("Start Booking Period:"))
            {
                string startBookingPeriod = lines[i].Split(":", 2)[1].Trim();
                string endBookingPeriod = lines[i + 1].Split(":", 2)[1].Trim();
                string carMake = lines[i + 2].Split(":", 2)[1].Trim();
                string carModel = lines[i + 3].Split(":", 2)[1].Trim();
                string pickupLocation = lines[i + 4].Split(":", 2)[1].Trim();
                string additionalPayment = lines[i + 5].Split(":", 2)[1].Trim();

                // Add a rows
                table.AddRow(
                    bookingNumber.ToString(),
                    startBookingPeriod,
                    endBookingPeriod,
                    carMake,
                    carModel,
                    pickupLocation,
                    additionalPayment
                );

                bookingNumber++;
                i += 6; // Skip past the processed booking details
            }
        }

        table.Title = new TableTitle("Booking Details");
        table.Border(TableBorder.Rounded);
        table.Centered();

        // Display the table
        AnsiConsole.Write(table);
    }

    static int PayBooking()
    {
        // Read file
        string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
        string[] lines = File.ReadAllLines(filePath);

        // Create a list to hold the booking options
        var bookingOptions = new List<string>();

        // Count the number of booking entries and generate options
        int bookingNumber = 1;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("Start Booking Period:"))
            {
                bookingOptions.Add($"{bookingNumber}");
                bookingNumber++;
                i += 6; // Skip past the processed booking details
            }
        }

        // Booking selection menu
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select a booking to [green]make payment[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(bookingOptions));

        // Convert the selected choice to an integer and return
        return int.Parse(choice);
    }

    // Menu to choose payment method
    static int PaymentMethod()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nChoose your [green]payment method[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(new[] {
                    "[[1]] Credit Card",
                    "[[2]] Digital Wallet"
                }));
        return choice[2] - '0';
    }

    static bool CreditCardPayment()
    {
        Console.WriteLine("--- Payment by Credit Card ---\n");

        // CC Number
        long ccn = CreditCard.promptCreditCardNumber();

        // CC Expiry Date
        DateTime exp = CreditCard.promptCreditCardExpiry();

        // CVV Number
        int cvv = CreditCard.promptCreditCardCVV();

        // CC Name
        string cchn = CreditCard.promptCreditCardName();

        // Make CreditCard object
        CreditCard creditCardPayment = new CreditCard(ccn, exp, cvv, cchn);

        // Verify the credit card details
        bool verify = creditCardPayment.verifyCreditCardDetails(creditCardPayment);

        // Payment confirmation message
        return isSuccessfulCreditCard(verify);
    }

    static bool DigitalWalletPayment()
    {
        Console.WriteLine("--- Payment by Digital Wallet ---\n");

        // Wallet Type
        string walletType = DigitalWallet.promptWalletType();

        // Wallet Name
        string walletName = DigitalWallet.promptWalletName();

        // Wallet Username
        string walletUsername = DigitalWallet.promptWalletUsername(walletType);

        // Wallet password
        string walletPassword = DigitalWallet.promptWalletPassword();

        // Make DigitalWallet object
        DigitalWallet digitalWalletPayment = new DigitalWallet(walletType, walletName, walletUsername, walletPassword);

        // Verify the digital wallet details
        bool verify = digitalWalletPayment.verifyDigitalPaymentDetails(digitalWalletPayment);

        // Payment confirmation message
        return isSuccessfulDigitalWallet(verify);
    }

    static bool isSuccessfulDigitalWallet(bool verify)
    {
        if (verify == true)
        {
            string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
            File.WriteAllText(filePath, string.Empty);
            Console.WriteLine("\nDigital Wallet details are correct! You have " + CountBookings(filePath) + " oustanding payments.");
        }
        else
        {
            Console.WriteLine("Incorrect digital wallet details. Try again.");
        }
        return verify;
    }

    static bool isSuccessfulCreditCard(bool verify)
    {
        if (verify == true)
        {
            string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
            File.WriteAllText(filePath, string.Empty);
            Console.WriteLine("\nCredit Card details are correct! You have " + CountBookings(filePath) + " oustanding payments.");
        }
        else
        {
            Console.WriteLine("Incorrect credit card details. Try again.");
        }
        return verify;
    }

    static int CountBookings(string filePath)
    {
        // Initialize booking count
        int bookingCount = 0;

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Count occurrences of "Booking Details:"
        foreach (string line in lines)
        {
            if (line.Trim() == "Booking Details:")
            {
                bookingCount++;
            }
        }

        return bookingCount;
    }
    // -----------------------------------------------------------------------------------------------
    // End of Yong Shyan's methods

    // Aaron's methods
    // -----------------------------------------------------------------------------------------------
    static void ReserveVehicle()
    {
        CarRenter currentRenter = new CarRenter(
                id: 1,
                address: "123 Main Street, Cityville, Country",
                email: "john.doe@example.com",
                username: "JohnDoe92",
                contactNumber: 1234567890,
                name: "John Doe",
                dateJoined: new DateTime(2021, 6, 15)
            )
        {
            PrimeStatus = true,
            Eligibility = true,
            DriverLicense = true,
            MonthlyExpenditure = 500.75f
        };

        while (true)
        {
            Console.Clear(); // Clear the console for fresh display
            DisplayCarList(carList);

            var carIds = new List<string>();
            foreach (var car in carList)
            {
                carIds.Add($"ID: {car.Id}, {car.Make} {car.Model} ({car.Year}) - {car.Status}");
            }

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which car would you like to reserve?")
                    .PageSize(10)
                    .AddChoices(carIds));

            var selectedCarId = int.Parse(choice.Split(',')[0].Split(':')[1].Trim());
            var selectedCar = carList.Find(car => car.Id == selectedCarId);

            if (selectedCar == null)
            {
                Console.WriteLine("Invalid car selection. Please try again.");
                continue; // Repeat the loop if the car is not found
            }

            if (selectedCar.VerifyCarStatus())
            {
                Console.Clear(); // Clear the console for the selected car's details
                Console.WriteLine("Car details:");
                Console.WriteLine($"ID: {selectedCar.Id}");
                Console.WriteLine($"Make: {selectedCar.Make}");
                Console.WriteLine($"Model: {selectedCar.Model}");
                Console.WriteLine($"Year: {selectedCar.Year}");
                Console.WriteLine($"Mileage: {selectedCar.Mileage}");
                Console.WriteLine($"Listing Name: {selectedCar.ListingName}");
                Console.WriteLine($"License Plate Number: {selectedCar.LicensePlateNumber}");
                Console.WriteLine($"Insurance Status: {(selectedCar.InsuranceStatus ? "Valid" : "Invalid")}");
                Console.WriteLine($"Description: {selectedCar.Description}");
                Console.WriteLine($"Rental Rate: ${selectedCar.RentalRate:F2} per day");
                Console.WriteLine("--------------------------------------------");

                // Create a new booking instance and set its properties
                CreateReservation(selectedCar, currentRenter);
                break; // Exit the loop after successful reservation
            }
            else
            {
                DisplayReservationUnavailable();
            }
        }
    }

    static void CreateReservation(Car selectedCar, CarRenter renter)
    {
        bool isValidBooking = false;

        while (!isValidBooking)
        {
            // Create a new booking instance
            Booking booking = new Booking
            {
                Id = nextBookingId++
            };

            // Use the AddBookingDetails method to set the booking details
            booking.AddCar(selectedCar);

            // Prompt user for booking details
            DateTime startBookingPeriod = ChooseStartDateTime();
            DateTime endBookingPeriod = ChooseEndDateTime();

            // Verify booking date and times
            List<string> datetimeErrors = VerifyBookingDateTimes(startBookingPeriod, endBookingPeriod);
            if (datetimeErrors.Count > 0)
            {
                foreach (string error in datetimeErrors)
                {
                    Console.WriteLine(error);
                }
                Console.WriteLine("Please correct the date and time errors above and try again.");
                return; // Exit or re-prompt the user
            }

            string carMake;
            string carModel;

            // Prompt and enter car make and model
            carMake = EnterCarMake();
            carModel = EnterCarModel();

            // Verify car make and model
            List<string> carErrors = booking.ChosenCar.VerifyCarMakeModel(carMake, carModel);
            if (carErrors.Count > 0)
            {
                foreach (string error in carErrors)
                {
                    Console.WriteLine(error);
                }
                Console.WriteLine("Please correct the car details errors above and try again.");
                return; // Exit or re-prompt the user
            }

            // Use the AddBookingDetails method to set the booking details
            booking.AddBookingDetails(startBookingPeriod, endBookingPeriod);

            // Choose pickup or delivery, and enter pickup location (for both iCar Station and Delivery)
            SelectPickupOption(booking);

            // Display reservation details and confirm reservation
            DisplayReservationDetails(booking, selectedCar);
            PromptReservationConfirmation();
            ConfirmReservation(booking);

            renter.AddBooking(booking);

            booking.SaveBookingDetailsToFile();
            bookings.Add(booking);

            // If all is well, exit the loop
            isValidBooking = true;
        }
    }

    // Method to display reservation unavailable
    static void DisplayReservationUnavailable()
    {
        AnsiConsole.MarkupLine("[red]The selected car is currently unavailable or reserved. Please choose another car.[/]");
        AnsiConsole.MarkupLine("[yellow]Press any key to try again...[/]");
        Console.ReadKey();
    }

    // Method to prompt for start date and time
    static void PromptStartDateTime()
    {
        Console.WriteLine("Enter the start date and time of the booking period (yyyy-MM-dd HH:mm): ");
    }

    // Method to choose the start date and time
    static DateTime ChooseStartDateTime()
    {
        PromptStartDateTime();
        return DateTime.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
    }

    // Method to prompt for end date and time
    static void PromptEndDateTime()
    {
        Console.WriteLine("Enter the end date and time of the booking period (yyyy-MM-dd HH:mm): ");
    }

    // Method to choose the end date and time
    static DateTime ChooseEndDateTime()
    {
        PromptEndDateTime();
        return DateTime.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
    }

    static List<string> VerifyBookingDateTimes(DateTime startBookingPeriod, DateTime endBookingPeriod)
    {
        List<string> errorMessages = new List<string>();

        if (startBookingPeriod == default)
        {
            errorMessages.Add("Start booking period is not set.");
        }
        if (endBookingPeriod == default)
        {
            errorMessages.Add("End booking period is not set.");
        }
        if (endBookingPeriod <= startBookingPeriod)
        {
            errorMessages.Add("End booking period must be after the start booking period.");
        }

        return errorMessages;
    }

    // Method to prompt for car make
    static void PromptCarMake()
    {
        Console.WriteLine("Enter the make of the chosen car: ");
    }

    // Method to enter the car make
    static string EnterCarMake()
    {
        PromptCarMake();
        return Console.ReadLine();
    }

    // Method to prompt for car make
    static void PromptCarModel()
    {
        Console.WriteLine("Enter the model of the chosen car: ");
    }

    // Method to enter the car make
    static string EnterCarModel()
    {
        PromptCarModel();
        return Console.ReadLine();
    }

    static string PromptPickupOption()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select your mode of Car Pickup:")
                .AddChoices(new[] { "iCar Station Physical Pickup", "Doorstep Delivery" })
        );
    }

    static void SelectPickupOption(Booking booking)
    {
        // Create an instance of BookingLocation
        BookingLocation bookingLocation = new BookingLocation();

        // Get user's choice of pickup or delivery
        var pickupOrDelivery = PromptPickupOption();

        if (pickupOrDelivery == "iCar Station Physical Pickup")
        {
            bool delivery = false;
            float additionalPayment = 0;
            DisplayPickupLocations();

            string pickupLocation;
            do
            {
                pickupLocation = EnterPickupLocation();
                if (!VerifyPickupLocation(pickupLocation))
                {
                    DisplayReservationDetailsIncorrect();
                }
            } while (!VerifyPickupLocation(pickupLocation));

            bookingLocation.AddPickupLocationDetails(pickupLocation, delivery, additionalPayment);
        }
        else if (pickupOrDelivery == "Doorstep Delivery")
        {
            string deliveryAddress;
            float deliveryFee = 0;
            bool delivery = true;
            do
            {
                Delivery();
                deliveryAddress = EnterDeliveryAddress();
                if (!VerifyDeliveryAddress(deliveryAddress))
                {
                    Console.WriteLine("Invalid delivery address. Please enter a valid address between 5 and 100 characters.");
                }
            } while (!VerifyDeliveryAddress(deliveryAddress));

            deliveryFee += 20;
            bookingLocation.AddPickupLocationDetails(deliveryAddress, delivery, deliveryFee);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please select a valid option.");
        }

        booking.AddBookingLocationDetails(bookingLocation);
    }

    static void DisplayPickupLocations()
    {
        Console.WriteLine("Available pickup locations:");
        // List of sample pickup locations
        string[] locations = { "Downtown iCar Station", "Changi iCar Station", "Marina Bay iCar Station", "Orchard iCar Station", "Sentosa iCar Station", "East Coast iCar Station" };
        foreach (var location in locations)
        {
            Console.WriteLine(location);
        }
    }

    static void PromptPickupLocation()
    {
        Console.WriteLine("Enter your pickup location from the list above: ");
    }

    static string EnterPickupLocation()
    {
        PromptPickupLocation();
        return Console.ReadLine();
    }

    // Method to verify pickup location
    static bool VerifyPickupLocation(string pickupLocation)
    {
        // List of valid pickup locations
        string[] validLocations = {
                "Downtown iCar Station",
                "Changi iCar Station",
                "Marina Bay iCar Station",
                "Orchard iCar Station",
                "Sentosa iCar Station",
                "East Coast iCar Station"
            };

        // Check if the entered pickup location is valid
        return Array.Exists(validLocations, location => location.Equals(pickupLocation, StringComparison.OrdinalIgnoreCase));
    }

    static void Delivery()
    {
        Console.WriteLine("You have chosen the Doorstep Delivery option.");
        Console.WriteLine("A small fee will be added based in accordance to the selected location");
    }

    static void PromptDeliveryAddress()
    {
        Console.WriteLine("Enter your delivery address: ");
    }

    static string EnterDeliveryAddress()
    {
        PromptDeliveryAddress();
        return Console.ReadLine();
    }

    static bool VerifyDeliveryAddress(string deliveryAddress)
    {
        const int MinLength = 5;
        const int MaxLength = 100;

        return !string.IsNullOrEmpty(deliveryAddress) && deliveryAddress.Length >= MinLength && deliveryAddress.Length <= MaxLength;
    }

    // Method to display reservation details
    static void DisplayReservationDetails(Booking Booking, Car ChosenCar)
    {
        Console.WriteLine("Reservation Details:");
        Console.WriteLine($"Booking Id: {Booking.Id}");
        Console.WriteLine($"Car Make: {ChosenCar.Make}");
        Console.WriteLine($"Car Model: {ChosenCar.Model}");
        Console.WriteLine($"Car Year: {ChosenCar.Year}");
        Console.WriteLine($"Rental Rate: ${ChosenCar.RentalRate:F2} per day");
        Console.WriteLine($"Start Date: {Booking.StartBookingPeriod:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"End Date: {Booking.EndBookingPeriod:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Pickup Location: {Booking.BookingLocations.PickupLocation}");
        Console.WriteLine($"Additional Payments: ${Booking.BookingLocations.AdditionalPayment:F2}");
        Console.WriteLine("--------------------------------------------");
    }

    static void DisplayReservationDetailsIncorrect()
    {
        Console.WriteLine("There was a problem upon verifying the reservation details due to incorrect detail or detail format.");
        Console.WriteLine("Press any key to try again...");
        Console.ReadKey();
    }

    static void PromptReservationConfirmation()
    {
        Console.WriteLine("Confirm All Reservation Details [Y/N]: ");
    }

    // Method to confirm reservation
    static void ConfirmReservation(Booking Booking)
    {
        string confirmation = Console.ReadLine();
        if (confirmation.ToUpper() == "Y")
        {
            Booking.ChosenCar.UpdateCarStatus();
            Booking.UpdateBookingStatus();
            DisplayPendingPayment();
        }
        else
        {
            Console.WriteLine("Reservation not confirmed.");
        }
    }

    static void DisplayPendingPayment()
    {
        Console.WriteLine("All Reservation details have been confirmed.");
        Console.WriteLine("Press any key to move on to make payment.");
        Console.ReadKey();
    }
    // -----------------------------------------------------------------------------------------------
    // End of Aaron's methods

    // Casey's methods
    // -----------------------------------------------------------------------------------------------
    static bool DisplayPenaltyAppeals()
    {
        Admin admin = new Admin(1);
        List<AppealPenalty> appeals = new List<AppealPenalty>();

        CarRenter renter1 = new CarRenter(1, "123 Main St", "mickey@gmail.com", "mickey123", 12345678, "Mickey Banana", DateTime.Now);
        CarRenter renter2 = new CarRenter(2, "456 Elm St", "john@gmail.com", "john123", 87654321, "Long John", DateTime.Now);

        CarOwner owner1 = new CarOwner(3, "789 Oak St", "peter@gmail.com", "peter123", 90123456, "Peter Rock", DateTime.Now);
        CarOwner owner2 = new CarOwner(4, "101 Pine St", "jane@gmail.com", "jane123", 78901234, "Jane Mary", DateTime.Now);

        Penalty penalty1 = new Penalty(1, 50.0f, "Late return", "Vehicle returned 2 hours late", DateTime.Now.AddDays(30), null);
        Penalty penalty2 = new Penalty(2, 100.0f, "Damage to car", "Scratch on left door", DateTime.Now.AddDays(30), null);
        Penalty penalty3 = new Penalty(3, 75.0f, "Excessive mileage", "Mileage exceeded limit", DateTime.Now.AddDays(30), null);
        Penalty penalty4 = new Penalty(4, 200.0f, "Late payment", "Payment overdue by 2 weeks", DateTime.Now.AddDays(30), null);

        appeals.Add(new AppealPenalty(1, "Late return", renter1, penalty1, DateTime.Now, "Available", "Traffic jam"));
        appeals.Add(new AppealPenalty(2, "Damage to car", renter2, penalty2, DateTime.Now, "Available", "Pre-existing damage"));
        appeals.Add(new AppealPenalty(3, "Excessive mileage", owner1, penalty3, DateTime.Now, "Available", "Unforeseen circumstances"));
        appeals.Add(new AppealPenalty(4, "Late payment", owner2, penalty4, DateTime.Now, "Available", "Financial difficulties"));

        // Display Appeal List
        admin.DisplayAppealsList(appeals);

        // Prompt appeal
        AppealPenalty selectedAppeal = admin.SelectAppeal(appeals);

        Console.WriteLine();
        selectedAppeal.DisplayInformation();
        Console.WriteLine();

        if (selectedAppeal.Appellant is CarRenter)
        {
            ProcessRenterAppeal(admin, selectedAppeal, appeals);
        }
        else if (selectedAppeal.Appellant is CarOwner)
        {
            ProcessCarOwnerAppeal(admin, selectedAppeal, appeals);
        }

        return true; // Return true to indicate successful execution
    }

    static void ProcessRenterAppeal(Admin admin, AppealPenalty selectedAppeal, List<AppealPenalty> appeals)
    {
        CarRenter renter = (CarRenter)selectedAppeal.Appellant;
        renter.DisplayInformation();

        Console.WriteLine("\nSystem prompts verification");
        bool isInfoConfirmed = admin.ConfirmRenterInformation(renter);

        if (isInfoConfirmed)
        {
            Console.WriteLine();
            renter.DisplayAppealHistory();
        }
        else
        {
            UpdateRenterInformation(renter);
        }

        ProcessAppealDecision(admin, selectedAppeal, appeals);
    }

    static void ProcessCarOwnerAppeal(Admin admin, AppealPenalty selectedAppeal, List<AppealPenalty> appeals)
    {
        CarOwner owner = (CarOwner)selectedAppeal.Appellant;
        owner.DisplayInformation();

        Console.WriteLine("\nSystem prompts verification");
        bool isInfoConfirmed = admin.ConfirmCarOwnerInformation(owner);

        if (isInfoConfirmed)
        {
            Console.WriteLine();
            owner.DisplayAppealHistory();
        }
        else
        {
            UpdateCarOwnerInformation(owner);
        }

        ProcessAppealDecision(admin, selectedAppeal, appeals);
    }

    static void ProcessAppealDecision(Admin admin, AppealPenalty selectedAppeal, List<AppealPenalty> appeals)
    {
        bool isAccepted = admin.DecideOnAppeal();

        if (isAccepted)
        {
            Console.WriteLine("\nSystem prompts for confirmation");
            bool isConfirmed = admin.ConfirmDecision();

            Console.WriteLine();
            if (isConfirmed)
            {
                Console.WriteLine("Appeal Accepted");
                selectedAppeal.UpdateStatus("Accepted");
                admin.SendAppealStatusEmail(selectedAppeal.Appellant, "Accepted");
                appeals.Remove(selectedAppeal);
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Appeal Rejected");
            selectedAppeal.UpdateStatus("Rejected");
            admin.SendAppealStatusEmail(selectedAppeal.Appellant, "Rejected");
        }
    }

    static void UpdateRenterInformation(CarRenter renter)
    {
        Console.WriteLine("Renter information:");
        Console.WriteLine($"Name: {renter.Name}");
        Console.WriteLine($"Email: {renter.Email}");
        Console.WriteLine($"Prime Status: {renter.PrimeStatus}");
        Console.WriteLine($"Eligibility: {renter.Eligibility}");

        Console.WriteLine();
        Console.Write("Enter updated name: ");
        string updatedName = Console.ReadLine();
        Console.Write("Enter updated email: ");
        string updatedEmail = Console.ReadLine();
        Console.Write("Enter updated prime status (y/n): ");
        bool updatedPrimeStatus = Console.ReadLine().ToLower() == "y";
        Console.Write("Enter updated eligibility (y/n): ");
        bool updatedEligibility = Console.ReadLine().ToLower() == "y";

        renter.Name = updatedName;
        renter.Email = updatedEmail;
        renter.PrimeStatus = updatedPrimeStatus;
        renter.Eligibility = updatedEligibility;

        Console.WriteLine();
        Console.WriteLine("-------- Updated renter information ----------");
        renter.DisplayInformation();
        Console.WriteLine();
        renter.DisplayAppealHistory();
    }

    static void UpdateCarOwnerInformation(CarOwner owner)
    {
        Console.WriteLine("Car Owner information:");
        Console.WriteLine($"Name: {owner.Name}");
        Console.WriteLine($"Email: {owner.Email}");
        Console.WriteLine($"Number of Listings: {owner.Listings.Count}");

        Console.WriteLine();
        Console.Write("Enter updated name: ");
        string updatedName = Console.ReadLine();
        Console.Write("Enter updated email: ");
        string updatedEmail = Console.ReadLine();

        owner.Name = updatedName;
        owner.Email = updatedEmail;

        Console.WriteLine();
        Console.WriteLine("-------- Updated car owner information ----------");
        owner.DisplayInformation();
        Console.WriteLine();
        owner.DisplayAppealHistory();
    }
    // -----------------------------------------------------------------------------------------------
    // End of Casey's methods
}