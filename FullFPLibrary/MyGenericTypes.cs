using LaYumba.Functional;
using System.Text.RegularExpressions;

namespace MyGenericTypes
{
    using static F;
    public class Email
    {
        static readonly Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private string Value { get; }

        private Email(string value) => Value = value;

        public static Option<Email> Create(string s)
           => regex.IsMatch(s)
              ? Some(new Email(s))
              : None;
        public static string Address(Option<Email> e)
            => e.Match(None: () => "Missing E-mail", Some: (e) => e);

        public static implicit operator string(Email e)
           => e.Value;
    }
}
