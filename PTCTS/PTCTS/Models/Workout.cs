using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PTCTS.Services;
using PTCTS.ViewModels;
namespace PTCTS.Models
{
    [Table("Workout")]
    public class Workout
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int availableEquipmentID { get; set; }
        public int estimatedMinutes { get; set; }

        public Workout()
        {

        }
        public Workout(int pkID, string wkName, string wkDescription, int equipmentID, int minutes)
        {
            this.ID = pkID;
            this.name = wkName;
            this.description = wkDescription;
            this.availableEquipmentID = equipmentID;
            this.estimatedMinutes = minutes;
        }

        public Workout(WorkoutViewModel workout)
        {
            this.ID = workout.ID;
            this.name = workout.name;
            this.description = workout.description;
            this.availableEquipmentID = App.Database.getAvailableEquipmentIDFromType(workout.availableEquipment);
            this.estimatedMinutes = workout.estimatedMinutes;
        }
        public void createTestWorkout()
        {
            Workout testWorkout = new Workout(0, "Workout Test", "Test workout", 1, 45);
            App.Database.saveWorkout(testWorkout);

            Workout restDay = new Workout(0, "Rest Day", "Client Rest Day", 3, 0);
            App.Database.saveWorkout(restDay);
        }

        public string validateWorkout(Workout workout)
        {
            string validation = null;

            if (Validation.nullCheck(workout.name))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Workout Name, " + Environment.NewLine;
            }

            if (Validation.nullCheck(workout.description))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Workout Description, " + Environment.NewLine;
            }

            if (workout.availableEquipmentID == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Available Needed, " + Environment.NewLine;
            }

            if (workout.estimatedMinutes == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Estimated Minutes, " + Environment.NewLine;
            }
            else if(workout.estimatedMinutes <= 0)
            {
                validation = validation + "Estimated Minutes must be greater than 0, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }
    }
}
