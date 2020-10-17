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
    public partial class AddClientMeasurements : ContentPage
    {
        Client cClient;
        public AddClientMeasurements(Client client)
        {
            InitializeComponent();
            cClient = client;
        }

        private async void saveMeasurementButtonClicked(object sender, EventArgs e)
        {
            Models.ClientMeasurements newMeasurement = new Models.ClientMeasurements();

            int newWeight = 0;
            int.TryParse(clientNewWeight.Text, out newWeight);

            decimal newBodyFatPercentage = 0;
            decimal.TryParse(clientBodyFatPercentage.Text, out newBodyFatPercentage);

            newMeasurement.ID = 0;
            newMeasurement.clientID = cClient.ID;
            newMeasurement.weight = newWeight;
            newMeasurement.bodyFatPercentage = newBodyFatPercentage;
            newMeasurement.measurementDate = clientMeasurementDate.Date;

            string validateMeasurement = newMeasurement.validateMeasurement(newMeasurement);

            if (validateMeasurement == null)
            {
                await App.Database.saveClientMeasurement(newMeasurement);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error Adding Client Measurement", "Detected the following errors when adding a measurement: " + Environment.NewLine + validateMeasurement, "OK");
            }
        }
    }
}