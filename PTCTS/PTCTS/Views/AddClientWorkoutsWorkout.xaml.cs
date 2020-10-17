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
    public partial class AddClientWorkoutsWorkout : ContentPage
    {
        Trainer cTrainer;
        int pkClientID;
        Workout selectedWorkout;
        public AddClientWorkoutsWorkout(Trainer trainer, int clientID)
        {
            InitializeComponent();
            cTrainer = trainer;
            pkClientID = clientID;
            this.Title = "Add Client Workout";
            //this.Title = "PTCTS";
        }

        protected override async void OnAppearing()
        {
            var workouts = await App.Database.getAllWorkouts();

            //var workouts = await App.Database.getWorkouts();

            workoutsListView.ItemsSource = workouts;
            workoutsListView.ItemTapped += WorkoutsListView_ItemTapped;

            base.OnAppearing();
        }

        private void WorkoutsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Workout workout = new Workout((WorkoutViewModel)e.Item);
            selectedWorkout = workout;
        }

        private async void addWorkoutButtonClick(object sender, EventArgs e)
        {
            if (scheduledDate.Date < DateTime.Today)
            {
                await DisplayAlert("Error Saving Workout", "Cannot save a workout date to before current date.", "OK");
                return;
            }

            if (selectedWorkout == null)
            {
                await DisplayAlert("Error Saving Workout", "Please select a workout to add to this client", "OK");
                return;
            }

            ClientWorkouts clientWorkouts = new ClientWorkouts(0, pkClientID, selectedWorkout.ID, scheduledDate.Date);
            ClientWorkoutsViewModel viewModel = new ClientWorkoutsViewModel(clientWorkouts.ID, clientWorkouts.clientID, clientWorkouts.workoutID, clientWorkouts.scheduledDate, clientWorkouts.completedDate, "", "");


            if (App.Database.verifyClientWorkoutDates(viewModel) > 0)
            {
                await DisplayAlert("Error Saving Workout", "Client already has a workout scheduled for the selected day." + Environment.NewLine + "Please select a different date to add this workout plan.", "OK");
                return;
            }

            var answer = await DisplayAlert("Add Workout", "Are you sure you want to add the " + selectedWorkout.name + " workout to this client?", "Yes", "No");

            if (!answer)
            {
                return;
            }
            else
            {
                clientWorkouts.scheduledDate = scheduledDate.Date;

                await App.Database.saveClientWorkout(clientWorkouts);
                await Navigation.PopAsync();
            }
        }
    }
}