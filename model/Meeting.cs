using Microsoft.Data.Sqlite;

class Meeting
{
    public int Id { get; set; }
    public string? MeetingName { get; set; }

    public Person? FirstPerson { get; set; }

    public Person? SecondPerson { get; set; }

    public Meeting(string MeetingName, Person? FirstPerson, Person? SecondPerson)
    {
        this.MeetingName = MeetingName;
        this.FirstPerson = FirstPerson;
        this.SecondPerson = SecondPerson;
    }

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
                                secondperson TEXT NOT NULL
                            );";
            cmd.ExecuteNonQuery();
        }
        return db;
    }
}
