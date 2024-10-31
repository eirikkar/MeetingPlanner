using Microsoft.Data.Sqlite;

class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Person(string? name)
    {
        Name = name;
    }

    public Person() { }

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

    public Person? GetPersonById(int id)
    {
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var cmd = new SqliteCommand("SELECT * FROM Persons WHERE id = @Id", db);
        cmd.Parameters.AddWithValue("@Id", id);
        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Person(reader.GetString(1)) { Id = reader.GetInt32(0) };
        }
        else
        {
            return null;
        }
    }

    public List<Person> GetAllPersons()
    {
        var persons = new List<Person>();
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var cmd = new SqliteCommand("SELECT * FROM Persons", db);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var person = new Person(reader.GetString(1)) { Id = reader.GetInt32(0) };
            persons.Add(person);
        }

        return persons;
    }

    public int DeletePerson(int id)
    {
        using SqliteConnection db = InitDatabase();
        db.Open();

        using (var sq = db.CreateCommand())
        {
            sq.CommandText = "DELETE FROM Persons WHERE Id = @Id";
            sq.Parameters.AddWithValue("@Id", id);
            sq.ExecuteNonQuery();
        }
        ReassignIds();
        return id;
    }

    public int GetPersonCount()
    {
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var sq = db.CreateCommand();
        sq.CommandText = "SELECT COUNT(*) FROM Persons";
        int count = Convert.ToInt32(sq.ExecuteScalar());
        return count;
    }

    public void ReassignIds()
    {
        using var db = InitDatabase();
        db.Open();
        using var transaction = db.BeginTransaction();
        var cmd = new SqliteCommand("SELECT id FROM Persons ORDER BY id", db, transaction);
        var reader = cmd.ExecuteReader();
        int newId = 1;

        while (reader.Read())
        {
            int oldId = reader.GetInt32(0);
            var updateCmd = new SqliteCommand(
                "UPDATE Persons SET id = @newId WHERE id = @oldId",
                db,
                transaction
            );
            updateCmd.Parameters.AddWithValue("@newId", newId);
            updateCmd.Parameters.AddWithValue("@oldId", oldId);
            updateCmd.ExecuteNonQuery();
            newId++;
        }

        transaction.Commit();
    }
}
