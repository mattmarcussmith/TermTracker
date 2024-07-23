using Plugin.LocalNotification;
using TermTracker.Data;
using TermTracker.Models;

namespace TermTracker.Pages.AssessmentNotes
{
    public partial class AddAssessmentNotePage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Assessment _assessment;

        public AddAssessmentNotePage(AppDbContext context, Assessment assessment)
        {
            InitializeComponent();
            _context = context;
            _assessment = assessment;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var note = new Note
            {
                Content = noteContent.Text,
                AssessmentID = _assessment.AssessmentID
            };
            
            if(string.IsNullOrWhiteSpace(note.Content))
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
