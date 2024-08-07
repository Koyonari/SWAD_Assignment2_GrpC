using Spectre.Console;
using SWAD_Assignment2_GrpC;
using System;
using System.Collections.Generic;



private static CarRegistry _carRegistry = new CarRegistry(
    @"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_makes.txt",
    @"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_models.txt",
    @"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_listings.txt"
);
private static List<Car> _carOwnerCars = _carRegistry.GetAllCars();


    DisplayWelcomeMessage();
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
                RegisterCar();
                break;
            case 5:
                if (DisplayPenaltyAppeals())
                {
                    // If true is returned, exit the program
                    return;
                }
                break;
            case 6:
                Console.WriteLine("Exited...");
                return;
            case 7:
                ViewCarListings();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
    

static void DisplayWelcomeMessage()
{
    AnsiConsole.Write(new FigletText("Welcome to").LeftJustified());
    AnsiConsole.Write(new FigletText("ICar").Centered().Color(Color.Aqua));
    AnsiConsole.Write(new FigletText("----------").LeftJustified().Color(Color.Teal));
}
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
                    if (DisplayPenaltyAppeals())
                    {
                        // If true is returned, exit the program
                        return; // Exits the loop and program
                    }
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
                "Register Vehicle",
                "Make Payment",
                "Reserve Vehicle",
                "Register Renter",
                "Review Appeal",
                "Exit iCar",
                "View My Listings"
            }));

    // Parsing the selected choice to integer, adjusting for zero-based index
    return choice switch
    {
        "Register Vehicle" => 1,
        "Make Payment" => 2,
        "Reserve Vehicle" => 3,
        "Register Renter" => 4,
        "Review Appeal" => 5,
        "Exit iCar" => 6,
        "View My Listings" => 7,
        _ => 0
    };
}

static void RegisterCar()
{
    // Collect car details from user
    string make = PromptForCarMake();
    string model = PromptForCarModel();
    int year = PromptForCarYear();
    int mileage = PromptForCarMileage();
    string listingName = PromptForListingName();
    string licensePlateNumber = PromptForLicensePlate();
    bool insuranceStatus = PromptForInsuranceStatus();
    string description = PromptForDescription();
    decimal dailyRate = PromptForDailyRate();

    // Create RentalRates object
    RentalRates rentalRates = new RentalRates
    {
        DailyRate = dailyRate,
    };

    // Confirm details
    bool confirmDetails = AnsiConsole.Confirm($"Confirm details:\nMake: {make}\nModel: {model}\nYear: {year}\nStatus: Available\nMileage: {mileage}\nListing Name: {listingName}\nLicense Plate Number: {licensePlateNumber}\nInsured: {insuranceStatus}\nDescription: {description}\nDaily Rate: {dailyRate:C}\nAre these details correct?");

    if (!confirmDetails)
    {
        Console.WriteLine("Car registration cancelled. Please restart the registration process.");
        return;
    }

    // Create and add the car
    if (_carRegistry.IsValidMake(make) && _carRegistry.IsValidModel(model))
    {
        Car car = new Car(GenerateId(), model, make, year, "Available", mileage, listingName, licensePlateNumber, insuranceStatus, description, rentalRates);
        _carRegistry.AddCar(car);
        _carOwnerCars.Add(car);
        Console.WriteLine("Car registered successfully!");
    }
    else
    {
        Console.WriteLine("Invalid make or model.");
    }
}

// Validation methods
static string PromptForCarMake()
{
    while (true)
    {
        string make = AnsiConsole.Ask<string>("Enter the car [green]make[/]:");
        if (!string.IsNullOrWhiteSpace(make))
            return make;
        Console.WriteLine("Error: Make cannot be empty. Please enter a valid make.");
    }
}

static string PromptForCarModel()
{
    while (true)
    {
        string model = AnsiConsole.Ask<string>("Enter the car [green]model[/]:");
        if (!string.IsNullOrWhiteSpace(model))
            return model;
        Console.WriteLine("Error: Model cannot be empty. Please enter a valid model.");
    }
}

static int PromptForCarYear()
{
    while (true)
    {
        int year = AnsiConsole.Ask<int>("Enter the car [green]year[/]:");
        if (year >= 1886 && year <= DateTime.Now.Year)
            return year;
        Console.WriteLine("Error: Invalid year. The year must be between 1886 and the current year.");
    }
}

static int PromptForCarMileage()
{
    while (true)
    {
        int mileage = AnsiConsole.Ask<int>("Enter the car [green]mileage[/]:");
        if (mileage >= 0)
            return mileage;
        Console.WriteLine("Error: Mileage must be a non-negative integer.");
    }
}

static string PromptForListingName()
{
    while (true)
    {
        string listingName = AnsiConsole.Ask<string>("Enter the car's [green]listing name[/]:");
        if (!string.IsNullOrWhiteSpace(listingName))
            return listingName;
        Console.WriteLine("Error: Listing name cannot be empty. Please enter a valid listing name.");
    }
}

static string PromptForLicensePlate()
{
    while (true)
    {
        string licensePlateNumber = AnsiConsole.Ask<string>("Enter the car's [green]license plate number[/]:");
        if (IsValidLicensePlate(licensePlateNumber))
            return licensePlateNumber;
        Console.WriteLine("Error: Invalid license plate number. Please follow the correct format.");
    }
}

static bool PromptForInsuranceStatus()
{
    return AnsiConsole.Confirm("Is the car [green]insured[/]?");
}

static string PromptForDescription()
{
    while (true)
    {
        string description = AnsiConsole.Ask<string>("Enter the car's [green]description[/]:");
        if (!string.IsNullOrWhiteSpace(description))
            return description;
        Console.WriteLine("Error: Description cannot be empty. Please enter a valid description.");
    }
}

static decimal PromptForDailyRate()
{
    while (true)
    {
        decimal dailyRate = AnsiConsole.Ask<decimal>("Enter the car's [green]daily rental rate[/]:");
        if (dailyRate >= 0)
            return dailyRate;
        Console.WriteLine("Error: Rental rate must be non-negative.");
    }
}

// License plate validation method
static bool IsValidLicensePlate(string licensePlate)
{
    var regex = new System.Text.RegularExpressions.Regex(@"^[A-Z0-9]{6,8}$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    return regex.IsMatch(licensePlate);
}

static void ViewCarListings()
{
    if (_carOwnerCars.Count == 0)
    {
        Console.WriteLine("No cars registered.");
        return;
    }

    // Display the list of registered cars with their IDs
    Console.WriteLine("Your Registered Cars:");
    foreach (var car in _carOwnerCars)
    {
        Console.WriteLine($"ID: {car.Id}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Status: {car.Status}, Mileage: {car.Mileage}, Listing Name: {car.ListingName}, License Plate: {car.LicensePlateNumber}, Insured: {car.InsuranceStatus}, Rental Rate: {car.RentalRates.DailyRate:C}");
    }

    // Ask the user which car they want to edit by ID
    int carId = AnsiConsole.Ask<int>("Enter the [green]ID[/] of the car you want to [green]edit[/]:");

    // Find the car by ID
    var selectedCar = _carOwnerCars.FirstOrDefault(c => c.Id == carId);

    if (selectedCar == null)
    {
        Console.WriteLine("Car with the specified ID not found. Returning to the main menu.");
        return;
    }

    // Call the method to edit the selected car
    EditListing(selectedCar);
}

static int GenerateId()
{
    int maxId = 0;

    // Check if the file exists and read the IDs
    if (File.Exists(@"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_listings.txt"))
    {
        var lines = File.ReadAllLines(@"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_listings.txt");
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            if (int.TryParse(parts[0], out int id))
            {
                if (id > maxId)
                {
                    maxId = id;
                }
            }
        }
    }
    // Return the next ID
    return maxId + 1;
}

static void EditListing(Car car)
{
    Console.WriteLine("Editing Car:");

    // Prompt user for new values, defaulting to existing values where no input is provided
    string newMake = AnsiConsole.Ask<string>($"Enter the new [green]make[/] (current: {car.Make}):") ?? car.Make;
    string newModel = AnsiConsole.Ask<string>($"Enter the new [green]model[/] (current: {car.Model}):") ?? car.Model;
    int newYear = AnsiConsole.Ask<int>($"Enter the new [green]year[/] (current: {car.Year}):", defaultValue: car.Year);
    int newMileage = AnsiConsole.Ask<int>($"Enter the new [green]mileage[/] (current: {car.Mileage}):", defaultValue: car.Mileage);
    string newListingName = AnsiConsole.Ask<string>($"Enter the new [green]listing name[/] (current: {car.ListingName}):") ?? car.ListingName;
    string newLicensePlateNumber = AnsiConsole.Ask<string>($"Enter the new [green]license plate number[/] (current: {car.LicensePlateNumber}):") ?? car.LicensePlateNumber;
    bool newInsuranceStatus = AnsiConsole.Confirm($"Is the car [green]insured[/] (current: {car.InsuranceStatus})?", defaultValue: car.InsuranceStatus);
    string newDescription = AnsiConsole.Ask<string>($"Enter the new [green]description[/] (current: {car.Description}):") ?? car.Description;
    decimal newDailyRate = AnsiConsole.Ask<decimal>($"Enter the new [green]daily rental rate[/] (current: {car.RentalRates.DailyRate:C}):", defaultValue: car.RentalRates.DailyRate);

    // Validate year
    if (newYear < 1886 || newYear > DateTime.Now.Year + 10)
    {
        Console.WriteLine("Error: Invalid year. The year must be between 1886 and the current year plus 10 years.");
        return;
    }

    // Validate mileage
    if (newMileage < 0)
    {
        Console.WriteLine("Error: Mileage must be a non-negative integer.");
        return;
    }

    // Validate license plate
    if (!IsValidLicensePlate(newLicensePlateNumber))
    {
        Console.WriteLine("Error: Invalid license plate number. Please follow the correct format.");
        return;
    }

    // Validate daily rate
    if (newDailyRate < 0)
    {
        Console.WriteLine("Error: Rental rate must be non-negative.");
        return;
    }

    // Update the car details
    car.Make = newMake;
    car.Model = newModel;
    car.Year = newYear;
    car.Status = "Available";
    car.Mileage = newMileage;
    car.ListingName = newListingName;
    car.LicensePlateNumber = newLicensePlateNumber;
    car.InsuranceStatus = newInsuranceStatus;
    car.Description = newDescription;
    car.RentalRates.DailyRate = newDailyRate;

    Console.WriteLine("Car listing updated successfully!");
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

static bool DisplayPenaltyAppeals()
{
    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Displaying Penalty Appeals")
            .AddChoices(new[] {
                "Appealing for Penalty #00001 (19/04/2024)",
                "Appealing for Penalty #00002 (12/03/2024)",
                "Appealing for Penalty #00003 (10/02/2024)",
                "Appealing for Penalty #00004 (29/01/2024)"
            }));

    switch (choice)
    {
        case "Appealing for Penalty #00001 (19/04/2024)":
            Console.WriteLine("Penalty #00001 (19/04/2024)\nReason: First time returning car late - Car Owner (Available)");
            Console.WriteLine("User ID:00029 \nAddress: Singapore Zoo Blk 29 St 10 #01-102 \nEmail: johncena@gmail.com \nUsername: BigJohn \nContact number: 90211239 \nFull Name: John Tan \nDate Joined: 07/08/2020");
            break;
        case "Appealing for Penalty #00002 (12/03/2024)":
            Console.WriteLine("Penalty #00002 (12/03/2024)\nReason: Damages to rental car - Renter (Available)");
            Console.WriteLine("User ID:00045 \nAddress: Marina Bay Sands Tower 3 #35-301 \nEmail: emilywong@hotmail.com \nUsername: SingaporeStar \nContact number: 91234567 \nFull Name: Emily Wong \nDate Joined: 15/03/2022");
            break;
        case "Appealing for Penalty #00003 (10/02/2024)":
            Console.WriteLine("Penalty #00003 (10/02/2024)\nReason: Late payment - Car Owner (Available)");
            Console.WriteLine("User ID:00078 \nAddress: Changi Airport Terminal 4 Staff Quarters #02-15 \nEmail: pilotlee@yahoo.com \nUsername: SkyKing88 \nContact number: 98765432 \nFull Name: Alex Lee \nDate Joined: 22/11/2021");
            break;
        case "Appealing for Penalty #00004 (29/01/2024)":
            Console.WriteLine("Penalty #00004 (29/01/2024)\nReason: Excessive mileage - Renter (Available)");
            Console.WriteLine("User ID:00103 \nAddress: Sentosa Cove Ocean Drive 18 \nEmail: mariatan@gmail.com \nUsername: BeachLover \nContact number: 93456789 \nFull Name: Maria Tan \nDate Joined: 01/06/2023");
            break;
        default:
            Console.WriteLine("Invalid choice.");
            return false;
    }

    return AnsiConsole.Confirm("Would you like to exit the program?");
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
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter '1' or '2'.");
        }

        booking.DisplayReservationDetails();
        booking.ConfirmReservation();
    }
    // -----------------------------------------------------------------------------------------------
    // End of Aaron's methods

    static bool DisplayPenaltyAppeals()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Displaying Penalty Appeals")
                .AddChoices(new[] {
                    "[[1]] Appealing for Penalty #00001 (19/04/2024)",
                    "[[2]] Appealing for Penalty #00002 (12/03/2024)",
                    "[[3]] Appealing for Penalty #00003 (10/02/2024)",
                    "[[4]] Appealing for Penalty #00004 (29/01/2024)"
                }));
        switch (choice[2] - '0')
        {
            case 1:
                Console.WriteLine("Penalty #00001 (19/04/2024)\nReason: First time returning car late - Car Owner (Available)");
                Console.WriteLine("User ID:00029 \nAddress: Singapore Zoo Blk 29 St 10 #01-102 \nEmail: johncena@gmail.com \nUsername: BigJohn \nContact number: 90211239 \nFull Name: John Tan \nDate Joined: 07/08/2020");
                return false;
            case 2:
                Console.WriteLine($"Penalty #00002 (12/03/2024)\nReason: Damages to rental car - Renter (Available)");
                Console.WriteLine("User ID:00045 \nAddress: Marina Bay Sands Tower 3 #35-301 \nEmail: emilywong@hotmail.com \nUsername: SingaporeStar \nContact number: 91234567 \nFull Name: Emily Wong \nDate Joined: 15/03/2022");
                return false;
            case 3:
                Console.WriteLine($"Penalty #00003 (10/02/2024)\nReason: Late payment - Car Owner (Available)");
                Console.WriteLine("User ID:00078 \nAddress: Changi Airport Terminal 4 Staff Quarters #02-15 \nEmail: pilotlee@yahoo.com \nUsername: SkyKing88 \nContact number: 98765432 \nFull Name: Alex Lee \nDate Joined: 22/11/2021");
                return false;
            case 4:
                Console.WriteLine($"Penalty #00004 (29/01/2024)\nReason: Excessive mileage - Renter (Available)");
                Console.WriteLine("User ID:00103 \nAddress: Sentosa Cove Ocean Drive 18 \nEmail: mariatan@gmail.com \nUsername: BeachLover \nContact number: 93456789 \nFull Name: Maria Tan \nDate Joined: 01/06/2023");
                return false;
            default:
                return false;
        }
    }
}