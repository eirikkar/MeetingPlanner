using System.Globalization;

namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        View view = new View();
        Person person = new Person();
        Meeting meeting = new Meeting();
        Controller controller = new Controller(person, view, meeting);
        //controller.MainMenu();
        var cultureInfo = new CultureInfo("no-NO");
        string? dateString = Console.ReadLine();
        var dateTime = DateTime.Parse(dateString ?? "", cultureInfo);
        Console.WriteLine(dateTime.ToString(cultureInfo));
    }
}
