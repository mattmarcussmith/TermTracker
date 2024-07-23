using TermTracker.Data;
using TermTracker.Models;
using Plugin.LocalNotification;
using System;


namespace TermTracker.Pages.Assessments
{
    public partial class AddAssessmentPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Course _course;

        public AddAssessmentPage(AppDbContext context, Course course)
        {
            InitializeComponent();
            _context = context;
            _course = course;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var assessment = new Assessment
            {
                Name = assessmentName.Text,
                DueDate = dueDate.Date,
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                Type = assessmentType.SelectedItem.ToString(),
                CourseID = _course.CourseID
            };

            await _context.AddAssessmentAsync(assessment);
            ScheduleNotifications(assessment);
            await DisplayAlert("Success", "Assessment added", "OK");
            await Navigation.PopAsync();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ScheduleNotifications(Assessment assessment)
        {
            if (dueDateNotificationSwitch.IsToggled)
            {
                var startNotification = new NotificationRequest
                {
                    NotificationId = assessment.AssessmentID,
                    Title = $"Assessment Alert: {assessment.Name}",
                    Description = $"Your asssment {assessment.Name} is Due on {assessment.DueDate.ToShortDateString()}",
                    ReturningData = "Dummy data",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = assessment.DueDate
                    },

                };
                LocalNotificationCenter.Current.Show(startNotification);
            }
            if (startNotificationSwitch.IsToggled)
            {
                var startNotification = new NotificationRequest
                {
                    NotificationId = assessment.AssessmentID,
                    Title = $"Assessment Starting: {assessment.Name}",
                    Description = $"Your assessment {assessment.Name} is starting on {assessment.StartDate.ToShortDateString()}",
                    ReturningData = "Dummy data",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = assessment.StartDate
                    },

                };
                LocalNotificationCenter.Current.Show(startNotification);
            }

            if (endNotificationSwitch.IsToggled)
            {
                var endNotification = new NotificationRequest
                {
                    NotificationId = assessment.AssessmentID + 1,
                    Title = $"Course Ending: {assessment.Name}",
                    Description = $"Your course {assessment.Name} is ending on {assessment.EndDate.ToShortDateString()}",
                    ReturningData = "Dummy data",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = assessment.EndDate
                    },

                };
                LocalNotificationCenter.Current.Show(endNotification);
            }

        }


    }
}
