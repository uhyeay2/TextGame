using TextGame.Console.Enums;
using TextGame.Console.ExtensionMethods;

namespace TextGame.Console.Screen
{
    internal class ScreenPrinter
    {
        #region Private Properties 

        /// <summary>
        /// How long in Miliseconds to wait between writing character/lines depending upon WriteLineStyle selected
        /// </summary>
        private int _sleepTimer;
        
        /// <summary>
        /// Maximum Width to print
        /// </summary>
        private int _screenWidth;

        /// <summary>
        /// What Style of Console.WriteLine to use - Either write characters one at a time (Sleep Per Character),
        /// Write whole lines at a time (Sleep Per Line), or just write the entire screen at once (Normal)
        /// </summary>
        private WriteLineStyle _writeLineStyle;

        /// <summary>
        /// What Alignment to do when calling Print - Either print Normally, Centered (Vertically and Horizontally),
        /// CenteredHorizontally, or CenteredVertically
        /// </summary>
        private PrintScreenAlignment _screenAlignment;

        /// <summary>
        /// Where to place arrows on Menu Screens - Either Before, After, or BeforeAndAfter
        /// </summary>
        private MenuArrowPosition _menuArrowPosition;

        #endregion

        #region Constructors

        public ScreenPrinter() : this(0, System.Console.WindowWidth, WriteLineStyle.Normal, PrintScreenAlignment.Centered, MenuArrowPosition.BeforeAndAfter) { }

        public ScreenPrinter(int sleepTimer, int screenWidth, WriteLineStyle writeLineStyle, PrintScreenAlignment screenAlignment, MenuArrowPosition menuArrowPosition)
        {
            if(screenWidth <= 0)
            {
                throw new ApplicationException("Cannot initialize ScreenPrinter with a ScreenWidth less than or equal to 0. - " +
                    $"ScreenWidth received: {screenWidth}");
            }

            _sleepTimer = sleepTimer;
            _screenWidth = screenWidth;
            _writeLineStyle = writeLineStyle;
            _screenAlignment = screenAlignment;
            _menuArrowPosition = menuArrowPosition;
        }

        #endregion

        #region Exposed Methods

        /// <summary>
        /// Update the settings provided - Any settings not provided (null) will be kept as is.
        /// </summary>
        /// <param name="sleepTimer"></param>
        /// <param name="screenWidth"></param>
        /// <param name="writeLineStyle"></param>
        /// <param name="screenAlignment"></param>
        public void UpdateSettings(int? sleepTimer = null, int? screenWidth = null, WriteLineStyle? writeLineStyle = null, PrintScreenAlignment? screenAlignment = null)
        {
            _sleepTimer = sleepTimer ?? _sleepTimer;
            _screenWidth = screenWidth ?? _screenWidth;
            _writeLineStyle = writeLineStyle ?? _writeLineStyle;
            _screenAlignment = screenAlignment ?? _screenAlignment;
        }

        /// <summary>
        /// Concat the collections of strings provided together, then Print them to the screen.
        /// By default will not clear the screen before printing unless centering vertically.
        /// </summary>
        /// <param name="clearBeforePrint"></param>
        /// <param name="strings"></param>
        public void PrintScreen(bool clearBeforePrint = false, params IEnumerable<string>[] strings) =>
            PrintScreen(strings.Aggregate((a, b) => a.Concat(b)), clearBeforePrint);

        /// <summary>
        /// Print the strings provided to the screen, by default will not clear before printing unless Centering Vertically.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="clearBeforePrint"></param>
        public void PrintScreen(IEnumerable<string> strings, bool clearBeforePrint = false)
        {
            if (clearBeforePrint)
            {
                System.Console.Clear();
            }

            if(_screenAlignment.IsCenteringVertically())
            {
                ClearScreenAndSkipLines(strings.Count());
            }

            Print(_screenAlignment.IsCenteringHorizontally() ? strings.PadToCenter(_screenWidth) : strings);
        }

        /// <summary>
        /// Print a Menu Screen, and allow the user to cycle up/down and click enter on the menuOption that they wish to select. 
        /// Returns the index of menuOptions that was selected. SleepTimer/WriteLineStyle as disabled during looping of screen
        /// to avoid delays when reprinting the menu selection. By default will not clear before printing unless Centering Vertically.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="menuOptions"></param>
        /// <param name="clearBeforePrinting"></param>
        /// <returns></returns>
        public int PrintMenuGetIndexSelected(IEnumerable<string> strings, IEnumerable<string> menuOptions, bool clearBeforePrinting = false)
        {
            var selectionMade = false;
            var selectedIndex = 0;

            PrintScreen(clearBeforePrinting, strings, menuOptions.ApplyArrows(selectedIndex, _menuArrowPosition));

            // store current sleepTimer and writeLineStyle to reset later
            var originalSleepTimer = _sleepTimer;
            var originalWriteLineStyle = _writeLineStyle;

            // set sleepTimer to 0 and writeLineStyle to normal to avoid delays when printing menus
            UpdateSettings(sleepTimer: 0, writeLineStyle: WriteLineStyle.Normal);

            // Loop to keep printing and getting input
            while (!selectionMade)
            {
                PrintScreen(clearBeforePrinting, strings, menuOptions.ApplyArrows(selectedIndex, _menuArrowPosition));

                selectionMade = System.Console.ReadKey().TryGetIndex(menuOptions.Count(), ref selectedIndex);
            }

            // set sleepTimer and writeLineStype back to what it was before Printing this menu screen.
            UpdateSettings(sleepTimer: originalSleepTimer, writeLineStyle: originalWriteLineStyle);

            return selectedIndex;
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Clear the screen using Console.Clear, and then use the numberOfStringsToPrint to calculate how many
        /// lines must be skipped so that the text is centered vertically. Then print the empty lines.
        /// </summary>
        /// <param name="numberOfStringsToPrint"></param>
        private static void ClearScreenAndSkipLines(int numberOfStringsToPrint)
        {
            System.Console.Clear();

            var numberOfLinesToSkip = System.Console.WindowHeight / 2 - numberOfStringsToPrint / 2;

            System.Console.Write(new string('\n', numberOfLinesToSkip));
        }

        /// <summary>
        /// Print a collection of strings using the current WriteLineStyle
        /// </summary>
        /// <param name="strings"></param>
        private void Print(IEnumerable<string> strings) => strings.ToList().ForEach(Print);

        /// <summary>
        /// Print a string using the current WriteLineStyle
        /// </summary>
        /// <param name="str"></param>
        private void Print(string str)
        {            
            switch (_writeLineStyle)
            {
                case WriteLineStyle.Normal:
                    goto default;

                case WriteLineStyle.SleepPerLine:
                    Thread.Sleep(_sleepTimer);
                    System.Console.WriteLine(str);
                    break;

                case WriteLineStyle.SleepPerCharacter:
                    foreach (var c in str)
                    {
                        if (c != ' ')
                        {
                            Thread.Sleep(_sleepTimer);
                        }
                        System.Console.Write(c);
                    }
                    System.Console.Write("\n");
                    break;

                default:
                    System.Console.WriteLine(str);
                    break;
            }

            // This fixes a bug so that when we press a key while a screen is being printed, the key press is ignored.
            while (System.Console.KeyAvailable) { System.Console.ReadKey(true); }
        }

        #endregion
    }
}
