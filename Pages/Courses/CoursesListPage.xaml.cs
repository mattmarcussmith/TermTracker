using TermTracker.Data;
using TermTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TermTracker.Pages.Assessments;
using TermTracker.Pages.CourseNotes;

namespace TermTracker.Pages.Courses
{
    public partial class CoursesListPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Term _term;

        public CoursesListPage(AppDbContext context, Term term)
        {
            InitializeComponent();
            _context = context;
            _term = term;
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCourses();
        }

        private async void LoadCourses()
        {
            var courses = await _context.GetCoursesByTermIdAsync(_term.TermID);
            coursesListView.ItemsSource = null;
            if (courses.Count == 0)
            {
                await DisplayAlert("No Courses", "There are no courses in this term. Please add a course.", "OK");
            }
            else
            {
                coursesListView.ItemsSource = courses;
            }
        }

        private async void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Course selectedCourse)
            {
                await Navigation.PushAsync(new EditCoursePage(_context, selectedCourse));
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Course course)
            {
                await Navigation.PushAsync(new EditCoursePage(_context, course));
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Course course)
            {
                await _context.DeleteCourseAsync(course);
                LoadCourses();
            }
        }
        private async void OnNotesClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var course = button?.CommandParameter as Course;
            if (course != null)
            {
                await Navigation.PushAsync(new CourseNotesListPage(_context, course));
            }
        }
        private async void OnAddCourseClicked(object sender, EventArgs e)
        {
         
            await Navigation.PushAsync(new AddCoursePage(_context, _term));
        }
          private async void OnAssessmentsClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var course = button?.CommandParameter as Course;
            if (course != null)
            {
                await Navigation.PushAsync(new AssessmentsPage(_context, course));
            }
        }
     
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
