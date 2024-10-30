class Controller
{
    private Person _person;
    private View _view;

    public Controller(Person person, View view)
    {
        _person = person;
        _view = view;
    }

    public List<Person> Persons()
    {
        return new List<Person>() { };
    }
}
