using System;
using System.Collections.Generic;
using System.Text;
using PTCTS.Models;

namespace PTCTS.ViewModels
{
    public class WorkoutPlanWorkoutViewModel
    {
        public int pkID { get; set; }
        public int workoutPlanID { get; set; }
        public int workoutID { get; set; }
        public string workoutName { get; set; }
        public string workoutDescription { get; set; }

        public WorkoutPlanWorkoutViewModel()
        {

        }

        public WorkoutPlanWorkoutViewModel(int ID, int planID, int wID, string wName, string wDesc)
        {
            this.pkID = ID;
            this.workoutPlanID = planID;
            this.workoutID = wID;
            this.workoutName = wName;
            this.workoutDescription = wDesc;
        }

        public async void deleteWorkoutPlanWorkout(WorkoutPlanWorkoutViewModel viewModel)
        {
            WorkoutPlanWorkout workoutPlanWorkout = new WorkoutPlanWorkout(viewModel.pkID, viewModel.workoutPlanID, viewModel.workoutID);

            await App.Database.DeleteWorkoutPlanWorkout(workoutPlanWorkout);

        }

    }
}
