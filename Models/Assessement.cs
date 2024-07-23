using SQLite;

namespace TermTracker.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentID { get; set; }
        public int CourseID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
    }
}
