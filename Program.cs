namespace MeetingPlanner;

class Program
{
    static void Main(string[] args)
    {
        View view = new View();
        Person person = new Person("Eirik");
        Person person2 = new Person("Victoria");
        Meeting meeting = new Meeting("Date", person, person2);
        Controller controller = new Controller(person, view, meeting);
        meeting.NewMeeting(meeting);
        controller.MainMenu();
    }
}
