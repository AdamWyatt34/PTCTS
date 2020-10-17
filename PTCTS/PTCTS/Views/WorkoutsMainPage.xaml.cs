using PTCTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTCTS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutsMainPage : ContentPage
    {
        Trainer cTrainer;
        public WorkoutsMainPage(Trainer trainer)
        {
            InitializeComponent();

            this.Title = "PTCTS";

            cTrainer = trainer;
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            var workouts = await App.Database.getAllWorkouts();

            //var workouts = await App.Database.getWorkouts();

            workoutsListView.ItemsSource = workouts;
            workoutsListView.ItemTapped += WorkoutsListView_ItemTapped;

            base.OnAppearing();
        }

        private async void WorkoutsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Workout workout = new Workout((WorkoutViewModel)e.Item);

            var editWorkoutPage = new EditWorkout(workout, cTrainer);
            await Navigation.PushAsync(editWorkoutPage);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new NavigationPage(new AddWorkout(cTrainer)));
            var workoutPage = new AddWorkout(cTrainer);
            await Navigation.PushAsync(workoutPage);
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                var exercises = await App.Database.searchAllWorkouts(e.NewTextValue);

                workoutsListView.ItemsSource = exercises;
            }
            else
            {
                var exercises = await App.Database.getAllWorkouts();

                workoutsListView.ItemsSource = exercises;
            }
        }
    }
}