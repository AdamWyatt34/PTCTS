using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PTCTS.Services;

namespace PTCTS.Models
{
    [Table("WorkoutPlanWorkout")]
    public class WorkoutPlanWorkout
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int workoutPlanID { get; set; }
        public int workoutID { get; set; }

        public WorkoutPlanWorkout()
        {

        }

        public WorkoutPlanWorkout(int pkID, int workoutPlan, int workout)
        {
            this.ID = pkID;
            this.workoutPlanID = workoutPlan;
            this.workoutID = workout;
        }

        public void addDefaultWorkoutPlanWorkouts()
        {
            WorkoutPlanWorkout workout = new WorkoutPlanWorkout(0, 1, 1);
            App.Database.saveWorkoutPlanWorkout(workout);

            workout = new WorkoutPlanWorkout(0, 1, 2);
            App.Database.saveWorkoutPlanWorkout(workout);

            workout = new WorkoutPlanWorkout(0, 1, 1);
            App.Database.saveWorkoutPlanWorkout(workout);
        }

        public string validatePlanWorkout(WorkoutPlanWorkout planWorkout)
        {
            string validation = null;

            if (Validation.nullCheck(planWorkout.workoutID.ToString()))
            {
                validation = validation + Validation.fieldNullCheck(validation);
                validation = validation + "Workout, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }
    }
}
