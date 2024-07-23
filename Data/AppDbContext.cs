using SQLite;
using TermTracker.Models;

namespace TermTracker.Data
{
    public class AppDbContext
    {
        readonly SQLiteAsyncConnection _database;

        public AppDbContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public SQLiteAsyncConnection Database => _database;

        public Task EnsureCreatedAsync()
        {
            return _database.EnsureCreatedAsync();
        }
        public async Task<List<Term>> GetAllTermsAsync()
        {
            return await _database.Table<Term>().ToListAsync();
        }

        public async Task AddTermAsync(Term term)
        {
            await _database.InsertAsync(term);
        }

        public async Task UpdateTermAsync(Term term)
        {
            await _database.UpdateAsync(term);
        }

        public async Task DeleteTermAsync(Term term)
        {
            await _database.DeleteAsync(term);
        }

        // Course Methods

        public async Task<List<Course>> GetCoursesByTermIdAsync(int termId)
        {
            return await _database.Table<Course>()
                                  .Where(c => c.TermID == termId)
                                  .ToListAsync();
        }
        public async Task AddCourseAsync(Course course)
        {
            await _database.InsertAsync(course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _database.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(Course course)
        {
            await _database.DeleteAsync(course);
        }

        // Assessment Methods

        public async Task<List<Assessment>> GetAssessmentsByCourseIdAsync(int courseId)
        {
            return await _database.Table<Assessment>()
                                  .Where(a => a.CourseID == courseId)
                                  .ToListAsync();
        }
        public async Task AddAssessmentAsync(Assessment assessment)
        {
            await _database.InsertAsync(assessment);
        }

        public async Task UpdateAssessmentAsync(Assessment assessment)
        {
            await _database.UpdateAsync(assessment);
        }

        public async Task DeleteAssessmentAsync(Assessment assessment)
        {
            await _database.DeleteAsync(assessment);
        }

        // Note Methods
        public async Task<List<Note>> GetNotesByAssessmentIdAsync(int assessmentId)
        {
            return await _database.Table<Note>()
                                  .Where(n => n.AssessmentID == assessmentId)
                                  .ToListAsync();
        }
        public async Task<List<Note>> GetNotesByCourseIdAsync(int courseId)
        {
            return await _database.Table<Note>()
                                  .Where(n => n.CourseID == courseId)
                                  .ToListAsync();

        }
        public async Task AddNoteAsync(Note note)
        {
            await _database.InsertAsync(note);
        }
        public async Task UpdateNoteAsync(Note note)
        {
            await _database.UpdateAsync(note);
        }
        public async Task DeleteNoteAsync(Note note)
        {
            await _database.DeleteAsync(note);
        }
    }
}
