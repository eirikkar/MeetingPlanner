class View
{
    public void NewLineMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void InlineMessage(string message)
    {
        Console.Write(message);
    }

    public void ViewPersons(List<Person> persons)
    {
        foreach (Person person in persons)
        {
            NewLineMessage(person.ToString());
        }
    }
}
