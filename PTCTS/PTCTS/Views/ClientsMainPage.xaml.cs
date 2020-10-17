using PTCTS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsMainPage : ContentPage
    {
        public Trainer currentTrainer;
        private ObservableCollection<Client> trainerClients;

        public ClientsMainPage()
        {
            InitializeComponent();
        }

        public ClientsMainPage(Trainer trainer)
        {
            InitializeComponent();

            currentTrainer = trainer;
            this.Title = "PTCTS";
            clientsListView.ItemSelected += ClientsListView_ItemSelected;
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void ClientsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Client selectedClient = e.SelectedItem as Client;
            //await Navigation.PushAsync(new MyNavigationPage(new EditClient(currentTrainer, selectedClient)));
            await Navigation.PushAsync(new EditClient(currentTrainer, selectedClient));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyNavigationPage(new AddClient(currentTrainer)));
        }

        protected override async void OnAppearing()
        {
            var allClients = await App.Database.getClients(currentTrainer.ID);
            trainerClients = new ObservableCollection<Client>(allClients);
            clientsListView.ItemsSource = trainerClients;

            base.OnAppearing();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                var exercises = await App.Database.searchClients(currentTrainer.ID,e.NewTextValue);

                clientsListView.ItemsSource = exercises;
            }
            else
            {
                var exercises = await App.Database.getClients(currentTrainer.ID);

                clientsListView.ItemsSource = exercises;
            }
        }
    }
}