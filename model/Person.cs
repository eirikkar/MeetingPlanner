class Person
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public Person(int id, string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }
}
