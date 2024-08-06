using Spectre.Console;

//Welcome Message
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

//Start Menu
int DisplayMenu()
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

while (true)
{
    int menu_opt = DisplayMenu();

    if (menu_opt == 6)
    {
        Console.WriteLine("Exited...");
        break;
    }
}


