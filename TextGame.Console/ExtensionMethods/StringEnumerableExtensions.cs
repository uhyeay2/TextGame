using TextGame.Console.Enums;

namespace TextGame.Console.ExtensionMethods
{
    internal static class StringEnumerableExtensions
    {
        #region Padding

        /// <summary>
        /// Return the collection of strings provided with whitespace padded on both sides of each string
        /// evenly to make all the strings match the targetLength provided.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="targetLength"></param>
        /// <returns></returns>
        internal static IEnumerable<string> PadToCenter(this IEnumerable<string> strings, int targetLength) =>
            strings.Select(s => s.PadToCenter(targetLength));

        /// <summary>
        /// Pad the collection of strings so the right so that they are all the same length.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static IEnumerable<string> PadRightToEqualLengths(this IEnumerable<string> strings) =>
            strings.Select(s => s.PadRight(strings.Max(s => s.Length)));

        #endregion

        #region Menu Arrows

        /// <summary>
        /// Apply "--> " or " <--" before and/or after the string at the provided index.
        /// Which arrows to use is dependant upon MenuArrow Enum - Before, After, BeforeAndAfter
        /// </summary>
        /// <param name="menuArrow"></param>
        /// <param name="strings"></param>
        /// <param name="indexToApply"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static IEnumerable<string> ApplyArrows(this IEnumerable<string> strings, int indexToApply, MenuArrowPosition menuArrow)
        {
            return menuArrow switch
            {
                MenuArrowPosition.BeforeAndAfter => strings.PadRightToEqualLengths().InsertArrowBeforeAndAfterIndex(indexToApply),

                MenuArrowPosition.Before => strings.InsertArrowBeforeIndex(indexToApply),

                MenuArrowPosition.After => strings.PadRightToEqualLengths().InsertArrowAfterIndex(indexToApply),

                _ => throw new NotImplementedException(
                    $"MenuArrowExtensions.ApplyArrows() is not built to handle a MenuArrowPosition of {nameof(menuArrow)}"),
            };
        }

        /// <summary>
        /// First pad all strings to equal length. Then Insert " <--" after the string at index provided.
        /// Insert "    " after all other strings to keep the spacing the same.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IEnumerable<string> InsertArrowAfterIndex(this IEnumerable<string> strings, int index) =>
            strings.Select((s, i) => s.InsertArrowAfter(i == index));

        /// <summary>
        /// Insert "--> " before and " <--" after the string at index provided. Insert "    " before and after all
        /// other strings to keep spacing the same.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IEnumerable<string> InsertArrowBeforeAndAfterIndex(this IEnumerable<string> strings, int index) =>
            strings.Select((s, i) => s.InsertArrowBeforeAndAfter(i == index));

        /// <summary>
        /// Insert "--> " before the string at index provided. Insert "    " before all others strings to keep spacing the same.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IEnumerable<string> InsertArrowBeforeIndex(this IEnumerable<string> strings, int index) =>
            strings.Select((s, i) => s.InsertArrowBefore(i == index));

        #endregion Insert Arrow Before and/or After by index
    }
}
