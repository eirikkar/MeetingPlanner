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
}
