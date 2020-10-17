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
    public partial class AddClientWorkoutsWorkoutPlan : ContentPage
    {
        WorkoutPlan selectedPlan;
        int pkClientID;
        Trainer currentTrainer;
        public AddClientWorkoutsWorkoutPlan(int clientID, Trainer trainer)
        {
            InitializeComponent();
            currentTrainer = trainer;
            pkClientID = clientID;
            workoutPlanListView.ItemSelected += WorkoutPlanListView_ItemSelected;
            this.Title = "Add Workout Plan";
        }

        protected override async void OnAppearing()
        {
            //var workoutPlans = await App.Database.getWorkoutPlans();
            var workoutPlans = await App.Database.getFullPlans();

            workoutPlanListView.ItemsSource = workoutPlans;

            base.OnAppearing();
        }

        private void WorkoutPlanListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            WorkoutPlan plan = new WorkoutPlan((WorkoutPlanViewModel)e.SelectedItem);

            selectedPlan = plan;
        }

        private async void addWorkoutPlanButtonClick(object sender, EventArgs e)
        {
            if (scheduledDate.Date < DateTime.Today)
            {
                await DisplayAlert("Error Saving Workout", "Cannot change a workout date to before current date.", "OK");
                return;
            }

            if (selectedPlan == null)
            {
                await DisplayAlert("Error Saving Workout Plan", "Please select a workout plan to add to this client.", "OK");
                return;
            }

            ClientWorkouts clientWorkouts = new ClientWorkouts(0, pkClientID, 1, scheduledDate.Date);
            ClientWorkoutsViewModel viewModel = new ClientWorkoutsViewModel(clientWorkouts.ID, clientWorkouts.clientID, clientWorkouts.workoutID, clientWorkouts.scheduledDate, clientWorkouts.completedDate, "", "");


            if (App.Database.verifyClientWorkoutDates(viewModel) > 0)
            {
                await DisplayAlert("Error Saving Workout", "Client already has a workout scheduled for the selected day." + Environment.NewLine + "Please select a different date to add this workout plan.", "OK");
                return;
            }


            var answer = await DisplayAlert("Add Workout Plan", "Are you sure you want to add the " + selectedPlan.name + " plan's workout to this client?", "Yes", "No");
        
            if(!answer)
            {
                return;
            }

            //get all workoutIDs from Plan
            int loopCounter = 1;
            DateTime selectedDate = scheduledDate.Date;
            List<int> allWorkoutIDs = App.Database.getAllWorkoutIDsFromWorkoutPlan(selectedPlan);
            foreach(int i in allWorkoutIDs)
            {
                ClientWorkouts workouts = new ClientWorkouts(0, pkClientID, 0, selectedDate);
                ClientWorkoutsViewModel tempViewModel = new ClientWorkoutsViewModel(workouts.ID, workouts.clientID, workouts.workoutID, workouts.scheduledDate, workouts.completedDate, "", "");
                workouts.workoutID = allWorkoutIDs[loopCounter - 1];
                if (loopCounter == 1)
                {
                    //do nothing
                }
                else
                {
                    if (App.Database.verifyClientWorkoutDates(tempViewModel) > 0)
                    {
                        do
                        {
                            selectedDate = selectedDate.AddDays(1);
                            workouts.scheduledDate = selectedDate;
                            tempViewModel.scheduledDate = selectedDate;
                        } while (App.Database.verifyClientWorkoutDates(tempViewModel) > 0);
                    }

                }

                await App.Database.saveClientWorkout(workouts);
                selectedDate = selectedDate.AddDays(1);
                loopCounter += 1;
            }

            await Navigation.PopAsync();
        }
    }
}