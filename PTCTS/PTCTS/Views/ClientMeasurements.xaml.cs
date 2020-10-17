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
    public partial class ClientMeasurements : ContentPage
    {
        Client cClient;
        public ClientMeasurements(Client client)
        {
            InitializeComponent();
            cClient = client;
            this.Title = "Client Measurements";
        }

        protected override void OnAppearing()
        {
            measurementsListView.ItemsSource = App.Database.getClientMeasurements(cClient).Result.ToList();

            base.OnAppearing();
        }

        private async void addMeasurementsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClientMeasurements(cClient));
        }
    }
}