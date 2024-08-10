using Spectre.Console;
using SWAD_Assignment2_GrpC;

class Program
{
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
        foreach (var car in carList)
        {
            Console.WriteLine($"ID: {car.Id}");
            Console.WriteLine($"Make: {car.Make}");
            Console.WriteLine($"Model: {car.Model}");
            Console.WriteLine($"Year: {car.Year}");
            Console.WriteLine($"Status: {car.Status}");
            Console.WriteLine($"Mileage: {car.Mileage}");
            Console.WriteLine($"Listing Name: {car.ListingName}");
            Console.WriteLine($"License Plate Number: {car.LicensePlateNumber}");
            Console.WriteLine($"Insurance Status: {(car.InsuranceStatus ? "Valid" : "Invalid")}");
            Console.WriteLine($"Description: {car.Description}");
            Console.WriteLine($"Rental Rate: ${car.RentalRate:F2} per day");
            Console.WriteLine("--------------------------------------------");
        }
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
            float additionalFee = selectedBooking.BookingLocations.CheckForAdditionalFee();
            float hrs = selectedBooking.GetRentedHrs();
            float rate = selectedBooking.ChosenCar.GetRentalRate() / 24;
            float currentPayment = additionalFee + hrs * rate;
            Console.WriteLine("Your total current payment is S$" + currentPayment);

            if (currentPayment > 0)
            {
                int paymentOpt = PaymentMethod();
                if (paymentOpt == 1)
                {
                    CreditCardPayment();
                }
                else if (paymentOpt == 2)
                {
                    DigitalWalletPayment();
                }
                else
                {
                    Console.WriteLine("Invalid payment method.");
                }
            }
        }
        else
        {
            Console.WriteLine("You have 0 current bookings.");
        }
 
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
        Console.Write("16-digit Credit Card Number: ");
        long ccn = long.Parse(Console.ReadLine());

        // CC Expiry Date
        Console.Write("Credit Card Expiration Date: ");
        DateTime exp = Convert.ToDateTime(Console.ReadLine());

        // CVV Number
        Console.Write("Credit Card CVV: ");
        int cvv = int.Parse(Console.ReadLine());

        // CC Name
        Console.Write("Credit Cardholder Name: ");
        string cchn = Console.ReadLine();

        // Make CreditCard object
        CreditCard creditCardPayment = new CreditCard(ccn, exp, cvv, cchn);

        // Make CreditCard list for validation
        List<CreditCard> creditCards = new List<CreditCard>();

        // Simulate Process with the bank externally and get OK response
        string validCCPath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\validCreditCard.txt";
        string[] lines = File.ReadAllLines(validCCPath);

        for (int i = 0; i < lines.Length; i += 4)
        {
            // Extract and trim details from each line
            long valid_ccn = long.Parse(lines[i].Split(':')[1].Trim());
            DateTime valid_exp = Convert.ToDateTime(lines[i + 1].Split(':')[1].Trim());
            int valid_cvv = int.Parse(lines[i + 2].Split(':')[1].Trim());
            string valid_cchn = lines[i + 3].Split(':')[1].Trim();

            // Make CreditCard object to add to list of valid credit cards
            CreditCard validCreditCard = new CreditCard(valid_ccn, valid_exp, valid_cvv, valid_cchn);
            creditCards.Add(validCreditCard);
        }

        // Check if the input credit card matches any in the valid list
        bool valid = false;
        foreach (CreditCard validCard in creditCards)
        {
            if (creditCardPayment.CreditCardNumber == validCard.CreditCardNumber &&
                creditCardPayment.ExpirationDate == validCard.ExpirationDate &&
                creditCardPayment.CvvNumber == validCard.CvvNumber &&
                creditCardPayment.CardholderName.Trim().ToLower().Equals(validCard.CardholderName.Trim().ToLower()))
            {
                valid = true; // Match found
            }
        }

        if (valid == true)
        {
            string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
            File.WriteAllText(filePath, string.Empty);
            Console.WriteLine("Credit Card details are correct! You have " + CountBookings(filePath) + " oustanding payments.");
        }
        else
        {
            Console.WriteLine("Incorrect credit card details. Try again.");
        }
        return valid;
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

    static bool DigitalWalletPayment()
    {
        Console.WriteLine("--- Payment by Digital Wallet\n");

        // Wallet Type
        Console.Write("Wallet Type: ");
        string walletType = Console.ReadLine();

        // Wallet Name
        Console.Write("Wallet Owner Name: ");
        string walletName = Console.ReadLine();

        // Wallet Username
        string capitalizedWalletType = char.ToUpper(walletType[0]) + walletType.Substring(1);
        Console.WriteLine("\nLog in with " + capitalizedWalletType);
        Console.Write("Wallet Username: ");
        string walletUsername = Console.ReadLine();

        // Wallet password
        Console.Write("Wallet Password: ");
        string walletPassword = Console.ReadLine();

        // Make DigitalWallet object
        DigitalWallet digitalWalletPayment = new DigitalWallet(walletType, walletName, walletUsername, walletPassword);

        // Make DigitalWallet list for validation
        List<DigitalWallet> digitalWallets = new List<DigitalWallet>();

        //Simulate process with the digital wallet externally and get OK response
        string validCCPath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\validDigitalWallet.txt";
        string[] lines = File.ReadAllLines(validCCPath);

        for (int i = 0; i < lines.Length; i += 4)
        {
            // Extract and trim details from each line
            string validWalletType = lines[i].Split(':')[1].Trim();
            string validWalletOwnerName = lines[i + 1].Split(':')[1].Trim();
            string validWalletUsername = lines[i + 2].Split(':')[1].Trim();
            string validWalletPassword = lines[i + 3].Split(':')[1].Trim();

            // Make CreditCard object to add to list of valid credit cards
            DigitalWallet validDigitalWallet = new DigitalWallet(validWalletType, validWalletOwnerName, validWalletUsername, validWalletPassword);
            digitalWallets.Add(validDigitalWallet);
        }

        // Check if the input credit card matches any in the valid list
        bool valid = false;
        foreach (DigitalWallet digitalWallet in digitalWallets)
        {
            if (digitalWalletPayment.WalletType.Trim().ToLower() == digitalWallet.WalletType.Trim().ToLower() &&
                digitalWalletPayment.WalletOwnerName.Trim().ToLower() == digitalWallet.WalletOwnerName.Trim().ToLower() &&
                digitalWalletPayment.WalletUsername.Equals(digitalWallet.WalletUsername) &&
                digitalWalletPayment.WalletPassword.Equals(digitalWallet.WalletPassword))
            {
                valid = true; // Match found
            }
        }

        if (valid == true)
        {
            string filePath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\BookingDetails.txt";
            File.WriteAllText(filePath, string.Empty);
            Console.WriteLine("Digital Wallet details are correct! You have " + CountBookings(filePath) + " oustanding payments.");
        }
        else
        {
            Console.WriteLine("Incorrect digital wallet details. Try again.");
        }
        return valid;
    }
    // -----------------------------------------------------------------------------------------------
    // End of Yong Shyan's methods

    // Aaron's methods
    // -----------------------------------------------------------------------------------------------
    static void ReserveVehicle() // Method by Aaron
    {
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
                CreateReservation(selectedCar);
                break; // Exit the loop after successful reservation
            }
            else
            {
                Console.WriteLine("The selected car is currently unavailable or reserved. Please choose another car.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }
        }
    }

    static void CreateReservation(Car selectedCar) // Method by Aaron
    {
        Booking booking = new Booking
        {
            ChosenCar = selectedCar,
            BookingLocations = new BookingLocation()
        };

        booking.ChooseStartDateTime();

        booking.ChooseEndDateTime();

        booking.EnterCarMake();

        booking.EnterCarModel();

        booking.VerifyBookingDetails();

        Console.WriteLine("[[1]] iCar Station Physical Pickup \n[[2]] Doorstep Delivery \nSelect your mode of Car Pickup: ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            booking.DisplayPickupLocations();
            booking.EnterPickupLocation();
            booking.VerifyPickupLocation();
        }
        else if (choice == "2")
        {
            booking.Delivery();
            booking.EnterDeliveryAddress();
            booking.VerifyDeliveryAddress();
            booking.UpdateDeliveryStatus();
            booking.BookingLocations.AdditionalPayment = 20;
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter '1' or '2'.");
        }

        booking.DisplayReservationDetails();
        booking.ConfirmReservation();
        booking.SaveBookingDetailsToFile();
        bookings.Add(booking);
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