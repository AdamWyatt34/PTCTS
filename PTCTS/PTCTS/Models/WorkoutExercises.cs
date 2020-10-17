using System;
using System.Collections.Generic;
using System.Text;
using PTCTS.ViewModels;
using SQLite;

namespace PTCTS.Models
{
    [Table("WorkoutExercises")]
    public class WorkoutExercises
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int workoutID { get; set; }
        public int exerciseID { get; set; }
        public int workoutExerciseTypeID { get; set; }
        public string comments { get; set; }

        public WorkoutExercises()
        {

        }

        public WorkoutExercises(int pkID, int workout, int exercise, int workoutExerciseType, string comments)
        {
            this.ID = pkID;
            this.workoutID = workout;
            this.exerciseID = exercise;
            this.workoutExerciseTypeID = workoutExerciseType;
            this.comments = comments;
        }

        public WorkoutExercises(WorkoutExerciseViewModel exerciseViewModel)
        {
            this.ID = exerciseViewModel.ID;
            this.workoutID = exerciseViewModel.workoutID;
            this.exerciseID = exerciseViewModel.exerciseID;
            this.workoutExerciseTypeID = App.Database.getWorkoutExerciseTypeIDFromType(exerciseViewModel.exerciseType);
        }
    }
}
