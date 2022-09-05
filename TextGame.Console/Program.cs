using TextGame.Console.Games;
using TextGame.Console.Screen;
using TextGame.Domain;

var windowWidth = 150;
Console.WindowWidth = windowWidth;
Console.CursorVisible = false;

// Initialize ScreenPrinter Object for printing to the console.
var printer = new ScreenPrinter();

printer.UpdateSettings(sleepTimer: 20);

var isPlaying = true;

while (isPlaying)
{
    var input = printer.PrintMenuGetIndexSelected(Constants.Messages.HomeScreen, Constants.MenuOptions.HomeScreenOptions);

    switch (input)
    {
        case var i when Constants.MenuOptions.HomeScreenOptions[i] == Constants.Games.Hangman:
            var hangman = new HangmanGame(printer);
            hangman.Start();
            break;

        case var i when Constants.MenuOptions.HomeScreenOptions[i] == Constants.Commands.Quit:
            isPlaying = false;
            break;
    };
}