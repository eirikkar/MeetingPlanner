using System.Text.RegularExpressions;

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

    /// <summary>
    /// Takes input from console and checks if the string is empty and less than 2 chars
    /// </summary>
    /// <returns>string from console</returns>
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

    /// <summary>
    /// Takes string from console and checks if the string is empty
    /// </summary>
    /// <returns>input</returns>
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

    /// <summary>
    /// Takes input from console parses it into a number, checks the person database count and returns lower number
    /// </summary>
    /// <returns>number</returns>
    public int ParsePerson()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            ReturnToMainMenu(input);

            if (!int.TryParse(input, out int num) || num < 0 || num >= _person.GetPersonCount() + 1)
            {
                _view.InlineMessage("Please enter a valid number or type r to return: ");
            }
            else
            {
                return num;
            }
        }
    }

    /// <summary>
    /// Takes input from console parses it into a number, checks the meeting database count and returns lower number
    /// </summary>
    /// <returns></returns>
    public int ParseMeeting()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            ReturnToMainMenu(input);

            if (
                !int.TryParse(input, out int num)
                || num < 0
                || num >= _meeting.GetMeetingCount() + 1
            )
            {
                _view.InlineMessage("Please enter a valid number or type r to return: ");
            }
            else
            {
                return num;
            }
        }
    }

    /// <summary>
    /// Parses datetime from console to DateTime, uses norwegian culture.
    /// </summary>
    /// <returns>DateTime</returns>
    public DateTime ParseDateTime()
    {
        DateTime dt;
        while (!DateTime.TryParse(Console.ReadLine(), Meeting.NorwegianCulture, out dt))
        {
            _view.NewLineMessage("Invalid input, please enter a valid date and time: ");
        }
        return dt;
    }

    /// <summary>
    /// Adds a new person, shows the ID of the person added.
    /// </summary>
    public void AddNewPerson()
    {
        Console.Clear();
        _view.NewLineMessage("Add new person");
        _view.NewLineMessage("");
        _view.NewLineMessage($"Person added with ID {_person.AddPerson(new Person(WriteName()))}");
        _view.NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Deletes person, shows the ID of the person deleted.
    /// </summary>
    public void DeletePerson()
    {
        Console.Clear();
        _view.NewLineMessage("Delete Person");
        _view.ViewPersons(_person.GetAllPersons());
        _view.NewLineMessage(
            "Type the number of the person you want to delete or type r to return to main menu: "
        );
        int id = _person.DeletePerson(ParsePerson());
        _view.NewLineMessage($"Person deleted with #{id}");
        _view.NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Deletes meeting, shows the ID of meeting deleted.
    /// </summary>
    public void DeleteMeeting()
    {
        Console.Clear();
        _view.NewLineMessage("Delete Meeting");
        _view.ViewMeetings(_meeting.GetAllMeetings());
        _view.NewLineMessage("");
        _view.InlineMessage(
            "Type the number of the person you want to delete or type r to return to main menu: "
        );
        int id = _meeting.DeleteMeeting(ParseMeeting());
        _view.NewLineMessage($"Meeting deleted with #{id}");
        _view.NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Parses message, checks if the input is not null or whitespace, and checks that it is just letters, numbers, space and -_
    /// </summary>
    /// <returns>input</returns>
    public string ParseMessage()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            ReturnToMainMenu(input);
            if (
                !string.IsNullOrWhiteSpace(input) && Regex.Match(input, "^[A-Za-z0-9 _-]*$").Success
            )
            {
                return input;
            }
            else
            {
                _view.InlineMessage("Only characters and digits press r to return to menu: ");
            }
        }
    }

    /// <summary>
    /// Adds a new meeting, shows the ID of the meeting created.
    /// </summary>
    public void AddNewMeeting()
    {
        Console.Clear();
        _view.NewLineMessage("Add new meeting");
        _view.NewLineMessage(
            "Press r to return to main menu or type what is the meeting is about: "
        );
        string meetingMessage = ParseMessage();

        Console.Clear();
        _view.ViewPersons(_person.GetAllPersons());
        _view.NewLineMessage("Please enter the first person in the meeting: ");
        Person? person1 = _person.GetPersonById(ParsePerson());

        Console.Clear();
        _view.ViewPersons(_person.GetAllPersons());
        _view.NewLineMessage("Please enter the second person in the meeting");
        Person? person2 = _person.GetPersonById(ParsePerson());

        Console.Clear();
        _view.NewLineMessage("Please enter the date of the meeting(dd.mm.yyyy tt:tt): ");
        DateTime dt = ParseDateTime();

        Console.Clear();
        int id = _meeting.NewMeeting(new Meeting(meetingMessage, person1, person2, dt));
        _view.NewLineMessage($"Meeting added with ID{id}");
        _view.NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Checks for input r or R and returns the user to main menu
    /// </summary>
    /// <param name="input"></param>
    public void ReturnToMainMenu(string? input)
    {
        if (!string.IsNullOrEmpty(input) && (input[0] == 'r' || input[0] == 'R'))
        {
            MainMenu();
        }
    }

    /// <summary>
    /// Person menu loop
    /// </summary>
    public void PersonMenu()
    {
        while (true)
        {
            _view.ViewPersonMenu();
            switch (Console.ReadLine())
            {
                case "1":
                    AddNewPerson();
                    break;
                case "2":
                    _view.ViewPersons(_person.GetAllPersons());
                    _view.NewLineMessage("");
                    _view.NewLineMessage("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "3":
                    DeletePerson();
                    break;
                case "4":
                    MainMenu();
                    break;
                case "5":
                    Environment.Exit(5);
                    break;
            }
        }
    }

    /// <summary>
    /// Main menu loop
    /// </summary>
    public void MainMenu()
    {
        while (true)
        {
            _view.ViewMainMenu();

            switch (Console.ReadLine())
            {
                case "1":
                    PersonMenu();
                    break;
                case "2":
                    AddNewMeeting();
                    break;
                case "3":
                    _view.ViewMeetings(_meeting.GetAllMeetings());
                    _view.NewLineMessage("");
                    _view.NewLineMessage("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "4":
                    DeleteMeeting();
                    break;
                case "5":
                    Environment.Exit(5);
                    break;
            }
        }
    }
}
