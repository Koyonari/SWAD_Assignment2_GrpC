using Spectre.Console;
using SWAD_Assignment2_GrpC;
using System;
using System.Collections.Generic;

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