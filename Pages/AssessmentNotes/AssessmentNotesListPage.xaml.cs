using TermTracker.Data;
using TermTracker.Models;

using Microsoft.Maui.Controls;

namespace TermTracker.Pages.AssessmentNotes
{
    public partial class AssessmentNotesListPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Assessment _assessment;


        public AssessmentNotesListPage(AppDbContext context, Assessment assessment)
        {

            InitializeComponent();
            _context = context;
            _assessment = assessment;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadNotesByAssessment();
        }

        private async void LoadNotesByAssessment()
        {
            var notes = await _context.GetNotesByAssessmentIdAsync(_assessment.AssessmentID);
            notesListView.ItemsSource = null;
            if (notes.Count == 0)
            {
                await DisplayAlert("No Notes", "There are no notes for this assessment. Please add a note.", "OK");
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
                await Navigation.PushAsync(new EditAssessmentNotePage(_context, selectedNote));
            }
        }

        private async void OnAddNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessmentNotePage(_context, _assessment));
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.CommandParameter as Note;
            if (note != null)
            {
                await Navigation.PushAsync(new EditAssessmentNotePage(_context, note));
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var note = button?.CommandParameter as Note;
            if (note != null)
            {
                await _context.DeleteNoteAsync(note);
                LoadNotesByAssessment();
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
                    Subject = "Note for Assessment: " + _assessment.Name
                });
            }
        }
    }
}
