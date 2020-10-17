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
    public partial class EditWorkout : ContentPage
    {
        WorkoutExerciseViewModel WorkoutExerciseViewModel;
        //string exerciseSelected = "";
        Workout cWorkout;
        Trainer cTrainer;
        public EditWorkout(Workout iWorkout, Trainer iTrainer)
        {
            InitializeComponent();
            cWorkout = iWorkout;
            cTrainer = iTrainer;

            var equipNeeded = App.Database.getAvailableEquipment();
            var availEquip = new List<string>();

            foreach (AvailableEquipment equipment in equipNeeded.Result)
            {
                availEquip.Add(equipment.equipment);
            }

            equipmentNeeded.ItemsSource = availEquip;

            editWorkoutExercises.ItemTapped += EditWorkoutExercises_ItemTapped;
            
        }

        private void EditWorkoutExercises_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var item = e.Item as WorkoutExerciseViewModel;
            //exerciseSelected = item.exerciseName;
            WorkoutExerciseViewModel = e.Item as WorkoutExerciseViewModel;
        }

        protected override void OnAppearing()
        {
            List<WorkoutExerciseViewModel> models = new List<WorkoutExerciseViewModel>();

            workoutName.Text = cWorkout.name;
            workoutDescription.Text = cWorkout.description;
            equipmentNeeded.SelectedIndex = cWorkout.availableEquipmentID - 1;
            estimatedMinutes.Text = cWorkout.estimatedMinutes.ToString();
            models = App.Database.getWorkoutExercises(cWorkout);
            editWorkoutExercises.ItemsSource = models;

            base.OnAppearing();
        }

        private async void deleteWorkoutButtonClick(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Workout", "Are you sure you want to delete this workout?", "Yes", "No");

            if (answer)
            {
                await App.Database.DeleteWorkout(cWorkout);

                App.Current.MainPage = new MainPage(cTrainer, 2);
            }
        }

        private async void addExerciseButtonClick(object sender, EventArgs e)
        {
            //add exercise
            var exercisePage = new MyNavigationPage( new EditWorkoutAddExercise(cWorkout, cTrainer));
            await Navigation.PushModalAsync(exercisePage);
        }

        private async void saveWorkoutButtonClick(object sender, EventArgs e)
        {
            Workout workout = new Workout
            {
                ID = cWorkout.ID,
                name = workoutName.Text,
                description = workoutDescription.Text,
                availableEquipmentID = equipmentNeeded.SelectedIndex + 1,
                estimatedMinutes = int.Parse(estimatedMinutes.Text)
            };

            string validateWorkout = workout.validateWorkout(workout);
            if (validateWorkout == null)
            {
                await App.Database.saveWorkout(workout);
                //workout.ID = App.Database.getNewWorkoutID();
               
                App.Current.MainPage = new MainPage(cTrainer, 2);
                //await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error Saving Workout", "Detected the following errors when saving this workout:" + Environment.NewLine + validateWorkout, "OK");
            }
        }

        private async void deleteExerciseButtonClick(object sender, EventArgs e)
        {
            List<WorkoutExerciseViewModel> models = new List<WorkoutExerciseViewModel>();

            var answer = await DisplayAlert("Delete Workout Exercise", "Are you sure you want to delete " + WorkoutExerciseViewModel.exerciseName + " from this workout?", "Yes", "No");
            
            if (answer)
            {
                //delete exercise from workout and reset list info
                WorkoutExerciseViewModel.deleteWorkoutExcerise(WorkoutExerciseViewModel);
                models = App.Database.getWorkoutExercises(cWorkout);
                editWorkoutExercises.ItemsSource = models;
            }
        }

        private async void editExerciseButtonClick(object sender, EventArgs e)
        {
            //open edit exercise form
            var exercisePage = new MyNavigationPage(new EditWorkoutEditExercise(cWorkout, cTrainer,WorkoutExerciseViewModel));
            await Navigation.PushModalAsync(exercisePage);
        }
    }
}