using System;
using System.Collections.Generic;
using System.Text;
using PTCTS.Models;

namespace PTCTS.ViewModels
{
    public class WorkoutExerciseViewModel
    {
        public int ID { get; set; }
        public int workoutID { get; set; }
        public int exerciseID { get; set; }
        public string exerciseName { get; set; }
        public string exerciseType { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public string repTimeUnit { get; set; }

        public WorkoutExerciseViewModel()
        {

        }

        public WorkoutExerciseViewModel(int pkID, int workout, int exercise, string eName, string eType, int set, int rep, int timeType = 0)
        {
            this.ID = pkID;
            this.workoutID = workout;
            this.exerciseID = exercise;
            this.exerciseName = eName;
            this.exerciseType = eType;
            this.sets = set;
            this.reps = rep;
            
            if (timeType == 0)
            {
                this.repTimeUnit = "reps";
            }
            else
            {
                this.repTimeUnit = App.Database.getTimeTypeFromID(timeType);
            }
        }

        public async void deleteWorkoutExcerise(WorkoutExerciseViewModel iWorkoutExercise)
        {
            WorkoutExercises workoutExercises = new WorkoutExercises(iWorkoutExercise);

            if (iWorkoutExercise.exerciseType == "Rep-Based")
            {
                RepBasedExercise repBasedExercise = new RepBasedExercise(workoutExercises, iWorkoutExercise.sets, iWorkoutExercise.reps);
                await App.Database.DeleteWorkoutExercise(workoutExercises);
                await App.Database.DeleteRepBasedExcerise(repBasedExercise);
            }
            else
            {
                TimeBasedExercise timeBasedExercise = new TimeBasedExercise(workoutExercises, App.Database.getTimeTypeIDFromType(iWorkoutExercise.repTimeUnit), iWorkoutExercise.sets, iWorkoutExercise.reps);
                await App.Database.DeleteWorkoutExercise(workoutExercises);
                await App.Database.DeleteTimeBasedExercise(timeBasedExercise);
            }

        }

        //public void saveWorkoutExercises(List<WorkoutExerciseViewModel> exerciseViewModels)
        //{

        //    foreach (WorkoutExerciseViewModel exercise in exerciseViewModels)
        //    {
        //        WorkoutExercises workoutExercises = new WorkoutExercises(exercise);

        //        if (workoutExercises.workoutExerciseTypeID == 1) //rep-based
        //        {
        //            RepBasedExercise repBased = new RepBasedExercise(workoutExercises,exercise.sets,exercise.reps);
        //            App.Database.saveRepBasedExercise(repBased);
        //        }
        //        else //time-based
        //        {
        //            TimeBasedExercise timeBased = new TimeBasedExercise(workoutExercises, App.Database.getTimeTypeIDFromType(exercise.repTimeUnit), exercise.sets, exercise.reps);
        //            App.Database.saveTimeBasedExercise(timeBased);
        //        }
        //    }
        //}
    }
}
