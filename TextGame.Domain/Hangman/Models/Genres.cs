using TextGame.Domain.Hangman.Enums;

namespace TextGame.Domain.Hangman.Models
{
    public static class Genres
    {        
        private static readonly Dictionary<Genre, string[]> _genreAnswerMapping = new()
        {
            { Genre.Animal,  new []
                {"Elephant", "Zebra", "Monkey", "Feline", "Puppy", "Horse" }
            },
            { Genre.Place,  new []
                {"Florida", "Alabama", "Alaska", "Paris", "England", "Antartica" }
            },
        };

        public static AnswerList GetAnswerList(Genre g) => new(_genreAnswerMapping[g]);
    }
}