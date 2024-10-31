namespace MeetingPlaner;

class Program
{
    static void Main(string[] args)
    {
        // New view, person, meeting and controller object.
        View view = new View();
        Person person = new Person();
        Meeting meeting = new Meeting();
        Controller controller = new Controller(person, view, meeting);
        // Starts the main menu
        controller.MainMenu();
    }
}
