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
    public partial class EditClient : ContentPage
    {
        Trainer cTrainer;
        Client cClient;

        public EditClient(Trainer trainer, Client client)
        {
            InitializeComponent();
            cTrainer = trainer;
            cClient = client;
            this.Title = "Edit Client";
        }

        protected override void OnAppearing()
        {
            var activityLevels = App.Database.getActivyLevels();
            List<string> aLevels = new List<string>();

            foreach (ActivityLevel level in activityLevels.Result)
            {
                aLevels.Add(level.activityLevel);
            }

            activityLevel.ItemsSource = aLevels;
            //NavigationPage.SetHasNavigationBar(this, false);
            //MyNavigationPage.SetHasNavigationBar(this, false);

            firstName.Text = cClient.fName;
            lastName.Text = cClient.lName;
            dateOfBirth.Date = cClient.dateOfBirth;
            shortTermGoal.Text = cClient.shortTermGoal;
            longTermGoal.Text = cClient.longTermGoal;
            activityLevel.SelectedIndex = cClient.activityLevelID - 1;

            base.OnAppearing();
        }

        private async void saveClientButtonClick(object sender, EventArgs e)
        {
            Client client = new Client
            {
                ID = cClient.ID,
                fName = firstName.Text,
                lName = lastName.Text,
                dateOfBirth = dateOfBirth.Date,
                personTypeID = 2,
                dateEntered = DateTime.Now,
                shortTermGoal = shortTermGoal.Text,
                longTermGoal = longTermGoal.Text,
                activityLevelID = activityLevel.SelectedIndex + 1,
                trainerID = cTrainer.ID
            };

            string validateClient = client.validateClient(client);

            if (validateClient == null)
            {
                await App.Database.saveClientAsync(client);

                App.Current.MainPage = new MainPage(cTrainer, 0);
            }
            else
            {
                await DisplayAlert("Error Saving Client", "Detected the following errors when saving a client:" + Environment.NewLine + validateClient, "OK");
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //open reports
            await Navigation.PushAsync(new ClientReports(cClient));
        }

        private async void workoutsButtonClick(object sender, EventArgs e)
        {
            //open client workout page where workouts can be added or marked complete 
            await Navigation.PushAsync(new ManageClientWorkouts(cTrainer, cClient));
        }

        private async void measurementsButtonClick(object sender, EventArgs e)
        {
            //open form to see past measurements and to add new measurements
            await Navigation.PushAsync(new ClientMeasurements(cClient));
        }
    }
}