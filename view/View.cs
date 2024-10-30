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
        Console.Clear();
        foreach (Person person in persons)
        {
            NewLineMessage(person.ToString());
        }
        NewLineMessage("");
        NewLineMessage("Press any key to continue...");
        Console.ReadKey();
    }

    public void ViewMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Meeting Planner");
        Console.WriteLine("1. Add Person");
        Console.WriteLine("2. View Persons");
        Console.WriteLine("3. Add Meeting");
        Console.WriteLine("4. View Meetings");
        Console.WriteLine("5. Exit");
    }
}
