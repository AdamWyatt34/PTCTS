using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PTCTS.Models
{
    [Table("WorkoutExerciseType")]
    public class WorkoutExerciseType
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string exerciseType { get; set; }

        public WorkoutExerciseType()
        {

        }

        public WorkoutExerciseType(int pkID, string type)
        {
            this.ID = pkID;
            this.exerciseType = type;
        }

        public void addDefaultWorkoutExerciseTypes()
        {
            WorkoutExerciseType exerciseType = new WorkoutExerciseType(0, "Rep-Based"); //1
            App.Database.saveWorkoutExerciseType(exerciseType);

            exerciseType = new WorkoutExerciseType(0, "Time-Based"); //2
            App.Database.saveWorkoutExerciseType(exerciseType);
        }
    }
}
