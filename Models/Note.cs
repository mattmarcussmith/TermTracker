using SQLite;

namespace TermTracker.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteID { get; set; }
        public int CourseID { get; set; }
        public int AssessmentID { get; set; }
        public string Content { get; set; }
    }
}
