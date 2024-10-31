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
        try
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
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }

    public int AddPerson(Person person)
    {
        try
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
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }

    public Person? GetPersonById(int id)
    {
        try
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
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }

    public List<Person> GetAllPersons()
    {
        try
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
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }

    public int DeletePerson(int id)
    {
        try
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
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }

    public int GetPersonCount()
    {
        try
        {
            using SqliteConnection db = InitDatabase();
            db.Open();
            using var sq = db.CreateCommand();
            sq.CommandText = "SELECT COUNT(*) FROM Persons";
            int count = Convert.ToInt32(sq.ExecuteScalar());
            return count;
        }
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }

    public void ReassignIds()
    {
        try
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
        catch (SqliteException e)
        {
            Console.WriteLine($"Database error: {e.Message}");
            throw;
        }
    }
}
