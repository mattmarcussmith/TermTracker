
using TermTracker.Data;
using TermTracker.Models;

namespace TermTracker.Pages.CourseNotes
{
    public partial class AddCourseNotePage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Course _course;

        public AddCourseNotePage(AppDbContext context, Course course)
        {
            InitializeComponent();
            _context = context;
            _course = course;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var note = new Note
            {
                Content = noteContent.Text,
                CourseID = _course.CourseID
            };

            if (string.IsNullOrWhiteSpace(note.Content))
            {
                await DisplayAlert("Error", "Note content is required", "OK");
                return;
            }

            await _context.AddNoteAsync(note);
            await DisplayAlert("Success", "Note added", "OK");
            await Navigation.PopAsync();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
