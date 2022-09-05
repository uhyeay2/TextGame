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

        /// <summary>
        /// The type of borders to print screens with - Either None, Just Menus, Just Screens, or Screens And Menus
        /// </summary>
        private BorderStyle _borderStyle;

        #endregion

        #region Constructors

        public ScreenPrinter() : this(35, System.Console.WindowWidth, WriteLineStyle.SleepPerCharacter, PrintScreenAlignment.Centered, MenuArrowPosition.BeforeAndAfter, BorderStyle.MenuCenteredAndScreen) { }

        public ScreenPrinter(int sleepTimer, int screenWidth, WriteLineStyle writeLineStyle, PrintScreenAlignment screenAlignment, MenuArrowPosition menuArrowPosition, BorderStyle borderStyle)
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
            _borderStyle = borderStyle;
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
        public void UpdateSettings(int? sleepTimer = null, int? screenWidth = null, WriteLineStyle? writeLineStyle = null, PrintScreenAlignment? screenAlignment = null, BorderStyle? borderStyle = null)
        {
            _sleepTimer = sleepTimer ?? _sleepTimer;
            _screenWidth = screenWidth ?? _screenWidth;
            _writeLineStyle = writeLineStyle ?? _writeLineStyle;
            _screenAlignment = screenAlignment ?? _screenAlignment;
            _borderStyle = borderStyle ?? _borderStyle;
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
            strings = PrepForPrinting(strings, clearBeforePrint);

            Print(strings);
        }

        /// <summary>
        /// Print a Menu Screen, and allow the user to cycle up/down and click enter on the menuOption that they wish to select. 
        /// Returns the index of menuOptions that was selected. SleepTimer/WriteLineStyle as disabled during looping of screen
        /// to avoid delays when reprinting the menu selection. By default will not clear before printing unless Centering Vertically.
        /// Can optionally pass in a MenuBorderSize which will be used if MenuBorder style is enabled. If MenuBorderSize is not 
        /// set then the Border will be set based on the longest menuOptions.Length.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="menuOptions"></param>
        /// <param name="clearBeforePrinting"></param>
        /// <returns></returns>
        public int PrintMenuGetIndexSelected(IEnumerable<string> strings, IEnumerable<string> menuOptions, bool clearBeforePrinting = false, int menuBorderSize = 0)
        {
            var selectionMade = false;
            var selectedIndex = 0;

            // Print Screen the first time using current settings.
            PrintScreen(clearBeforePrinting, strings, PrepMenuOptions(menuOptions, selectedIndex, menuBorderSize));

            // store current sleepTimer and writeLineStyle to reset later
            var originalSleepTimer = _sleepTimer;
            var originalWriteLineStyle = _writeLineStyle;

            // set sleepTimer to 0 and writeLineStyle to normal to avoid delays when printing Menu Screens
            UpdateSettings(sleepTimer: 0, writeLineStyle: WriteLineStyle.Normal);

            // Loop to keep printing until input is received (User clicks Enter)
            while (!selectionMade)
            {
                PrintScreen(clearBeforePrinting, strings, PrepMenuOptions(menuOptions, selectedIndex, menuBorderSize));

                selectionMade = System.Console.ReadKey().TryGetIndex(menuOptions.Count(), ref selectedIndex);
            }

            // set sleepTimer and writeLineStype back to what it was before Printing this menu screen.
            UpdateSettings(sleepTimer: originalSleepTimer, writeLineStyle: originalWriteLineStyle);

            return selectedIndex;
        }

        public char ReadKey() => System.Console.ReadKey().KeyChar;

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Clear Screen before printing depending upon bool passed in. 
        /// Clear Screen and Skip Lines if Centering Vertically. 
        /// Apply Horizontal Padding to each string if Centering Horizontally. 
        /// Apply Border if ScreenBorder is Enabled. Then return the strings.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="clearBeforePrint"></param>
        /// <returns></returns>
        private IEnumerable<string> PrepForPrinting(IEnumerable<string> strings, bool clearBeforePrint)
        {
            if (clearBeforePrint)
            {
                System.Console.Clear();
            }

            if (_screenAlignment.IsCenteringVertically())
            {
                ClearScreenAndSkipLines(strings.Count());
            }

            if (_borderStyle.IsScreenBorderEnabled())
            {
                strings = strings.InsideBorder(strings.Max(s => s.Length), _screenAlignment.IsCenteringHorizontally());
            }

            return _screenAlignment.IsCenteringHorizontally() ? strings.PadToCenter(_screenWidth) : strings;
        }

        /// <summary>
        /// Clear the screen using Console.Clear, and then use the numberOfStringsToPrint to calculate how many
        /// lines must be skipped so that the text is centered vertically. Then print the empty lines.
        /// </summary>
        /// <param name="numberOfStringsToPrint"></param>
        private static void ClearScreenAndSkipLines(int numberOfStringsToPrint)
        {
            System.Console.Clear();

            var numberOfLinesToSkip = (System.Console.WindowHeight - numberOfStringsToPrint) / 2;

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
                        if (c != ' ' && c != '_' && c != '|')
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

        /// <summary>
        /// Apply Border/Center if Enabled. Apply Arrows to the menuOption at the selectedIndex.
        /// </summary>
        /// <param name="menuOptions"></param>
        /// <param name="selectedIndex"></param>
        /// <param name="menuBorderSize"></param>
        /// <returns></returns>
        IEnumerable<string> PrepMenuOptions(IEnumerable<string> menuOptions, int selectedIndex, int menuBorderSize)
        {
            menuOptions = menuOptions.ApplyArrows(selectedIndex, _menuArrowPosition);

            if (_borderStyle.IsMenuBorderEnabled())
            {
                menuOptions = menuOptions.InsideBorder(menuBorderSize, _borderStyle.IsMenuCenteringEnabled());
            }

            return menuOptions;
        }

        #endregion
    }
}
