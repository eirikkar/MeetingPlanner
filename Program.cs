namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person(1, "Eirik", DateTime.Now);
        person.InitDatabase();
        System.Console.WriteLine(person.MeetTime);
    }
}
