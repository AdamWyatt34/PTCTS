using PTCTS.Models;
using PTCTS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWorkout : ContentPage
    {
        Trainer cTrainer;
        //List<WorkoutExerciseViewModel> workoutExercises = new List<WorkoutExerciseViewModel>();
        public AddWorkout(Trainer trainer)
        {
            InitializeComponent();
            cTrainer = trainer;

            var equipNeeded = App.Database.getAvailableEquipment();
            var availEquip = new List<string>();

            foreach(AvailableEquipment equipment in equipNeeded.Result)
            {
                availEquip.Add(equipment.equipment);
            }

            equipmentNeeded.ItemsSource = availEquip;

        }

        //public AddWorkout(Workout workout, Trainer trainer)
        //{
        //    InitializeComponent();

        //    cTrainer = trainer;
        //    cWorkout = workout;
        //    //workoutExercises = exerciseViewModels;
        //    workoutName.Text = cWorkout.name;
        //    workoutDescription.Text = cWorkout.description;
        //    equipmentNeeded.SelectedIndex = cWorkout.availableEquipmentID - 1;
        //    estimatedMinutes.Text = cWorkout.estimatedMinutes.ToString();
        //}

        protected override void OnAppearing()
        {
            //NavigationPage.SetHasNavigationBar(this, false);

            base.OnAppearing();
        }
        //private async void addExerciseButtonClick(object sender, EventArgs e)
        //{
        //    int iEstimatedMinutes = 0;
        //    if(estimatedMinutes.Text != "" && estimatedMinutes.Text != string.Empty && estimatedMinutes.Text != null)
        //    {
        //        iEstimatedMinutes = int.Parse(estimatedMinutes.Text);
        //    }

        //    Workout workout = new Workout(0, workoutName.Text, workoutDescription.Text, equipmentNeeded.SelectedIndex + 1,iEstimatedMinutes);
        //    //{
        //    //    ID = 0,
        //    //    name = workoutName.Text,
        //    //    description = workoutDescription.Text,
        //    //    availableEquipmentID = equipmentNeeded.SelectedIndex + 1,
        //    //    estimatedMinutes = int.Parse(estimatedMinutes.Text)
        //    //};

        //    var exercisePage = new MyNavigationPage( new AddWorkoutExercise(workout, workoutExercises, cTrainer));
        //    await Navigation.PushAsync(exercisePage);
        //    //await Navigation.PushModalAsync(exercisePage);
        //}

        //private async void deleteExerciseButtonClick(object sender, EventArgs e)
        //{
        //    WorkoutExerciseViewModel workoutExerciseView = (WorkoutExerciseViewModel)newWorkoutExercises.SelectedItem;

        //    var answer = await DisplayAlert("Delete Exercise", "Are you sure you want to delete " + workoutExerciseView.exerciseName + " from this workout?", "Yes", "No");

        //    if(answer)
        //    {
        //        workoutExercises.Remove(workoutExerciseView);
        //    }
        //}

        private async void saveWorkoutButtonClick(object sender, EventArgs e)
        {
            //WorkoutExerciseViewModel model = new WorkoutExerciseViewModel();
            
            Workout workout = new Workout
            {
                ID = 0,
                name = workoutName.Text,
                description = workoutDescription.Text,
                availableEquipmentID = equipmentNeeded.SelectedIndex + 1,
                //estimatedMinutes = int.Parse(estimatedMinutes.Text)
            };

            if (estimatedMinutes.Text == null)
            {
                workout.estimatedMinutes = 0;
            }
            else
            {
                workout.estimatedMinutes = int.Parse(estimatedMinutes.Text);
            }

            string validateWorkout = workout.validateWorkout(workout);
            if (validateWorkout == null)
            {
                await App.Database.saveWorkout(workout);
                workout.ID = App.Database.getNewWorkoutID();

                var exericsePage = new AddWorkoutExercise(workout, cTrainer);
                await Navigation.PushModalAsync(exericsePage);
                //App.Current.MainPage = new MainPage(cTrainer, 3);
                //await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error Adding Workout", "Detected the following errors when adding a workout:" + Environment.NewLine + validateWorkout, "OK");
            }
            

            //foreach(WorkoutExerciseViewModel workoutExerciseViewModel in workoutExercises)
            //{
            //    workoutExerciseViewModel.workoutID = workout.ID;
            //}

            //model.saveWorkoutExercises(workoutExercises);
            //App.Current.MainPage = new MainPage(cTrainer, 2);

            //navigate to add exercise page
           //await  Navigation.PushModalAsync(new AddWorkoutExercise(workout, cTrainer));
        }

    }
}