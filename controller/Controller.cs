using System.Globalization;

class Controller
{
    private Person _person;
    private View _view;
    private Meeting _meeting;

    public Controller(Person person, View view, Meeting meeting)
    {
        _person = person;
        _view = view;
        _meeting = meeting;
    }

    public List<Person> Persons()
    {
        return new List<Person>() { };
    }

    public List<Meeting> Meetings()
    {
        return new List<Meeting>() { };
    }

    public string WriteName()
    {
        while (true)
        {
            _view.InlineMessage("Enter name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || !name.All(char.IsLetter))
            {
                _view.NewLineMessage("Name is invalid. Please enter a valid name.");
            }
            else
            {
                return name;
            }
        }
    }

    public string ParseString()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                _view.NewLineMessage("Please enter something.");
            }
            else
            {
                return input;
            }
        }
    }

    public int ParseInt()
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            _view.InlineMessage("Please enter a valid number: ");
        }
        return num;
    }

    public DateTime ParseDateTime()
    {
        DateTime dt;
        while (
            !DateTime.TryParse(
                Console.ReadLine(),
                CultureInfo.CreateSpecificCulture("no-NO"),
                out dt
            )
        )
        {
            _view.NewLineMessage("Invalid input, please enter a valid date and time: ");
        }
        return dt;
    }

    public void AddNewPerson()
    {
        Console.Clear();
        _view.NewLineMessage("Add new person");
        _view.NewLineMessage("");
        _view.NewLineMessage($"Person added with ID {_person.AddPerson(new Person(WriteName()))}");
        _view.NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public void AddNewMeeting()
    {
        Console.Clear();
        _view.NewLineMessage("Add new meeting");
        _view.NewLineMessage("What is the meeting about: ");
        string? meetingMessage = Console.ReadLine();

        Console.Clear();
        _view.ViewPersons(_person.GetAllPersons());
        _view.NewLineMessage("Please enter the first person in the meeting: ");
        Person? person1 = _person.GetPersonById(ParseInt());

        Console.Clear();
        _view.ViewPersons(_person.GetAllPersons());
        _view.NewLineMessage("Please enter the second person in the meeting");
        Person? person2 = _person.GetPersonById(ParseInt());

        Console.Clear();
        _view.NewLineMessage("Please enter the date of the meeting(dd.mm.yyyy tt:tt): ");
        DateTime dt = ParseDateTime();

        Console.Clear();
        int id = _meeting.NewMeeting(new Meeting(meetingMessage, person1, person2, dt));
        _view.NewLineMessage($"Meeting added with ID{id}");
        _view.NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public void MainMenu()
    {
        bool exit = false;
        while (!exit)
        {
            _view.ViewMainMenu();

            switch (Console.ReadLine())
            {
                case "1":
                    AddNewPerson();
                    break;
                case "2":
                    _view.ViewPersons(_person.GetAllPersons());
                    _view.NewLineMessage("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "3":
                    AddNewMeeting();
                    break;
                case "4":
                    _view.ViewMeetings(_meeting.GetAllMeetings());
                    break;
                case "5":
                    exit = true;
                    break;
            }
        }
    }
}
