using TermTracker.Data;
using TermTracker.Models;
using Plugin.LocalNotification;

namespace TermTracker.Pages.Terms
{
    public partial class EditTermPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Term _term;

        public EditTermPage(AppDbContext context, Term term)
        {
            InitializeComponent();
            _context = context;
            _term = term;
            BindingContext = _term;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _term.Title = termName.Text;
            _term.StartDate = startDate.Date;
            _term.EndDate = endDate.Date;
            await _context.UpdateTermAsync(_term);
            await DisplayAlert("Success", "Term updated", "OK");
         
        }
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}

