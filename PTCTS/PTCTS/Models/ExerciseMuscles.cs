using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("ExerciseMuscles")]
    public class ExerciseMuscles
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int exerciseID { get; set; }
        public int muscleID { get; set; }

        public ExerciseMuscles()
        {

        }

        public ExerciseMuscles(int pkID, int exercise, int muscle)
        {
            this.ID = pkID;
            this.exerciseID = exercise;
            this.muscleID = muscle;
        }

        public void addDefaultExerciseMuscles()
        {
            ExerciseMuscles exerciseMuscles = new ExerciseMuscles(0, 1, 1);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 2, 2);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 2, 3);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 2, 7);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 3, 4);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 3, 5);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 4, 7);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 5, 3);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 6, 8);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 7, 4);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 7, 5);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 8, 5);
            App.Database.saveExerciseMuscle(exerciseMuscles);

            exerciseMuscles = new ExerciseMuscles(0, 8, 6);
            App.Database.saveExerciseMuscle(exerciseMuscles);
        }
    }
}
