using LaYumba.Functional;
using static LaYumba.Functional.F;



//Option<string> _ = None;

//Option<string> john = Some("bob");


//string Greet(Option<string> greetee)
//    => greetee.Match(
//        None: () => "Yo, who are you?",
//        Some: (name) => $"Hello, {name}");

//Console.WriteLine(Greet(Some("Bob")));

//Console.WriteLine(Greet(None));


static Option<int> Parse(string s)
            => int.TryParse(s, out int result)
        ? Some(result) : None;

string IntDisplay(Option<int> result)
    => result.Match(
        None : () => "Not a Number",
        Some : (value) => $"{value}");


//Console.WriteLine(IntDisplay(Parse("fred")));
//Console.WriteLine(IntDisplay(Parse("15")));

Console.WriteLine(System.Enum.Parse<DayOfWeek>("Friday"));
Console.WriteLine(System.Enum.Parse<DayOfWeek>("BobDay"));

public static class Util
{
  //  public static Func<string, Option<T>> Parse<T> (this System.Enum e)
  //      => (s) => e.TryParse(s, out T result) ? Some(result) : None;
    public static Option<T> Parse<T>(this string s) where T : struct
        => System.Enum.TryParse(s, out T t) ? Some(t) : None ;

    public static Option<T> Lookup<T>(this IEnumerable<T> list, Func<T, bool> f)
        => list.Find(f);

}



//Func<IEnumerable, Func<T, bool>> lookup;
