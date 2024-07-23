using TermTracker.Data;
using TermTracker.Models;

using Microsoft.Maui.Controls;

namespace TermTracker.Pages.CourseNotes
{
    public partial class CourseNotesListPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Course _course;


        public CourseNotesListPage(AppDbContext context, Course course)
        {

            InitializeComponent();
            _context = context;
            _course = course;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadNotesByCourse();
        }

        private async void LoadNotesByCourse()
        {
            var notes = await _context.GetNotesByCourseIdAsync(_course.CourseID);
            notesListView.ItemsSource = null;
            if (notes.Count == 0)
            {
                await DisplayAlert("No Notes", "There are no notes for this Course. Please add a note.", "OK");
            }
            else
            {
                notesListView.ItemsSource = notes;
            }
        }

        private async void OnNoteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Note selectedNote)
            {
                await Navigation.PushAsync(new EditCourseNotePage(_context, selectedNote));
            }
        }

        private async void OnAddNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCourseNotePage(_context, _course));
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.CommandParameter as Note;
            if (note != null)
            {
                await Navigation.PushAsync(new EditCourseNotePage(_context, note));
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.CommandParameter as Note;
            if (note != null)
            {
                await _context.DeleteNoteAsync(note);
                LoadNotesByCourse();
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.CommandParameter as Note;
            if (note != null)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Title = "Share Note",
                    Text = note.Content,
                    Subject = "Note for Course: " + _course.Name
                });
            }
        }
    }
}
