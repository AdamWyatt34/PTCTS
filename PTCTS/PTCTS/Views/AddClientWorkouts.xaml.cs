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
    public partial class AddClientWorkouts : MyTabbedPage
    {
        Trainer cTrainer;
        Client cClient;
        public AddClientWorkouts(Trainer trainer, Client client)
        {
            InitializeComponent();

            cTrainer = trainer;
            cClient = client;

            MyNavigationPage navigation = new MyNavigationPage(new AddClientWorkoutsWorkoutPlan(cClient.ID,cTrainer));
            navigation.Title = "Workout Plans";

            Children.Add(navigation);

            navigation = new MyNavigationPage(new AddClientWorkoutsWorkout(cTrainer, cClient.ID));
            navigation.Title = "Workouts";

            Children.Add(navigation);

        }
    }
}