using PTCTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExercise : ContentPage
    {
        Trainer cTrainer;
        public AddExercise(Trainer trainer)
        {
            InitializeComponent();

            cTrainer = trainer;

            var exerciseTypes = App.Database.getExerciseTypes();
            var exerciseTypeList = new List<string>();
            foreach(ExerciseType type in exerciseTypes.Result)
            {
                exerciseTypeList.Add(type.Type);
            }
            exerciseType.ItemsSource = exerciseTypeList;

            var equipmentTypes = App.Database.getEquipment();
            var equipmentTypeList = new List<string>();
            foreach(Equipment equip in equipmentTypes.Result)
            {
                equipmentTypeList.Add(equip.equipmentName);
            }
            equipment.ItemsSource = equipmentTypeList;
            NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "Add Exercise";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Exercise newExercise = new Exercise
            {
                ID = 0,
                exerciseName = exerciseName.Text,
                description = exerciseDescription.Text,
                exerciseTypeID = exerciseType.SelectedIndex + 1,
                equipmentID = equipment.SelectedIndex + 1
            };

            string validateExercise = newExercise.validateExercises(newExercise);

            if(validateExercise == null)
            {
                await App.Database.saveExercise(newExercise);

                App.Current.MainPage = new MainPage(cTrainer, 3);
                //await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error Adding Exercise", "Detected the following errors when adding a exercise:" + Environment.NewLine + validateExercise, "OK");
            }
        }
    }
}