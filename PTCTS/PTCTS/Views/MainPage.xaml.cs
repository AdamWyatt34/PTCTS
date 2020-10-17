using PTCTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MyTabbedPage
    {
        Trainer currentTrainer;

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(Trainer trainer)
        {
            InitializeComponent();

            App.Current.MainPage = this;

            currentTrainer = trainer;

            MyNavigationPage navigation = new MyNavigationPage(new ClientsMainPage(trainer));
            navigation.Title = "Clients";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new WorkoutPlansMainPage(trainer));
            navigation.Title = "Workout Plans";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new WorkoutsMainPage(trainer));
            navigation.Title = "Workouts";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new ExercisesMainPage(trainer));
            navigation.Title = "Exercises";

            Children.Add(navigation);
            this.Title = "PTCTS";
            //Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, ToolbarPlacement.Bottom);


        }

        public MainPage(Trainer trainer, int activePage)
        {
            InitializeComponent();

            App.Current.MainPage = this;

            currentTrainer = trainer;

            MyNavigationPage navigation = new MyNavigationPage(new ClientsMainPage(trainer));
            navigation.Title = "Clients";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new WorkoutPlansMainPage(trainer));
            navigation.Title = "Workout Plans";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new WorkoutsMainPage(trainer));
            navigation.Title = "Workouts";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new ExercisesMainPage(trainer));
            navigation.Title = "Exercises";

            Children.Add(navigation);
            this.Title = "PTCTS";

            CurrentPage = Children[activePage];
        }

        protected override void OnDisappearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            base.OnDisappearing();
        }
    }
}