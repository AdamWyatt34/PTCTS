using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTCTS.Models;
using PTCTS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditClientWorkout : ContentPage
    {
        ClientWorkoutsViewModel cClientWorkouts;
        public EditClientWorkout(ClientWorkoutsViewModel clientWorkouts)
        {
            InitializeComponent();
            cClientWorkouts = clientWorkouts;
            this.Title = "Change Workout Date";
        }

        protected override void OnAppearing()
        {
            workoutName.Text = cClientWorkouts.name;
            workoutDescription.Text = cClientWorkouts.description;
            scheduledDate.Date = cClientWorkouts.scheduledDate;

            base.OnAppearing();
        }

        private async void saveButtonClick(object sender, EventArgs e)
        {
            //validate date is not less than today and no other workout scheduled
            cClientWorkouts.scheduledDate = scheduledDate.Date;

            if (cClientWorkouts.scheduledDate < DateTime.Today)
            {
                await DisplayAlert("Error Saving Workout", "Cannot change a workout date to before current date.", "OK");
                return;
            }

            if  (App.Database.verifyClientWorkoutDates(cClientWorkouts) > 0)
            {
                await DisplayAlert("Error Saving Workout", "Client already has a workout scheduled for this day." + Environment.NewLine + "Please select a different date or delete the current workout on this day.", "OK");
                return;
            }

            ClientWorkouts clientWorkouts = new ClientWorkouts(cClientWorkouts.ID, cClientWorkouts.clientID, cClientWorkouts.workoutID, cClientWorkouts.scheduledDate, cClientWorkouts.completedDate);

            await App.Database.saveClientWorkout(clientWorkouts);
            await Navigation.PopAsync();

        }
    }
}