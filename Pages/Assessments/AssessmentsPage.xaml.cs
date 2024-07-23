using TermTracker.Data;
using TermTracker.Models;
using System.Collections.Generic;
using TermTracker.Pages.AssessmentNotes;

namespace TermTracker.Pages.Assessments
{
    public partial class AssessmentsPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Course _course;

        public AssessmentsPage(AppDbContext context, Course course)
        {
            InitializeComponent();
            _context = context;
            _course = course;
            BindingContext = course;
   
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadAssessments();
        }
        private async void LoadAssessments()
        {
            var assessments =  await _context.GetAssessmentsByCourseIdAsync(_course.CourseID);
            assessmentsListView.ItemsSource = null;
            if (assessments.Count == 0)
            {
                await DisplayAlert("No Assessments", "There are no assessments. Please add an assessment.", "OK");
                
            }
            else
            {
                assessmentsListView.ItemsSource = assessments;
            }
        
        }

        private async void OnAssessmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Assessment selectedAssessment)
            {
                await Navigation.PushAsync(new EditAssessmentPage(_context, selectedAssessment));
            }
        }

        private async void OnAddAssessmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAssessmentPage(_context, _course));
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var assessment = button?.CommandParameter as Assessment;
            if (assessment != null)
            {
                await Navigation.PushAsync(new EditAssessmentPage(_context, assessment));
            }
        }
        private async void OnNotesClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var assessment = button?.CommandParameter as Assessment;
            if (assessment != null)
            {
                await Navigation.PushAsync(new AssessmentNotesListPage(_context, assessment));
            }
        }
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var assessment = button?.CommandParameter as Assessment;
            if (assessment != null)
            {
                await _context.DeleteAssessmentAsync(assessment);
                LoadAssessments();
            }
        }
       
    }
}
