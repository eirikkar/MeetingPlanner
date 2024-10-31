using System.Globalization;
using Microsoft.Data.Sqlite;

class Meeting
{
    public int Id { get; set; }
    public string? MeetingName { get; set; }

    public Person? FirstPerson { get; set; }

    public Person? SecondPerson { get; set; }

    public DateTime? Date { get; set; }

    public static readonly CultureInfo NorwegianCulture = new CultureInfo("nb-NO");

    public Meeting(string? meetingName, Person? firstPerson, Person? secondPerson, DateTime? date)
    {
        MeetingName = meetingName;
        FirstPerson = firstPerson;
        SecondPerson = secondPerson;
        Date = date;
    }

    public Meeting() { }

    public SqliteConnection InitDatabase()
    {
        var db = new SqliteConnection("Data Source=database/meeting.sqlite");
        db.Open();
        using (var cmd = db.CreateCommand())
        {
            cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Meetings (
                                id INTEGER PRIMARY KEY,
                                meetingname TEXT NOT NULL,
                                firstperson TEXT NOT NULL, 
                                secondperson TEXT NOT NULL,
                                date TEXT NOT NULL
                            );";
            cmd.ExecuteNonQuery();
        }
        return db;
    }

    public int NewMeeting(Meeting meeting)
    {
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var sq = db.CreateCommand();
        sq.CommandText =
            "INSERT INTO Meetings (MeetingName, FirstPerson, SecondPerson, Date) VALUES (@MeetingName, @FirstPerson, @SecondPerson, @Date)";
        sq.Parameters.AddWithValue("@MeetingName", meeting.MeetingName);
        sq.Parameters.AddWithValue("@FirstPerson", meeting.FirstPerson?.Name);
        sq.Parameters.AddWithValue("@SecondPerson", meeting.SecondPerson?.Name);
        sq.Parameters.AddWithValue("@Date", meeting.Date?.ToString(NorwegianCulture));
        sq.ExecuteNonQuery();
        sq.CommandText = "SELECT last_insert_rowid()";
        int id = Convert.ToInt32(sq.ExecuteScalar());
        return id;
    }

    public List<Meeting> GetAllMeetings()
    {
        var meetings = new List<Meeting>();
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var cmd = new SqliteCommand("SELECT * FROM Meetings", db);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var meeting = new Meeting(
                reader.GetString(1),
                new Person(reader.GetString(2)),
                new Person(reader.GetString(3)),
                DateTime.Parse(reader.GetString(4), NorwegianCulture)
            )
            {
                Id = reader.GetInt32(0),
            };
            meetings.Add(meeting);
        }

        return meetings;
    }

    public int DeleteMeeting(int id)
    {
        using SqliteConnection db = InitDatabase();
        db.Open();

        using (var sq = db.CreateCommand())
        {
            sq.CommandText = "DELETE FROM Meetings WHERE Id = @Id";
            sq.Parameters.AddWithValue("@Id", id);
            sq.ExecuteNonQuery();
        }
        ReassignIds();
        return id;
    }

    public int GetMeetingCount()
    {
        using SqliteConnection db = InitDatabase();
        db.Open();
        using var sq = db.CreateCommand();
        sq.CommandText = "SELECT COUNT(*) FROM Meetings";
        int count = Convert.ToInt32(sq.ExecuteScalar());
        return count;
    }

    public void ReassignIds()
    {
        using var db = InitDatabase();
        db.Open();
        using var transaction = db.BeginTransaction();
        var cmd = new SqliteCommand("SELECT id FROM Meetings ORDER BY id", db, transaction);
        var reader = cmd.ExecuteReader();
        int newId = 1;

        while (reader.Read())
        {
            int oldId = reader.GetInt32(0);
            var updateCmd = new SqliteCommand(
                "UPDATE Meetings SET id = @newId WHERE id = @oldId",
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
