using LaYumba.Functional;
using System.Collections.Specialized;

namespace Chapter06
{
    public record Employee(
        string Id,
        Option<WorkPermit> WorkPermit,
        DateTime JoinedOn,
        Option<DateTime> LeftOn);

    public record WorkPermit(
        string Number,
        DateTime Expiry
        );

    public static class MyExtensions
    {
        public static Option<string> Lookup(
            this NameValueCollection collection, 
            string key
            )
            => collection[key];
    }
    public class MyCompany
    {
        static double YearsBetween(DateTime start, DateTime end)
            => (end - start).Days / 365d;

        Option<WorkPermit> GetWorkPermit(Dictionary<string, Employee> employees, string employeeId)
            =>   employees.Lookup(employeeId).Bind(e => e.WorkPermit);
        double AverageYearsWorkedAtTheCompany(List<Employee> employees)
              => employees
            .Bind(e => e.LeftOn.Map(leftOn => YearsBetween(e.JoinedOn, leftOn)))
            .Average();
    }
}