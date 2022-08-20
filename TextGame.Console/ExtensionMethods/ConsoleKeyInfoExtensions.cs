using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Console.ExtensionMethods
{
    internal static class ConsoleKeyInfoExtensions
    {
        /// <summary>
        /// Return true if the ConsoleKeyInfo provided is ConsoleKey.Enter.
        /// If the ConsoleKeyInfo is UpArrow, then decrease currentIndex by one OR set to (outOfRangeIndex - 1) if currentIndex == 0
        /// If the ConsoleKeyInfo is DownArrow, then increase the currentIndex by one OR set to 0 if currentIndex == (outOfRangeIndex - 1)
        /// Return false for any input other than enter.
        /// </summary>
        /// <param name="readKey"></param>
        /// <param name="outOfRangeIndex"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        internal static bool TryGetIndex(this ConsoleKeyInfo readKey, int outOfRangeIndex, ref int currentIndex)
        {
            switch (readKey.Key)
            {
                case ConsoleKey.Enter:
                    return true;

                case ConsoleKey.UpArrow:
                    // 0 goes to max, everything else goes down
                    currentIndex = currentIndex == 0 ? --outOfRangeIndex : --currentIndex;
                    goto default;

                case ConsoleKey.DownArrow:
                    // max goes to 0, everything else goes up
                    currentIndex = currentIndex == --outOfRangeIndex ? 0 : ++currentIndex;
                    goto default;

                default: return false;
            }
        }
    }
}
