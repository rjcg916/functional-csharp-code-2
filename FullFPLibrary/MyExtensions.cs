using LanguageExt;

namespace MyExtensions
{

    public static class EnumExtensions
    {
        public static Option<T> MyParse<T>(this Enum @enum, string s) where T : struct
        {
            // match string in enum
            return Enum.TryParse<T>(s, out T v) ? new Some<T>(v) : Option<T>.None;
        }
    }

    public static class EnumerableExtensions
    {
        // IEnumerable<T> -> (T -> Bool) -> Some<T> 
        public static Option<T> Lookup<T>(this IEnumerable<T> values, Func<T, Boolean> pred)
        {
            return values.Find<T>(pred);
        }
    }
}