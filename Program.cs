using Spectre.Console;



private static CarRegistry _carRegistry = new CarRegistry(
    @"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_makes.txt",
    @"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_models.txt",
    @"C:\Users\65942\OneDrive - Ngee Ann Polytechnic\Attachments\sem3\SWAD\SWAD_assg2\car_listings.txt"
);
private static List<Car> _carOwnerCars = _carRegistry.GetAllCars();


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

