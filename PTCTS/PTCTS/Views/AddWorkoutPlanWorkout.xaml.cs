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
    public partial class AddWorkoutPlanWorkout : ContentPage
    {
        WorkoutPlan cPlan;
        Trainer cTrainer;
        string workoutSelected = "";
        public AddWorkoutPlanWorkout(WorkoutPlan workoutPlan, Trainer trainer)
        {
            InitializeComponent();
            cPlan = workoutPlan;
            cTrainer = trainer;
        }

        protected override void OnAppearing()
        {
            var allWorkouts = App.Database.getAllWorkouts();
            var workouts = new List<WorkoutViewModel>();

            foreach(WorkoutViewModel workout in allWorkouts.Result)
            {
                workouts.Add(workout);
            }

            selectedWorkout.ItemsSource = workouts;
            selectedWorkout.ItemTapped += SelectedWorkout_ItemTapped;

            base.OnAppearing();
        }

        private void SelectedWorkout_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as WorkoutViewModel;
            workoutSelected = item.name;
        }

        private async void saveWorkoutButtonClick(object sender, EventArgs e)
        {   

            int workoutID = App.Database.getWorkoutIDFromName(workoutSelected);
            WorkoutPlanWorkout planWorkout = new WorkoutPlanWorkout(0, cPlan.ID, workoutID);
            string validatePlanWorkout = planWorkout.validatePlanWorkout(planWorkout);

            if(workoutSelected == "")
            {
                await DisplayAlert("Error Adding Workout to Workout Plan", "Please select a workout to add to workout plan.", "OK");
                return;
            }

            if(validatePlanWorkout == null)
            {
                await App.Database.saveWorkoutPlanWorkout(planWorkout);
            }
            else
            {
                await DisplayAlert("Error Adding Workout to Workout Plan", "Detected the following errors when adding a workout to this plan:" + Environment.NewLine + validatePlanWorkout, "OK");
                return;
            }

            var answer = await DisplayAlert("Workout Added", "Workout added successfuly to plan." + Environment.NewLine + "Would you like to add another workout?", "Yes", "No");

            if(answer)
            {
                selectedWorkout.SelectedItem = null;
            }
            else
            {
                App.Current.MainPage = new MainPage(cTrainer, 1);
            }


        }

        private void backToMainMenuButtonClick(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage(cTrainer, 1);
        }
    }
}