using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PTCTS.Services;
using PTCTS.Views;

namespace PTCTS
{
    public partial class App : Application
    {
        static PTCTS_Database database;
        public App()
        {
            InitializeComponent();
            
            //DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new Login());

        }

        public static PTCTS_Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new PTCTS_Database();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
