using TextGame.Console.Enums;
using TextGame.Console.Screen;

var windowWidth = 150;

Console.WindowWidth = windowWidth;
Console.CursorVisible = false;

// Initialize ScreenPrinter Object
var printer = new ScreenPrinter();

var introMessage = new string[]
{
    "Thank you for trying my Screen Printer", "",
    "Try using the Custom Constructor for ScreenPrinter.", "Each parameter is a different setting for printing.", "",
    "You can also update these settings with ScreenPrinter.UpdateSettings()", "",
    "I would love to hear any feedback or questions you may have.", "Feel free to contact me on Discord.", "",
    "Daniel Aguirre - RedRain#6970 ", "", "Press Enter to Continue."
};

// Pass in an IEnumerable<string> that you wish to print to the screen.
printer.PrintScreen(introMessage);
Console.ReadLine();

var menuScreenMessage = new string[]
{
    "This is an example of a menu screen.", "The PrintMenuGetIndexSelected() is used to print this screen.", "",
    "This method will return the index of whatever option you select from the menu options.", "",
    "You can have as many menu options as you like, but if you are centering/using borders then ",
    "make sure to be mindful of how long your menuOptions are, they must fit inside your border.", "",
    "Select an option below, and then we will print your selection."
};

var menuOptions = new string[] { "Index 0 - Option 1", "Index 1 - Option 2", "Index 2 - Option 3"};

// Pass in IEnumerable<string> of messages to print above the MenuOptions, as well as the MenuOptions you wish to print.
var input = printer.PrintMenuGetIndexSelected(menuScreenMessage, menuOptions);

printer.PrintScreen(new string[] { "You selected:", menuOptions[input], "", "Thanks for trying this out! I hope you enjoy it." });

Console.ReadLine();