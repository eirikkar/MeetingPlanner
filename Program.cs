namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        Person person1 = new Person("Eirik");
        Person person2 = new Person("Victoria");
        Meeting meeting = new Meeting("Møte mellom", person1, person2);
        Console.WriteLine(meeting.MeetingName);
    }
}
