using Microsoft.Data.Sqlite;

class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }

    public SqliteConnection InitDatabase()
    {
        var db = new SqliteConnection("Data Source=database/person.sqlite");
        db.Open();
        using (var cmd = db.CreateCommand())
        {
            cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Persons (
                                id INTEGER PRIMARY KEY,
                                name TEXT NOT NULL
                            );";
            cmd.ExecuteNonQuery();
        }
        return db;
    }

    public int AddPerson(Person person)
    {
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var sq = db.CreateCommand();
        sq.CommandText = "INSERT INTO Persons (Name) VALUES (@Name)";
        sq.Parameters.AddWithValue("@Name", person.Name);
        sq.ExecuteNonQuery();
        sq.CommandText = "SELECT last_insert_rowid()";
        int id = Convert.ToInt32(sq.ExecuteScalar());
        return id;
    }
}
