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

    static void Main(string[] args)
    {
        DisplayWelcomeMessage();

        while (true)
        {
            int menu_opt = DisplayMenu();
            switch (menu_opt)
            {
                case 1:
                    Console.WriteLine("[[1]] Register Vehicle");
                    // Add registration logic here
                    break;
                case 2:
                    Console.WriteLine("[[2]] Make Payment");
                    // Add payment logic here
                    break;
                case 3:
                    Console.WriteLine("[[3]] Reserve Vehicle");
                    ReserveVehicle();
                    break;
                case 4:
                    Console.WriteLine("[[4]] Register Renter");
                    // Add renter registration logic here
                    break;
                case 5:
                    Console.WriteLine("[[5]] Review Appeal");
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
                .Title("Hello, what are we going to be [green]doing here[/] today?")
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