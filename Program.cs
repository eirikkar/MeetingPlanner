namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person("Eirik");
        Console.WriteLine(person.AddPerson(person));
    }
}
