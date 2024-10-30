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

    public string? WriteName()
    {
        while (true)
        {
            _view.InlineMessage("Enter name: ");
            string? name = Console.ReadLine();
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
}
