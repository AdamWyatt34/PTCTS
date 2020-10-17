using PTCTS.Models;
using PTCTS.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWorkoutPlan : ContentPage
    {
        WorkoutPlanWorkoutViewModel workoutPlanVM;
        WorkoutPlan cWorkoutPlan;
        Trainer cTrainer;
        public EditWorkoutPlan(WorkoutPlan workoutPlan, Trainer trainer)
        {
            InitializeComponent();
            cWorkoutPlan = workoutPlan;
            cTrainer = trainer;

            workoutPlanWorkouts.ItemSelected += WorkoutPlanWorkouts_ItemSelected;
        }

        private void WorkoutPlanWorkouts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            workoutPlanVM = e.SelectedItem as WorkoutPlanWorkoutViewModel;
        }

        protected override void OnAppearing()
        {
            List<WorkoutPlanWorkoutViewModel> models = new List<WorkoutPlanWorkoutViewModel>();

            models = App.Database.getWorkoutPlanWorkouts(cWorkoutPlan);

            workoutPlanLevel.Items.Add("Beginner");
            workoutPlanLevel.Items.Add("Intermediate");
            workoutPlanLevel.Items.Add("Advanced");

            var workoutPlanTypes = App.Database.getWorkoutPlanTypes();
            var planTypes = new List<string>();

            foreach (WorkoutPlanType workoutPlan in workoutPlanTypes.Result)
            {
                planTypes.Add(workoutPlan.Type);
            }

            workoutPlanType.ItemsSource = planTypes;

            workoutPlanName.Text = cWorkoutPlan.name;
            workoutPlanDescription.Text = cWorkoutPlan.description;
            workoutPlanLevel.SelectedItem = cWorkoutPlan.planLevel;
            workoutPlanType.SelectedIndex = cWorkoutPlan.workoutPlanType - 1;
            workoutPlanWorkouts.ItemsSource = models;
            //set all fields here

            base.OnAppearing();
        }

        private async void addWorkoutButtonClick(object sender, EventArgs e)
        {
            var exercisePage = new EditWorkoutPlanAddWorkout(cWorkoutPlan, cTrainer);
            await Navigation.PushModalAsync(exercisePage);
        }

        private async void deleteWorkoutButtonClick(object sender, EventArgs e)
        {
            List<WorkoutPlanWorkoutViewModel> models = new List<WorkoutPlanWorkoutViewModel>();

            if (workoutPlanVM == null)
            {
                await DisplayAlert("Delete Plan Workout", "Please select a workout to delete.", "OK");
                return;
            }

            var answer = await DisplayAlert("Delete Plan Workout", "Are you sure you want to delete " + workoutPlanVM.workoutName + " from this plan?", "Yes", "No");

            if (answer)
            {
                workoutPlanVM.deleteWorkoutPlanWorkout(workoutPlanVM);
                models = App.Database.getWorkoutPlanWorkouts(cWorkoutPlan);
                workoutPlanWorkouts.ItemsSource = models;
            }
        }

        private async void deleteWorkoutPlanButtonClick(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Workout Plan", "Are you sure you want to delete this workout plan?", "Yes", "No");

            if (answer)
            {
                await App.Database.DeleteWorkoutPlan(cWorkoutPlan);

                App.Current.MainPage = new MainPage(cTrainer, 2);
            }
        }

        private async void saveWorkoutPlanButtonClick(object sender, EventArgs e)
        {
            WorkoutPlan workoutPlan = new WorkoutPlan
            {
                ID = 0,
                name = workoutPlanName.Text,
                description = workoutPlanDescription.Text,
                planLevel = workoutPlanLevel.SelectedItem.ToString(),
                workoutPlanType = workoutPlanType.SelectedIndex - 1
            };

            string validatePlan = workoutPlan.validatePlan(workoutPlan);
            if (validatePlan != null)
            {
                await App.Database.saveWorkoutPlan(workoutPlan);
                App.Current.MainPage = new MainPage(cTrainer, 1);
            }
            else
            {
                await DisplayAlert("Error Adding Workout Plan", "Detected the following errors when adding a workout plan:" + Environment.NewLine + validatePlan, "OK");
            }
        }
    }
}