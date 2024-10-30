using Microsoft.Data.Sqlite;

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

    public SqliteConnection InitDatabase()
    {
        var db = new SqliteConnection("Data Source=database/db.sqlite");
        db.Open();
        using (var cmd = db.CreateCommand())
        {
            cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Contacts (
                                id INTEGER PRIMARY KEY,
                                firstname TEXT NOT NULL,
                                lastname TEXT NOT NULL
                            );";
            cmd.ExecuteNonQuery();
        }
        return db;
    }
}
