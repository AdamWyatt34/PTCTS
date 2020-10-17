using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PTCTS.Models;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            //App.Database.InitializeAsync();
        }

        protected override async void OnAppearing()
        {
            var trainers = await App.Database.getTrainers();

            if(!trainers.Any())
            {

                Trainer trainer = new Trainer();
                trainer.createBaseTrainers();

                WorkoutExercises workout = new WorkoutExercises();
                //await App.Database.initializeWorkoutExercisesTable();

                //RepBasedExercise repExercise = new RepBasedExercise(0, 1, 1, 1, "Test comments", 3, 10);
                //await App.Database.saveRepBasedExercise(repExercise);

                //repExercise = new RepBasedExercise(0, 1, 2, 1, "", 3, 12);
                //await App.Database.saveRepBasedExercise(repExercise);

            }
            //await App.Database.InitializeAsync();


            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Trainer trainer = new Trainer(username.Text,password.Text);
            trainer.ID =  App.Database.verifyLogin(trainer);

            if(trainer.ID != 0)
            {
                trainer = App.Database.loadTrainerFromID(trainer);
                //await Navigation.PushModalAsync(new MainPage(trainer));
                //var mainPage = new NavigationPage(new MainPage(trainer));
                App.Current.MainPage = new MainPage(trainer);
                //await Navigation.PushAsync(mainPage);
            }
            else
            {
                await DisplayAlert("Login Not Successful", "Login Not Successful","OK");
            }
        }
    }
}