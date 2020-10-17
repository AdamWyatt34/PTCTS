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
    public partial class EditExercise : ContentPage
    {
        Trainer cTrainer;
        Exercise cExercise;
        public EditExercise(Exercise iExercise, Trainer trainer)
        {
            InitializeComponent();
            cTrainer = trainer;
            cExercise = iExercise;

            var exerciseTypes = App.Database.getExerciseTypes();
            var exerciseTypeList = new List<string>();
            foreach (ExerciseType type in exerciseTypes.Result)
            {
                exerciseTypeList.Add(type.Type);
            }
            exerciseType.ItemsSource = exerciseTypeList;

            var equipmentTypes = App.Database.getEquipment();
            var equipmentTypeList = new List<string>();
            foreach (Equipment equip in equipmentTypes.Result)
            {
                equipmentTypeList.Add(equip.equipmentName);
            }
            equipment.ItemsSource = equipmentTypeList;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            exerciseName.Text = cExercise.exerciseName;
            exerciseDescription.Text = cExercise.description;
            exerciseType.SelectedIndex = cExercise.exerciseTypeID - 1;
            equipment.SelectedIndex = cExercise.equipmentID - 1;

            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            cExercise.exerciseName = exerciseName.Text;
            cExercise.description = exerciseDescription.Text;
            cExercise.exerciseTypeID = exerciseType.SelectedIndex + 1;
            cExercise.equipmentID = equipment.SelectedIndex + 1;

            string validateExercise = cExercise.validateExercises(cExercise);

            if (validateExercise == null)
            {
                await App.Database.saveExercise(cExercise);

                //await Navigation.PopModalAsync();
                App.Current.MainPage = new MainPage(cTrainer,3);
            }
            else
            {
                await DisplayAlert("Error Adding Exercise", "Detected the following errors when adding a exercise:" + Environment.NewLine + validateExercise, "OK");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Exercise", "Are you sure you want to delete this exercise?", "Yes", "No");

            if (answer)
            {
                await App.Database.DeleteExercise(cExercise);
                App.Current.MainPage = new MainPage(cTrainer, 3);
            }
        }
    }
}