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
    public partial class ViewWorkout : ContentPage
    {
        Workout cWorkout;
        Trainer cTrainer;
        public ViewWorkout(Workout iWorkout, Trainer iTrainer)
        {
            InitializeComponent();
            cWorkout = iWorkout;
            cTrainer = iTrainer;
            this.Title = "View Client Workout";
        }

        protected override void OnAppearing()
        {

            List<WorkoutExerciseViewModel> models = new List<WorkoutExerciseViewModel>();

            workoutName.Text = cWorkout.name;
            workoutDescription.Text = cWorkout.description;
            workoutEquipment.Text= App.Database.getAvailableEquipmentFromID(cWorkout.availableEquipmentID);
            workoutMinutes.Text = cWorkout.estimatedMinutes.ToString();
            models = App.Database.getWorkoutExercises(cWorkout);
            viewWorkoutExercises.ItemsSource = models;

            base.OnAppearing();
        }
    }
}