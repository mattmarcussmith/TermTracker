using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TermTracker.Models;

namespace TermTracker.Data
{
    public static class SeedData
    {
        public static async Task Initialize(AppDbContext context)
        {
            
            await SeedDatabase(context);
            
        }

        private static async Task SeedDatabase(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var terms = await context.GetAllTermsAsync();
            if (terms.Count > 0)
                return; // DB has been seeded

            // Add term
            var term = new Term
            {
                Title = "Fall 2024",
                StartDate = new DateTime(2024, 9, 1),
                EndDate = new DateTime(2024, 12, 15)
            };
            await context.AddTermAsync(term);

            // Add course
            var course = new Course
            {
                TermID = term.TermID,
                Name = "Introduction to Programming",
                StartDate = new DateTime(2024, 9, 1),
                EndDate = new DateTime(2024, 12, 15),
                Status = "Planned",
                InstructorName = "Anika Patel",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                InstructorPhone = "555-123-4567",
                Notes = "Introduction to Programming notes"
            };
            await context.AddCourseAsync(course);

            // Add assessments
            var assessment1 = new Assessment
            {
                CourseID = course.CourseID,
                Name = "Midterm Exam",
                DueDate = new DateTime(2024, 10, 15),
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2024, 10, 15),
                Type = "Objective"
            };
            await context.AddAssessmentAsync(assessment1);

            var assessment2 = new Assessment
            {
                CourseID = course.CourseID,
                Name = "Final Exam",
                DueDate = new DateTime(2024, 12, 15),
                StartDate = new DateTime(2024, 12, 1),
                EndDate = new DateTime(2024, 12, 15),
                Type = "Performance"
            };
            await context.AddAssessmentAsync(assessment2);
        }
    }
}
