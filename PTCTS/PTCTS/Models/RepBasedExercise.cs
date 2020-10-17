using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PTCTS.Services;

namespace PTCTS.Models
{
    [Table("RepBasedExercise")]
    public class RepBasedExercise : WorkoutExercises
    {
        //[PrimaryKey, Column("workoutExerciseID")]
        public int workoutExerciseID { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }

        public RepBasedExercise() : base()
        {

        }

        public RepBasedExercise(int pkID, int workout, int exercise, int workoutExerciseType, string comments,
            int sets, int reps) : base(pkID, workout, exercise, workoutExerciseType, comments)
        {
            this.workoutExerciseID = pkID;
            this.sets = sets;
            this.reps = reps;
        }

        public RepBasedExercise(WorkoutExercises workoutExercises, int sets, int reps):base(workoutExercises.ID,
            workoutExercises.workoutID,workoutExercises.exerciseID,workoutExercises.workoutExerciseTypeID,workoutExercises.comments)
        {
            this.workoutExerciseTypeID = workoutExercises.ID;
            this.sets = sets;
            this.reps = reps;
        }

        public async void addDefaultRepBasedExercise()
        {
            RepBasedExercise repExercise = new RepBasedExercise(0, 1, 1, 1, "Test comments", 3, 10);
            await App.Database.saveRepBasedExercise(repExercise);

            repExercise = new RepBasedExercise(0, 1, 2, 1, "", 3, 12);
            await App.Database.saveRepBasedExercise(repExercise);
        }

        public string validateExercise(RepBasedExercise repBasedExercise)
        {
            string validation = null;

            if (Validation.nullCheck(repBasedExercise.workoutExerciseID.ToString())) 
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise, " + Environment.NewLine; ;
            }

            if (Validation.nullCheck(repBasedExercise.workoutExerciseTypeID.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise Type, " + Environment.NewLine;
            }

            if (repBasedExercise.sets == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Sets, " + Environment.NewLine;
            }
            else if(repBasedExercise.sets <= 0)
            {
                validation = validation + "Sets must be greater than 0, " + Environment.NewLine;
            }

            if (repBasedExercise.reps == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Reps, " + Environment.NewLine;
            }
            else if(repBasedExercise.reps <= 0)
            {
                validation = validation + "Reps must be greater than 0, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }
    }
}
