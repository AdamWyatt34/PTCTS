using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("ExerciseType")]
    public class ExerciseType
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string Type { get; set; }

        public ExerciseType()
        {

        }
        public ExerciseType(int pkID, string type)
        {
            this.ID = pkID;
            this.Type = type;
        }

        public void createExerciseTypes()
        {
            ExerciseType exercise = new ExerciseType(0, "Cardio"); //1
            App.Database.saveExerciseType(exercise);

            exercise = new ExerciseType(0, "Olympic Weightlifting"); //2
            App.Database.saveExerciseType(exercise);

            exercise = new ExerciseType(0, "Plyometrics"); //3
            App.Database.saveExerciseType(exercise);

            exercise = new ExerciseType(0, "Powerlifting"); //4
            App.Database.saveExerciseType(exercise);

            exercise = new ExerciseType(0, "Strength"); //5
            App.Database.saveExerciseType(exercise);

            exercise = new ExerciseType(0, "Stretching"); //6
            App.Database.saveExerciseType(exercise);

            exercise = new ExerciseType(0, "Strongman"); //7
            App.Database.saveExerciseType(exercise);
        }
    }
}
