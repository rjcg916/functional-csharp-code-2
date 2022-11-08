using LanguageExt;

namespace FullFPLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
  

            string Greet(Option<string> greetee)
                => greetee.Match(
                    None: () => "Yo, who are you?",
                    Some: (name) => $"Hello, {name}");

            Console.WriteLine(Greet(new Some<string>("Bob")));

            Console.WriteLine(Greet(new Option<string>()));
        }
    }
}