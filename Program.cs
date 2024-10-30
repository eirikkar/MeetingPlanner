namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        Meeting meeting = new Meeting();
        Person person = new Person("Eirik");
        person.InitDatabase();
        meeting.InitDatabase();
        System.Console.WriteLine(person.Name);
    }
}
