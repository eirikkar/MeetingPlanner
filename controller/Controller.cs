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
            string name = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(name) || name.Length < 2 || !name.All(char.IsLetter))
            {
                _view.NewLineMessage("Name is invalid. Please enter a valid name.");
            }
            else
            {
                return name;
            }
        }
    }

    public void AddNewPerson()
    {
        Console.Clear();
        _view.NewLineMessage("Add new person");
        _view.NewLineMessage("");
        _person.AddPerson(new Person(WriteName()));
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
                    break;
                case "3":

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
