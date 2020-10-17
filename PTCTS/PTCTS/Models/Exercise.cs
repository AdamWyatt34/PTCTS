using System;
using System.Collections.Generic;
using System.Text;
using PTCTS.Services;
using PTCTS.ViewModels;
using SQLite;
namespace PTCTS.Models
{
    [Table("Exercise")]
    public class Exercise
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string exerciseName { get; set; }
        public string description { get; set; }
        public int exerciseTypeID { get; set; }
        public int equipmentID { get; set; }

        public Exercise()
        {

        }

        public Exercise(int ID, string name, string description, int exerciseTypeID, int equipment)
        {
            this.ID = ID;
            this.exerciseName = name;
            this.description = description;
            this.exerciseTypeID = exerciseTypeID;
            this.equipmentID = equipment;
        }

        public Exercise(ExerciseViewModel exerciseViewModel)
        {
            this.ID = exerciseViewModel.ID;
            this.description = exerciseViewModel.description;
            this.exerciseName = exerciseViewModel.exerciseName;
            this.exerciseTypeID = App.Database.getExerciseTypeIDFromType(exerciseViewModel.exerciseType);
            this.equipmentID = App.Database.getEquipmentIDFromEquipName(exerciseViewModel.equipment);
        }

        public void addExercises()
        {
            Exercise exercise = new Exercise(0, "Barbell Curl", "Arm exercise to help build biceps usually performed in moderate to high reps, such as 8-12 reps per set", 5,1);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Barbell Bench Press", "Chest and tricep exercise that helps build muscle and strength. Can be performed for a variety of reps to focus on different strengths", 5,1);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Barbell Deadlift", "Compound exercise used to develop strength and size in the posterior chain. Targets back, quads, and hamstrings and performed for strength.", 5,1);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Triceps Pushdown", "Popular exercise for targeting the triceps. Usually performed for moderate to high reps such as 8-12 or more.", 5,2);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Standing Shoulder Press", "Classic shoulder building exercise, standing engages the core while performing the exercise. Great for building strength and shoulder definition", 5,3);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Shrug", "Shrugs are a popular movement to build and strengthen the trapezius muscles. Often used to build mass in the trapezius muscle.", 5,1);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Burpee to pull-up", "Add-on to the classic burpee movement, where the jump is replaced with a pull-up. Engages the lats and is used in circuit and HIIT workouts.", 3,4);
            App.Database.saveExercise(exercise);

            exercise = new Exercise(0, "Running", "Performed on a treadmill or on pavement/grass, running increases cardiovascular strength. Excelent for general health and fat-loss", 1,5);
            App.Database.saveExercise(exercise);
            //bench, squat, deadlift, row, triceps pushdown, shoulder press, shrugs, pullup, jogging
        }

        public string validateExercises(Exercise exercise)
        {
            string validation = null;

            if(Validation.nullCheck(exercise.exerciseName))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise Name, " + Environment.NewLine;
            }

            if(Validation.nullCheck(exercise.description))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise Description, " + Environment.NewLine;
            }

            if (exercise.exerciseTypeID == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Exercise Type, " + Environment.NewLine;
            }

            if (exercise.equipmentID == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Equipment Needed, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }
    }
}
