class View
{
    public string? WriteName()
    {
        while (true)
        {
            Console.Write("Enter name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || name.Length < 2 || !name.All(char.IsLetter))
            {
                Console.WriteLine("Name is invalid. Please enter a valid name.");
            }
            else
            {
                return name;
            }
        }
    }
}
