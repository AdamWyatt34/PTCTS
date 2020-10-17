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
    public partial class ExercisesMainPage : ContentPage
    {
        Trainer cTrainer;
        public ExercisesMainPage(Trainer trainer)
        {
            InitializeComponent();

            cTrainer = trainer;

            exercisesListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(exerciseTapped);
        }

        protected override async void OnAppearing()
        {
            var exercises = await App.Database.getAllExercises();

            exercisesListView.ItemsSource = exercises;
            //NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "PTCTS";
            base.OnAppearing();
        }

        private async void exerciseTapped(object sender, ItemTappedEventArgs e)
        {
            ExerciseViewModel exerciseVM = (ExerciseViewModel)e.Item;
            Exercise exercise = new Exercise(exerciseVM);
            await Navigation.PushAsync(new MyNavigationPage(new EditExercise(exercise,cTrainer)));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyNavigationPage( new AddExercise(cTrainer)));
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                var exercises = await App.Database.searchAllExercises(e.NewTextValue);

                exercisesListView.ItemsSource = exercises;
            }
            else
            {
                var exercises = await App.Database.getAllExercises();

                exercisesListView.ItemsSource = exercises;
            }
        }
    }
}