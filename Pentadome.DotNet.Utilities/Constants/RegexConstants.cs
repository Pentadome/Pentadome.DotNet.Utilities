using System.Text.RegularExpressions;

namespace Pentadome.DotNet.Utilities
{
    public static class RegexConstants
    {
        public static Regex Digit { get; } = new Regex("[0-9]");
        public static Regex LowerCase { get; } = new Regex(@"\p{Ll}");
        public static Regex UpperCase { get; } = new Regex(@"\p{Lu}");
        public static Regex SpecialCharacter { get; } = new Regex(@"\W", RegexOptions.ECMAScript);
        public static Regex WildCard { get; } = new Regex("^[*]+$");
        public static Regex WhiteSpace { get; } = new Regex(@"\s");
    }
}
