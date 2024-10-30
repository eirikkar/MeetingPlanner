namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        View view = new View();
        Person person = new Person("Eirik");
        Console.WriteLine(person.AddPerson(person));
        view.ViewPersons(person.GetAllPersons());
    }
}
