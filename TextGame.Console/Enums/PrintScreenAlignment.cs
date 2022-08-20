using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Console.Enums
{
    internal enum PrintScreenAlignment
    {
        Normal,
        Centered,
        CenteredHorizontally,
        CenteredVertically
    }

    internal static class PrintScreenAlignmentExtension
    {
        internal static bool IsCenteringVertically(this PrintScreenAlignment alignment) =>
            alignment == PrintScreenAlignment.Centered || alignment == PrintScreenAlignment.CenteredVertically;

        internal static bool IsCenteringHorizontally(this PrintScreenAlignment alignment) =>
            alignment == PrintScreenAlignment.Centered || alignment == PrintScreenAlignment.CenteredHorizontally;
    }
}
