using SQLite;

namespace TermTracker.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int TermID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

