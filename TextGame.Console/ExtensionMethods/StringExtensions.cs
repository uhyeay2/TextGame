using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Console.ExtensionMethods
{
    internal static class StringExtensions
    {
        #region Padding

        /// <summary>
        /// Return the string provided with whitespace padded on both sides evenly to make the string match the targetLength provided.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="targetLength"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string PadToCenter(this string str, int targetLength)
        {
            if (str.Length > targetLength)
            {
                throw new ArgumentOutOfRangeException($"The string ({str}) cannot be padded to the center of the targetLength. " +
                    $"- the targetLength: ({targetLength}) is shorter than the string currently is ({str} - Length: {str.Length})");
            }

            // formula for centering string to target length
            var leftPaddingSize = (targetLength / 2) + (str.Length / 2);

            return str.PadLeft(leftPaddingSize).PadRight(targetLength);
        }

        #endregion

        #region Menu Arrows

        /// <summary>
        /// Insert an arrow after the string. If insertArrow is false, will insert whitespace instead.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string InsertArrowAfter(this string str, bool insertArrow = true)
        {
            return insertArrow ? $"{str} <--" : $"{str}    ";
        }

        /// <summary>
        /// Insert an arrow before the string. if insertArrow is false, will insert whitespace instead.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="insertArrow"></param>
        /// <returns></returns>
        public static string InsertArrowBefore(this string str, bool insertArrow = true)
        {
            return insertArrow ? $"--> {str}" : $"    {str}";
        }

        /// <summary>
        /// Insert an arrow before and after the string. If insertArrow is false, will insert whitespace instead.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="insertArrow"></param>
        /// <returns></returns>
        public static string InsertArrowBeforeAndAfter(this string str, bool insertArrow = true)
        {
            return insertArrow ? $"--> {str} <--" : $"    {str}    ";
        }

        #endregion
    }
}
