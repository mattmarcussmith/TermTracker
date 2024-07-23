using TermTracker.Data;
using TermTracker.Models;
using Plugin.LocalNotification;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Compatibility;
using TermTracker.Pages.Courses;

namespace TermTracker.Pages.Terms
{
    public partial class TermsListPage : ContentPage
    {
        private readonly AppDbContext _context;

        public TermsListPage(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadTerms();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTerms();
        }

        private async void LoadTerms()
        {
            var terms = await _context.GetAllTermsAsync();
            termsListView.ItemsSource = null;
            if(terms.Count == 0) 
             {
                await DisplayAlert("No Terms", "There are no terms. Please add a term.", "OK");
              
             }
            termsListView.ItemsSource = terms;

        }
        private async void OnTermSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Term selectedTerm)
            {
                await Navigation.PushAsync(new EditTermPage(_context, selectedTerm));
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Term term)
            {
                await Navigation.PushAsync(new EditTermPage(_context, term));
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Term term)
            {
                await _context.DeleteTermAsync(term);
                LoadTerms();
                await DisplayAlert("Success", "Term deleted", "OK");
      
            } 
           
        }

        private async void OnClassesClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Term term)
            {
                await Navigation.PushAsync(new CoursesListPage(_context, term));
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

     
    }
}

