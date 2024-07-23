using TermTracker.Data;
using TermTracker.Models;
using Plugin.LocalNotification;


namespace TermTracker.Pages.Courses
{
    public partial class AddCoursePage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Term _term;

        public AddCoursePage(AppDbContext context, Term term)
        {
            InitializeComponent();
            _context = context;
            _term = term;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
   

            courseName.BackgroundColor = Colors.White;
            courseStatusPicker.BackgroundColor = Colors.White;
            instructorName.BackgroundColor = Colors.White;
            instructorPhone.BackgroundColor = Colors.White;
            instructorEmail.BackgroundColor = Colors.White;
            courseNotes.BackgroundColor = Colors.White;

            if(string.IsNullOrWhiteSpace(courseName.Text))
            {
                courseName.Background = Colors.Red;
         
                return;
             
            }
            if(courseStatusPicker.SelectedItem == null)
            {
                courseStatusPicker.Background = Colors.Red;
                return;
               
            }
            if (string.IsNullOrWhiteSpace(instructorPhone.Text))
            {
                instructorPhone.BackgroundColor = Colors.Red;
              
                return;
            }
            if (string.IsNullOrWhiteSpace(instructorEmail.Text))
            {
                instructorEmail.BackgroundColor = Colors.Red;
        
                return;
            }
            if (string.IsNullOrWhiteSpace(instructorName.Text))
            {
                instructorName.BackgroundColor = Colors.Red;
        
                return;
            }
         


            var course = new Course
            {
                Name = courseName.Text,
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                Status = courseStatusPicker.SelectedItem.ToString(),
                InstructorName = instructorName.Text,
                InstructorPhone = instructorPhone.Text,
                InstructorEmail = instructorEmail.Text,
                Notes = courseNotes.Text,
                TermID = _term.TermID
            };

          

            await _context.AddCourseAsync(course);
            ScheduleNotifications(course);
            await DisplayAlert("Success", "Course added", "OK");
            await Navigation.PopAsync();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void ScheduleNotifications(Course course)
        {
            if (startNotificationSwitch.IsToggled)
            {
                var startNotification = new NotificationRequest
                {
                    NotificationId = course.CourseID,
                    Title = $"Course Starting: {course.Name}",
                    Description = $"Your course {course.Name} is starting on {course.StartDate.ToShortDateString()}",
                    ReturningData = "Dummy data",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = course.StartDate
                    },
              
                };
                LocalNotificationCenter.Current.Show(startNotification);
            }

            if (endNotificationSwitch.IsToggled)
            {
                var endNotification = new NotificationRequest
                {
                    NotificationId = course.CourseID + 1,
                    Title = $"Course Ending: {course.Name}",
                    Description = $"Your course {course.Name} is ending on {course.EndDate.ToShortDateString()}",
                    ReturningData = "Dummy data",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = course.EndDate
                    },
            
                };
                LocalNotificationCenter.Current.Show(endNotification);
            }
        }
    }
}
