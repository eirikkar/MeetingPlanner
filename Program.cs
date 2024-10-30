namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person(1, "Eirik", "Karlsen");
        person.InitDatabase();
    }
}
