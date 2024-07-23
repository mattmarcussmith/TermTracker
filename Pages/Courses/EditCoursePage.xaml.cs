using Plugin.LocalNotification;
using TermTracker.Data;
using TermTracker.Models;


namespace TermTracker.Pages.Courses
{
    public partial class EditCoursePage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Course _course;

        public EditCoursePage(AppDbContext context, Course course)
        {
            InitializeComponent();
            _context = context;
            _course = course;
            BindingContext = _course;
            courseStatusPicker.SelectedItem = _course.Status;
        }

       // Save edit course on click

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _course.Name = courseName.Text;
            _course.StartDate = startDate.Date;
            _course.EndDate = endDate.Date;
            _course.Status = courseStatusPicker.SelectedItem.ToString();
            _course.InstructorName = instructorName.Text;
            _course.InstructorPhone = instructorPhone.Text;
            _course.InstructorEmail = instructorEmail.Text;
            _course.Notes = courseNotes.Text;

      

            if (string.IsNullOrWhiteSpace(courseName.Text))
            {
                courseName.Background = Colors.Red;
        
                return;

            }
            if (courseStatusPicker.SelectedItem == null)
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
   
            await _context.UpdateCourseAsync(_course);
            ScheduleNotifications(_course);
            await DisplayAlert("Success", "Course updated", "OK");
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
