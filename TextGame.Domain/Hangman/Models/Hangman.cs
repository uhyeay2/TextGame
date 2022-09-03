using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGame.Domain.Hangman.Enums;

namespace TextGame.Domain.Hangman.Models
{
    public class Hangman
    {
        #region Private Properties

        static Dictionary<int, string[]> _display { get; set; } = new()
        {
            {0,  new []
                {
                "     _______     ",
                "     |      |    ",
                "     |      |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "         ___|___ ",
                } 
            },
            {1, new[]
                {
                "     _______     ",
                "    _|_     |    ",
                "   |x x|    |    ",
                "   |___|    |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "         ___|___ ",
                }
            },
            {2, new[]
                {
                "     _______     ",
                "    _|_     |    ",
                "   |x x|    |    ",
                "   |___|    |    ",
                "     |      |    ",
                "     |      |    ",
                "     |      |    ",
                "     |      |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "         ___|___ ",
                }
            },
            {3, new[]
                {
                "     _______     ",
                "    _|_     |    ",
                "   |x x|    |    ",
                "   |___|    |    ",
                "     |      |    ",
                "    /|      |    ",
                "   / |      |    ",
                "     |      |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "         ___|___ ",
                }
            },
            {4, new[]
                {
                "     _______     ",
                "    _|_     |    ",
                "   |x x|    |    ",
                "   |___|    |    ",
                "     |      |    ",
                "    /|\\     |    ",
                "   / | \\    |    ",
                "     |      |    ",
                "            |    ",
                "            |    ",
                "            |    ",
                "         ___|___ ",
                }
            },
            {5, new[]
                {
                "     _______     ",
                "    _|_     |    ",
                "   |x x|    |    ",
                "   |___|    |    ",
                "     |      |    ",
                "    /|\\     |    ",
                "   / | \\    |    ",
                "     |      |    ",
                "      \\     |    ",
                "       \\    |    ",
                "            |    ",
                "         ___|___ ",
                }
            },
            {6, new[]
                {
                "     _______     ",
                "    _|_     |    ",
                "   |x x|    |    ",
                "   |___|    |    ",
                "     |      |    ",
                "    /|\\     |    ",
                "   / | \\    |    ",
                "     |      |    ",
                "    / \\     |    ",
                "   /   \\    |    ",
                "            |    ",
                "         ___|___ ",
                }
            }
        };

        int _wrongGuessesMade = 0;

        private int _maxWrongGuesses = _display.Count - 1;

        #endregion

        #region Exposed Members

        public bool IsAlive => _wrongGuessesMade < _maxWrongGuesses;

        public int GuessesLeft => _maxWrongGuesses - _wrongGuessesMade;

        /// <summary>
        /// Increase the number of wrong guesses made if the addGuessResponse provided is WrongGuessAdded
        /// </summary>
        /// <param name="r"></param>
        public void UpdateGuessesMade(AddGuessResponse r) => _wrongGuessesMade += 
                r == AddGuessResponse.WrongGuessAdded ? 1 : 0;

        /// <summary>
        /// Get the string array to display for this hangman based on the number of wrong guesses made.
        /// </summary>
        /// <returns></returns>
        public string[] GetDisplay() => _display[DetermineDisplayToSelect()];

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Helper method to return _display.Count - 1 if there are too many wrong Guesses, otherwise return the number of wrongGuesses made.
        /// </summary>
        /// <returns></returns>
        int DetermineDisplayToSelect() => _wrongGuessesMade >= _display.Count ? _maxWrongGuesses : _wrongGuessesMade;

        #endregion
    }
}
