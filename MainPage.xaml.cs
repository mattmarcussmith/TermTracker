using TermTracker.Data;
using TermTracker.Models;
using TermTracker.Pages;
using System;
using System.IO;
using Plugin.LocalNotification;
using Microsoft.Maui.Controls.Compatibility;
using TermTracker.Pages.Terms;

namespace TermTracker
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDbContext _context;

       public MainPage()
        {
            InitializeComponent();
            _context = new AppDbContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TermTracker.db3"));
            SeedDatabase();
        }
        public MainPage(AppDbContext context)
        {
            InitializeComponent();
           
            _context = context;

            SeedDatabase();
        }
        private async void SeedDatabase()
        {
            await SeedData.Initialize(_context);
        }
        private async void OnAddTermClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(termName.Text))
            {
                await DisplayAlert("Error", "Please enter a term title and select start and end dates.", "OK");
                return;
            }

            var term = new Term
            {
                Title = termName.Text,
                StartDate = startDate.Date,
                EndDate = endDate.Date
            };
            await _context.AddTermAsync(term);
            await DisplayAlert("Success", "Term added", "OK");
          
        }

        private async void OnTermsListClicked(object sender, EventArgs e)
        {
        
            await Navigation.PushAsync(new TermsListPage(_context));
        }

     
    }
}
