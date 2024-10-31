class View
{
    /// <summary>
    /// Prints a new line message to the console.
    /// </summary>
    /// <param name="message"></param>
    public void NewLineMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Prints a inline message to the console.
    /// </summary>
    /// <param name="message"></param>
    public void InlineMessage(string message)
    {
        Console.Write(message);
    }

    /// <summary>
    /// Shows a list of persons to the console.
    /// </summary>
    /// <param name="persons"></param>
    public void ViewPersons(List<Person> persons)
    {
        Console.Clear();
        foreach (Person person in persons)
        {
            NewLineMessage($"#{person.Id} {person.Name}");
        }
        NewLineMessage("");
    }

    /// <summary>
    /// Shows a list of Meetings to the console.
    /// </summary>
    /// <param name="meetings"></param>
    public void ViewMeetings(List<Meeting> meetings)
    {
        Console.Clear();
        foreach (Meeting meeting in meetings)
        {
            NewLineMessage(
                $"Meeting: '{meeting.MeetingName}' with {meeting.FirstPerson?.Name} and {meeting.SecondPerson?.Name} at {meeting.Date?.ToString(Meeting.NorwegianCulture)}"
            );
        }
    }

    /// <summary>
    /// Shows the main menu text.
    /// </summary>
    public void ViewMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Meeting Planner");
        Console.WriteLine("1. Person menu");
        Console.WriteLine("2. Add Meeting");
        Console.WriteLine("3. View Meetings");
        Console.WriteLine("4. Delete Meeting");
        Console.WriteLine("5. Exit");
    }

    /// <summary>
    /// Shows the person menu text.
    /// </summary>
    public void ViewPersonMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Add Person");
        Console.WriteLine("2. View Persons");
        Console.WriteLine("3. Delete Person");
        Console.WriteLine("4. Back to Main menu");
        Console.WriteLine("5. Exit");
    }
}
