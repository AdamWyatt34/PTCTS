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
    public partial class WorkoutPlansMainPage : ContentPage
    {
        public Trainer currentTrainer;

        public WorkoutPlansMainPage()
        {
            InitializeComponent();
        }

        public WorkoutPlansMainPage(Trainer trainer)
        {
            InitializeComponent();

            currentTrainer = trainer;
            this.Title = "PTCTS";
            workoutPlanListView.ItemSelected += WorkoutPlanListView_ItemSelected;
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void WorkoutPlanListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            WorkoutPlan workoutPlan = new WorkoutPlan((WorkoutPlanViewModel)e.SelectedItem);

            var editPlanPage = new EditWorkoutPlan(workoutPlan, currentTrainer);
            await Navigation.PushAsync(editPlanPage);
        }

        protected override async void OnAppearing()
        {
            //var workoutPlans = await App.Database.getWorkoutPlans();
            var workoutPlans = await App.Database.getFullPlans();

            workoutPlanListView.ItemsSource = workoutPlans;

            base.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var planPage = new AddWorkoutPlan(currentTrainer);
            await Navigation.PushAsync(planPage);
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                var exercises = await App.Database.searchFullPlans(e.NewTextValue);

                workoutPlanListView.ItemsSource = exercises;
            }
            else
            {
                var workoutPlans = await App.Database.getFullPlans();

                workoutPlanListView.ItemsSource = workoutPlans;
            }
        }
    }
}