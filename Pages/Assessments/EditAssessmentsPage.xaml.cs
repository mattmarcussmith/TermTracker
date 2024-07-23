using TermTracker.Data;
using TermTracker.Models;
using Plugin.LocalNotification;
using System;

namespace TermTracker.Pages.Assessments
{
    public partial class EditAssessmentPage : ContentPage
    {
        private readonly AppDbContext _context;
        private readonly Assessment _assessment;

        public EditAssessmentPage(AppDbContext context, Assessment assessment)
        {
            InitializeComponent();
            _context = context;
            _assessment = assessment;
            
            BindingContext = assessment;
            assessmentName.Text = assessment.Name;
            dueDate.Date = assessment.DueDate;
            endDate.Date = assessment.EndDate;
            startDate.Date = assessment.StartDate;
            assessmentType.SelectedItem = assessment.Type;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _assessment.Name = assessmentName.Text;
            _assessment.DueDate = dueDate.Date;
            _assessment.StartDate = startDate.Date;
            _assessment.EndDate = endDate.Date;
            _assessment.Type = assessmentType.SelectedItem.ToString();
      

            await _context.UpdateAssessmentAsync(_assessment);
            ScheduleNotifications(_assessment);
            await DisplayAlert("Success", "Assessment updated", "OK");
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
