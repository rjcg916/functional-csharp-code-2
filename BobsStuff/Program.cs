using LaYumba.Functional;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;



var result1 = "Bob".Balanced();

var result2 = "Bob (Graham)".Balanced();

var result3 = "Bob )Graham)".Balanced();

var result4 = "Bob (Graham(".Balanced();

var result5 = "Bob )Graham(".Balanced();

var result6 = "Bob (Graham[)]".Balanced();

var result7 = "Bob (Graham[)]]".Balanced();

var result8 = "Bob (Graham[()])".Balanced();

var result = false;

static class StringExt
{
    //    var greet = (Greeting gr, Name name) => $"{gr}, {name}";

    //    var greetWith = (Greeting gr) => (Name name) => $"{gr}, {name}";

    //   private static Func<char[],bool> BalancedParens = (s) 
    //       => Balanced_("", ('(', ')'), s);

    //  var balancedChars = (string stack) => (char open, char close) delim
    //                          => (char[] charsToCheck) 
    //                          => Balanced_(stack, delim, charsToCheck);

    static char[] openers = new char[] { '(', '[' };
    static char[] closers = new char[] { ')', ']' };

    static Func<char, int> getCharType = (char c) => openers.Contains(c) ? 1 : (  closers.Contains(c) ? -1 : 0);
    static Func<char, char> getClosingChar = (char c) => closers.ElementAt(Array.IndexOf(openers, c));

    private static bool Balanced__(Func<char, int> getCharType, Func<char, char> getClosingChar,  string stack, char[] charsToCheck)
    {
        if (charsToCheck.Length == 0) return stack.Length == 0;

        var newStack = stack;
        var charToCheck = charsToCheck[0];

        if (getCharType(charToCheck) == 1)
        {  //push
            newStack = getClosingChar( charToCheck) + stack;
        }
        else if (getCharType(charToCheck) == -1)
        {   // pop
            if (stack.Length == 0) return false; //mismatch 
            if (charToCheck != stack[0]) return false; //improper nesting
            newStack = stack[1..];
        }

        return Balanced__(getCharType, getClosingChar, newStack, charsToCheck.Skip(1).ToArray());
    }

    private static bool Balanced_((char open, char close) delim, string stack, char[] charsToCheck) {

        if (charsToCheck.Length == 0) return stack.Length == 0;

        var newStack = stack;
        var charToCheck = charsToCheck[0];

        if (charToCheck == delim.open) {  //push
            newStack = delim.close + stack;
        } else if (charToCheck == delim.close) {   // pop
            if (stack.Length == 0) return false; //mismatch 
            newStack = stack[1..];
        }
            
        return Balanced_(delim, newStack, charsToCheck.Skip(1).ToArray());
    }

   public static bool Balanced(this string s)
    {
        if (s == null) return true;
        //      var BalancedParens = balancedChars("", ('(',')'));
        //      return BalancedParens(s);
        // return Balanced_(('(', ')'), "", s.ToArray());
        return Balanced__(getCharType, getClosingChar, "", s.ToArray());
    }
}

static class Chapter07
{
    public static Func<T,R> Compose<T,R,X> (this Func<X,R> f1, Func<T,X> f2)
       => x => f1(f2(x));
}

static class MyMap
{
    // ISet<T> -> (T -> R) -> ISet<R>
    public static ISet<R> Map<T, R>
        (this ISet<T> ts, Func<T, R> f)
    {
        var rs = new HashSet<R>();
        foreach (var t in ts)
            rs.Add(f(t));
        return rs;
    }

    // IDictionary<K,T> -> ((K,T) -> (K,R)) -> IDictionary<K,R> 
    public static IDictionary<K, R> Map<K, T,R> (
        this IDictionary<K, T> dict, Func<T,R> f)
    {
        var rs = new Dictionary<K, R>();
        foreach (var pair in dict)
            rs[pair.Key] = f(pair.Value);
        return rs;
    }

    public static IEnumerable<R> Map<T, R>
        (this IEnumerable<T> ts, Func<T, R> f)
        => ts.Select(f);

}


//var range = Enumerable.Range(1, 10);

//Predicate<int> greaterThan5 = i => i > 5;

//Predicate<int> lessThanOrEqual5 =  greaterThan5.Negate<int>();

//var subSet = range.Where(i => greaterThan5(i));

//var subSet2 = range.Where( i => lessThanOrEqual5(i));

//var result = subSet;

//int[] sortMe = new int[] { 3, 10, 9, 7, 6 };



// set pivot
// move to first less than pivot
// move to first greate than pivot

//public static class Chapter02
//{
//      public static List<T> MySort<T>(this List<T> list, Comparison<T> c) { 
//        list.Sort(c); 
//        return list; 
//      }
   
  
//      public static Predicate<T> Negate<T>(this Predicate<T> pred )
//        => t => !pred(t);

 //   public static Func<T, bool> Negate<T>(this Func<T, bool> pred)
 //       => t => !pred(t);



//Option<string> _ = None;

//Option<string> john = Some("bob");


//string Greet(Option<string> greetee)
//    => greetee.Match(
//        None: () => "Yo, who are you?",
//        Some: (name) => $"Hello, {name}");

//Console.WriteLine(Greet(Some("Bob")));

//Console.WriteLine(Greet(None));


//static Option<int> Parse(string s)
//            => int.TryParse(s, out int result)
//        ? Some(result) : None;

//string IntDisplay(Option<int> result)
//    => result.Match(
//        None : () => "Not a Number",
//        Some : (value) => $"{value}");


//Console.WriteLine(IntDisplay(Parse("fred")));
//Console.WriteLine(IntDisplay(Parse("15")));

//Console.WriteLine(System.Enum.Parse<DayOfWeek>("Friday"));
//Console.WriteLine(System.Enum.Parse<DayOfWeek>("BobDay"));

//public static class Util
//{
  //  public static Func<string, Option<T>> Parse<T> (this System.Enum e)
  //      => (s) => e.TryParse(s, out T result) ? Some(result) : None;
//    public static Option<T> Parse<T>(this string s) where T : struct
//        => System.Enum.TryParse(s, out T t) ? Some(t) : None ;

//    public static Option<T> Lookup<T>(this IEnumerable<T> list, Func<T, bool> f)
//        => list.Find(f);

//}



//Func<IEnumerable, Func<T, bool>> lookup;
//
//
// 