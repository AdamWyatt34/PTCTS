using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTCTS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWorkoutPlan : ContentPage
    {
        Trainer cTrainer;
        public AddWorkoutPlan(Trainer trainer)
        {
            InitializeComponent();
            cTrainer = trainer;
        }

        protected override void OnAppearing()
        {
            workoutPlanLevel.Items.Add("Beginner");
            workoutPlanLevel.Items.Add("Intermediate");
            workoutPlanLevel.Items.Add("Advanced");

            var workoutPlanTypes = App.Database.getWorkoutPlanTypes();
            var planTypes = new List<string>();

            foreach(WorkoutPlanType workoutPlan in workoutPlanTypes.Result)
            {
                planTypes.Add(workoutPlan.Type);
            }

            workoutPlanType.ItemsSource = planTypes;
            

            base.OnAppearing();
        }

        private async void savePlanButtonClick(object sender, EventArgs e)
        {
            WorkoutPlan workoutPlan = new WorkoutPlan
            {
                ID = 0,
                name = workoutPlanName.Text,
                description = workoutPlanDescription.Text,
                //planLevel = workoutPlanLevel.SelectedItem.ToString(),
                workoutPlanType = workoutPlanType.SelectedIndex + 1
            };
            


            if (workoutPlanLevel.SelectedItem == null)
            {
                workoutPlan.planLevel = null;
            }
            else
            {
                workoutPlan.planLevel = workoutPlanLevel.SelectedItem.ToString();
            }

            string validatePlan = workoutPlan.validatePlan(workoutPlan);
            if (validatePlan == null)
            {
                await App.Database.saveWorkoutPlan(workoutPlan);
                workoutPlan.ID = App.Database.getNewWorkoutPlanID();

                var workoutPage = new AddWorkoutPlanWorkout(workoutPlan, cTrainer);
                await Navigation.PushModalAsync(workoutPage);
            }
            else
            {
                await DisplayAlert("Error Adding Workout Plan", "Detected the following errors when adding a workout plan:" + Environment.NewLine + validatePlan, "OK");
            }
        }
    }
}