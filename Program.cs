using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();

        while (true)
        {
            int menu_opt = DisplayMenu();
            if (menu_opt == 6)
            {
                Console.WriteLine("Exited...");
                break;
            }
            else if (menu_opt == 5)
            {
                if (DisplayPenaltyAppeals())
                {
                    // If true is returned, exit the program
                    break;
                }
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

