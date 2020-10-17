using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;
using PTCTS.Services;

namespace PTCTS.Models
{
    [Table("TimeBasedExercise")]
    public class TimeBasedExercise : WorkoutExercises
    {
        //[PrimaryKey, Column("workoutExerciseID")]
        public int workoutExerciseID { get; set; }
        public int timeTypeID { get; set; }
        public int sets { get; set; }
        public int time { get; set; }

        public TimeBasedExercise():base()
        {

        }

        public TimeBasedExercise(int pkID, int workout, int exercise, int workoutExerciseType, string comments,
            int timeTypeID, int sets, int time) : base(pkID, workout, exercise, workoutExerciseType, comments)
        {
            this.workoutExerciseTypeID = pkID;
            this.timeTypeID = timeTypeID;
            this.sets = sets;
            this.time = time;
        }

        public TimeBasedExercise(WorkoutExercises workoutExercises,int timeTypeID,int sets, int time):base(workoutExercises.ID,
            workoutExercises.workoutID, workoutExercises.exerciseID, workoutExercises.workoutExerciseTypeID, workoutExercises.comments)
        {
            this.workoutExerciseTypeID = workoutExercises.ID;
            this.timeTypeID = timeTypeID;
            this.sets = sets;
            this.time = time;
        }

        public void addTestTimeBased()
        {
            TimeBasedExercise timeExercise = new TimeBasedExercise(0, 1, 8, 2, "", 2, 1, 15);
            App.Database.saveTimeBasedExercise(timeExercise);
        }

        public string validateExercise(TimeBasedExercise timeBasedExercise)
        {
            string validation = null;

            if (Validation.nullCheck(timeBasedExercise.workoutExerciseID.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise, " + Environment.NewLine; ;
            }

            if (Validation.nullCheck(timeBasedExercise.workoutExerciseTypeID.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise Type, " + Environment.NewLine;
            }

            if (timeBasedExercise.sets == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Sets, " + Environment.NewLine;
            }
            else if (timeBasedExercise.sets <= 0)
            {
                validation = validation + "Sets must be greater than 0, " + Environment.NewLine;
            }

            if (timeBasedExercise.time == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Reps, " + Environment.NewLine;
            }
            else if (timeBasedExercise.time <= 0)
            {
                validation = validation + "Reps must be greater than 0, " + Environment.NewLine;
            }

            if (timeBasedExercise.timeTypeID == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Time Type, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }

    }
}
