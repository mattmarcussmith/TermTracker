using TermTracker.Data;
using TermTracker.Models;

namespace TermTracker.Pages.CourseNotes
{
    public partial class EditCourseNotePage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Note _note;

        public EditCourseNotePage(AppDbContext context, Note note)
        {
            InitializeComponent();
            _context = context;
            _note = note;
            noteContent.Text = note.Content;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _note.Content = noteContent.Text;
            await _context.UpdateNoteAsync(_note);
            await DisplayAlert("Success", "Note updated", "OK");
            await Navigation.PopAsync();
        }
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
