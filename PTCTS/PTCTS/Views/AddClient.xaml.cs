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
    public partial class AddClient : ContentPage
    {
        Trainer cTrainer;
        public AddClient(Trainer trainer)
        {
            InitializeComponent();
            cTrainer = trainer;
        }

        protected override void OnAppearing()
        {
            var activityLevels = App.Database.getActivyLevels();
            List<string> aLevels = new List<string>();

            foreach(ActivityLevel level in activityLevels.Result)
            {
                aLevels.Add(level.activityLevel);
            }

            activityLevel.ItemsSource = aLevels;
            NavigationPage.SetHasNavigationBar(this, false);

            base.OnAppearing();
        }

        private async void saveClientButtonClick(object sender, EventArgs e)
        {
            Client client = new Client
            {
                ID = 0,
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

            if(validateClient == null)
            {
                await App.Database.saveClientAsync(client);

                App.Current.MainPage = new MainPage(cTrainer, 0);
            }
            else
            {
                await DisplayAlert("Error Adding Client", "Detected the following errors when adding a client:" + Environment.NewLine + validateClient, "OK");
            }

        }
    }
}