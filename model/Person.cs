using Microsoft.Data.Sqlite;

class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public DateTime MeetTime { get; set; }

    public Person(int id, string name, DateTime meetTime)
    {
        Name = name;
        Id = id;
        MeetTime = meetTime;
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
                                name TEXT NOT NULL,
                                datetime TEXT NOT NULL
                            );";
            cmd.ExecuteNonQuery();
        }
        return db;
    }
}
