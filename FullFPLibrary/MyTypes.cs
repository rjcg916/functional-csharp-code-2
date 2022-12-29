using LanguageExt;
using System.Net.Mail;

namespace MyTypes
{
    public struct Email
    {
        public string Value { get; }
        public static Option<Email> Create(string email)
            => IsValid(email) ? new Some<Email>(new Email(email)) : Option<Email>.None;
        private Email(string value)
            => Value = value;
        private static bool IsValid(string email)
            => MailAddress.TryCreate(email, out MailAddress _);
        public static string Address(Option<Email> e)
              => e.Match(None: () => "Missing E-mail", Some: (e) => e);

        public static implicit operator string(Email e)
            =>  e.Value;
    }
}
