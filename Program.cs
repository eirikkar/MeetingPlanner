namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        View view = new View();
        Person person = new Person();
        Meeting meeting = new Meeting();
        Controller controller = new Controller(person, view, meeting);
        controller.MainMenu();
    }
}
