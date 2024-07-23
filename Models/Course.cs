﻿using SQLite;

namespace TermTracker.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseID { get; set; }
        public int TermID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
    }
}

