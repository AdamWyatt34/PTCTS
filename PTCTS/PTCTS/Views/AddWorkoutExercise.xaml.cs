using PTCTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PTCTS.ViewModels;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWorkoutExercise : ContentPage
    {
        string exerciseSelected = "";
        Trainer cTrainer;
        Workout cWorkout;
        public AddWorkoutExercise(Workout workout, Trainer trainer)
        {
            InitializeComponent();
            cWorkout = workout;
            cTrainer = trainer;
            var workoutExerciseTypes = App.Database.getWorkoutExerciseTypes();
            var workoutExerciseTypeList = new List<string>();
            foreach(WorkoutExerciseType type in workoutExerciseTypes.Result)
            {
                workoutExerciseTypeList.Add(type.exerciseType);
            }
            exerciseType.ItemsSource = workoutExerciseTypeList;

            var timeTypes = App.Database.getTimeTypes();
            var types = new List<string>();
            foreach(TimeType timeType in timeTypes.Result)
            {
                types.Add(timeType.Type);
            }
            exerciseTimeType.ItemsSource = types;

            var allExercises = App.Database.getExercises();
            var exercises = new List<Exercise>();

            foreach(Exercise exercise in allExercises.Result)
            {
                exercises.Add(exercise);
            }

            selectedExercise.ItemsSource = exercises;
            NavigationPage.SetHasNavigationBar(this, false);
            selectedExercise.ItemTapped += SelectedExercise_ItemTapped;
            //NavigationPage.SetHasNavigationBar(App.Current.MainPage, false);
        }

        private void SelectedExercise_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Exercise;
            exerciseSelected = item.exerciseName;
        }

        private async void saveExerciseButtonClick(object sender, EventArgs e)
        {
            //WorkoutExerciseViewModel exerciseToAdd;

            string validateWorkoutExercise;
            int tempSets;
            int tempReps;

            if(setsEntryCell.Text == null)
            {
                tempSets = 0;
            }
            else
            {
                tempSets = int.Parse(setsEntryCell.Text);
            }

            if(repsEntryCell.Text == null)
            {
                tempReps = 0;
            }
            else
            {
                tempReps = int.Parse(repsEntryCell.Text);
            }

            if (exerciseSelected == "")
            {
                await DisplayAlert("Error Adding Exercise to Workout.", "Please select an exercise to add to this workout", "OK");
                return;
            }

            if (exerciseType.SelectedItem == null)
            {
                await DisplayAlert("Error Adding Exercise to Workout", "Please select exercise type to add exercise to this workout", "OK");
                return;
            }

            if (exerciseType.SelectedItem.ToString() == "Rep-Based")
            {
                RepBasedExercise repBasedExercise = new RepBasedExercise(0, cWorkout.ID, App.Database.getExerciseIDFromName(exerciseSelected), exerciseType.SelectedIndex + 1, "", tempSets, tempReps);

                validateWorkoutExercise = repBasedExercise.validateExercise(repBasedExercise);

                if (validateWorkoutExercise == null)
                {
                    await App.Database.saveRepBasedExercise(repBasedExercise);
                }
                else
                {
                    await DisplayAlert("Error Adding Exercise to Workout", "Detected the following errors when adding an exercise to this workout." + Environment.NewLine + validateWorkoutExercise, "OK");
                    return;
                }
                //WorkoutExerciseViewModel newExercise = new WorkoutExerciseViewModel(0, 0, App.Database.getExerciseIDFromName(exerciseSelected), exerciseSelected, exerciseType.SelectedItem.ToString(), int.Parse(setsEntryCell.Text),int.Parse(repsEntryCell.Text));
                //exerciseToAdd = newExercise;
            }
            else
            {
                TimeBasedExercise timeBasedExercise = new TimeBasedExercise(0, cWorkout.ID, App.Database.getExerciseIDFromName(exerciseSelected), exerciseType.SelectedIndex + 1, "", int.Parse(setsEntryCell.Text), int.Parse(repsEntryCell.Text), exerciseTimeType.SelectedIndex + 1);

                validateWorkoutExercise = timeBasedExercise.validateExercise(timeBasedExercise);

                if (validateWorkoutExercise == null)
                {
                    App.Database.saveTimeBasedExercise(timeBasedExercise);
                }
                else
                {
                    await DisplayAlert("Error Adding Exercise to Workout", "Detected the following errors when adding an exercise to this workout." + Environment.NewLine + validateWorkoutExercise, "OK");
                    return;
                }
                //WorkoutExerciseViewModel newExercise = new WorkoutExerciseViewModel(0, 0, App.Database.getExerciseIDFromName(selectedExercise.SelectedItem.ToString()), selectedExercise.SelectedItem.ToString(), exerciseType.SelectedItem.ToString(), int.Parse(setsEntryCell.Text),int.Parse(repsEntryCell.Text),exerciseTimeType.SelectedIndex + 1);
                //exerciseToAdd = newExercise;  
            }

            var answer = await DisplayAlert("Exercise Added", "Exercise Successfully Added to Workout." + Environment.NewLine + "Would you like to add another exericse?", "Yes","No");
            
            if (answer)
            {
                selectedExercise.SelectedItem = null;
                exerciseType.SelectedIndex = -1;
                setsEntryCell.Text = "";
                repsEntryCell.Text = "";
                exerciseTimeType.SelectedIndex = -1;
                timeTypeStackLayout.IsVisible = false;
                //reset exercise form.
            }
            else
            {
                App.Current.MainPage = new MainPage(cTrainer, 2);
            }
            //var workoutPage = new AddWorkout(cWorkout, workoutExercises, cTrainer);
            //await Navigation.PushAsync(workoutPage);
            //await Navigation.PopAsync();
        }

        private void exerciseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (exerciseType.SelectedIndex != -1)
            {
                var selectedValue = exerciseType.Items[exerciseType.SelectedIndex];

                if (selectedValue != "Rep-Based")
                {
                    timeTypeStackLayout.IsVisible = true;
                }
                else
                {
                    timeTypeStackLayout.IsVisible = false;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage(cTrainer, 2);
        }
    }
}