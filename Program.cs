namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person(1, "Eirik");
        person.InitDatabase();
        System.Console.WriteLine(person.Name);
    }
}
