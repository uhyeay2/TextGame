using TextGame.Console.Enums;
using TextGame.Console.Screen;

var windowWidth = 150;

Console.WindowWidth = windowWidth;
Console.CursorVisible = false;

var printer = new ScreenPrinter(80, windowWidth, WriteLineStyle.SleepPerCharacter, PrintScreenAlignment.Centered, MenuArrowPosition.BeforeAndAfter);

printer.PrintScreen(new string[] {"Testing", "This is a test", "One, Two, Three", "", "Thank you" });

Console.ReadLine();

var menuOptions = new string[] { "Option 1", "Option 2", "Option 3", "Option 4", "A Really Really Long Option" };

var input = printer.PrintMenuGetIndexSelected(
    new string[] { "This is a menu screen.", "Please select an option below:", "", "-------------------", "" }, menuOptions);

printer.PrintScreen(new string[] { "You selected:", menuOptions[input] });


Console.ReadLine();