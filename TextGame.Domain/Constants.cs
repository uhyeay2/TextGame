using TextGame.Domain.Hangman.Enums;

namespace TextGame.Domain
{
    public static class Constants
    {
        public static class Messages
        {
            public static readonly string[] HomeScreen = new[]
            {
                "Welcome to the Home Screen!",
                "Thank you for playing my TextGame project!", "",
                "You can select from one of the games below.", "",
                "I hope you enjoy!", "", "",
                "Created By: Daniel Aguirre", ""
            };

            public static readonly string[] HangmanIntroduction = new[]
            {
                "Thank you for playing Hangman!", "",
                "Created by Daniel Aguirre", "",
                "I hope you enjoy!", "","",
                "Please select a Genre from the options below.", "",
                "The Genre you select will determine what words are generated.", ""
            };

            public static readonly string[] HangmanCorrectGuess = new[]
            {
                "Great Job!", "", "You found a letter!", "", "Press any key to continue."
            };
            public static readonly string[] HangmanWrongGuess = new[]
            {
                "Oh no!","",
                "I'm not able to find a match for that letter!", "",
                "Press any key to continue."
            };

            public static readonly string[] HangmanGuessAlreadyMade = new[]
            {
                "I'm sorry, it looks like you already tried that letter.", "", 
                "Let's go back and try another.", "", "Press any key to continue."
            };

            public static readonly string[] HangmanInvalidCharacterGuessed = new[]
            {
                "That can't be right....", "",
                "The character you entered doesn't appear to be a letter.", "",
                "Lets go back and try a letter this time.", "", "Press any key to continue."
            };

            public static readonly string[] HangmanGameWon = new[]
            {
                "Congratulation!", "",
                "You won the game of Hangman!", "",
                "I hope you enjoyed playing!", ""
            };

            public static readonly string[] HangmanGameLost = new[]
            {
                "Game Over!", "",
                "Better luck next time!", "",
                "I hope you enjoyed playing!", ""
            };

            public static readonly string[] HangmanContinuePlaying = new[]
            {
                "Thanks for playing Hangman!", "", "What would you like to do next?", ""
            };

            public static readonly string[] HangmanChangeGenres = new[]
            {
                "So you would like to try a new genre?", "",
                "Below are the options you can choose from!", ""
            };
        }

        public static class MenuOptions
        {
            public static readonly string[] HomeScreenOptions = new[]
            {
                Games.Hangman,
                Commands.Quit
            };

            public static readonly string[] HangmanGenreOptions = new[]
            {
                Enum.GetName(Genre.Animal)!,
                Enum.GetName(Genre.Place)!
            };

            public static readonly string[] HangmanContinuePlayingOptions = new[]
            {
                Commands.Continue,
                "Switch Genre",
                Commands.Quit
            };

        }

        public static class Games
        {
            public const string Hangman = "Hangman";
        }

        public static class Commands
        {
            public const string Quit = "Quit";
            public const string Continue = "Continue";
        }
    }
}
