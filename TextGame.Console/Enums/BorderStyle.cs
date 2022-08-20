namespace TextGame.Console.Enums
{
    internal enum BorderStyle
    {
        None,
        MenuOnly,
        MenuOnlyCentered,
        ScreenOnly,
        MenuAndScreen,
        MenuCenteredAndScreen
    }

    internal static class BorderStyleExtensions
    {
        internal static bool IsMenuBorderEnabled(this BorderStyle style) =>
            style == BorderStyle.MenuOnly || style == BorderStyle.MenuOnlyCentered || 
            style == BorderStyle.MenuAndScreen || style == BorderStyle.MenuCenteredAndScreen;

        internal static bool IsMenuCenteringEnabled(this BorderStyle style) =>
            style == BorderStyle.MenuOnlyCentered || style == BorderStyle.MenuCenteredAndScreen;

        internal static bool IsScreenBorderEnabled(this BorderStyle style) =>
            style == BorderStyle.ScreenOnly || style == BorderStyle.MenuAndScreen || style == BorderStyle.MenuCenteredAndScreen;
    }
}
