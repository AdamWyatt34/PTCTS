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
    public partial class ManageClientWorkouts : ContentPage
    {
        //ClientWorkouts selectedWorkout;
        ClientWorkoutsViewModel selectedWorkout;
        Trainer cTrainer;
        Client cClient;
        public ManageClientWorkouts(Trainer trainer, Client client)
        {
            InitializeComponent();
            cTrainer = trainer;
            cClient = client;
            pendingWorkouts.ItemSelected += PendingWorkouts_ItemSelected;
            this.Title = "Client Workouts";
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        private void PendingWorkouts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ClientWorkoutsViewModel clientWorkout = e.SelectedItem as ClientWorkoutsViewModel;
            //ClientWorkouts workouts = e.SelectedItem as ClientWorkoutsViewModel;
            ClientWorkouts workouts = new ClientWorkouts(clientWorkout.ID, clientWorkout.clientID, clientWorkout.workoutID, clientWorkout.scheduledDate, clientWorkout.completedDate);
            selectedWorkout = clientWorkout;
            //selectedWorkout = workouts;
        }

        protected override void OnAppearing()
        {
            //pendingWorkouts.ItemsSource = App.Database.getPendingClientWorkouts(cClient);
            pendingWorkouts.ItemsSource = App.Database.getNewPendingClientWorkouts(cClient);


            base.OnAppearing();
        }

        private async void addWorkoutsButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientWorkouts(cTrainer, cClient));
        }

        private async void markWorkoutCompleteButtonClick(object sender, EventArgs e)
        {
            if(selectedWorkout == null)
            {
                await DisplayAlert("No Workout Selected", "Please select a workout to complete this action.", "OK");
                return;
            }

            var answer = await DisplayAlert("Mark Workout Complete", "Are you sure you want to mark this workout complete?", "Yes","No");
        
            if(answer)
            {
                selectedWorkout.completedDate = DateTime.Now;
                ClientWorkouts clientWorkouts = new ClientWorkouts(selectedWorkout.ID, selectedWorkout.clientID, selectedWorkout.workoutID, selectedWorkout.scheduledDate, selectedWorkout.completedDate);
                await App.Database.saveClientWorkout(clientWorkouts);
            }

            pendingWorkouts.ItemsSource = App.Database.getNewPendingClientWorkouts(cClient);
        }

        private async void viewWorkoutButtonClick(object sender, EventArgs e)
        {
            if (selectedWorkout == null)
            {
                await DisplayAlert("No Workout Selected", "Please select a workout to complete this action.", "OK");
                return;
            }

            Workout iWorkout = App.Database.loadWorkoutFromID(selectedWorkout.workoutID);

            await Navigation.PushAsync(new ViewWorkout(iWorkout, cTrainer));
        }

        private async void deleteWorkoutButtonClick(object sender, EventArgs e)
        {
            if (selectedWorkout == null)
            {
                await DisplayAlert("No Workout Selected", "Please select a workout to complete this action.", "OK");
                return;
            }

            ClientWorkouts clientWorkouts = new ClientWorkouts(selectedWorkout.ID, selectedWorkout.clientID, selectedWorkout.workoutID, selectedWorkout.scheduledDate, selectedWorkout.completedDate);

            var answer = await DisplayAlert("Delete Workout", "Are you sure you want to delete this workout?", "Yes","No");

            if (answer)
            {
                await App.Database.DeleteClientWorkout(clientWorkouts);
            }

            pendingWorkouts.ItemsSource = App.Database.getNewPendingClientWorkouts(cClient);
        }

        private async void changeWorkoutDateButtonClick(object sender, EventArgs e)
        {
            if (selectedWorkout == null)
            {
                await DisplayAlert("No Workout Selected", "Please select a workout to complete this action.", "OK");
                return;
            }

            //ClientWorkoutsViewModel clientWorkoutsViewModel = new ClientWorkoutsViewModel(selectedWorkout);

            await Navigation.PushAsync(new EditClientWorkout(selectedWorkout));
        }

        private async void addWorkout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientWorkoutsWorkout(cTrainer, cClient.ID));
        }

        private async void addWorkoutPlan(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientWorkoutsWorkoutPlan(cClient.ID, cTrainer));
        }
    }
}