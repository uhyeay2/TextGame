using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Domain.Hangman.Enums
{
    public enum AddGuessResponse
    {
        CorrectGuessAdded,
        WrongGuessAdded,
        GuessAlreadyMade,
        InvalidCharacter
    }
}
