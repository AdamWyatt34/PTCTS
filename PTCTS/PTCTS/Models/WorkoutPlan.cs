using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PTCTS.Services;
using PTCTS.ViewModels;

namespace PTCTS.Models
{
    [Table("WorkoutPlan")]
    public class WorkoutPlan
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string planLevel { get; set; }
        public int workoutPlanType { get; set; }
        public int planLength { get; set; }

        public WorkoutPlan()
        {

        }

        public WorkoutPlan(int pkID, string planName, string description, string planLevel, int planType)
        {
            this.ID = pkID;
            this.name = planName;
            this.description = description;
            this.planLevel = planLevel;
            this.workoutPlanType = planType;
            this.planLength = App.Database.workoutPlanDays(pkID);
        }

        public WorkoutPlan(WorkoutPlanViewModel viewModel)
        {
            this.ID = viewModel.ID;
            this.name = viewModel.name;
            this.description = viewModel.description;
            this.planLevel = viewModel.planLevel;
            this.workoutPlanType = App.Database.getWorkoutPlanTypeIDFromType(viewModel.workoutPlanType);
            this.planLength = viewModel.planLength;
        }

        public void defaultWorkoutPlan()
        {
            WorkoutPlan plan = new WorkoutPlan(0, "Beginner Week", "A program to introduce a new person into fitness.", "Beginner", 4);

            App.Database.saveWorkoutPlan(plan);
        }

        public string validatePlan(WorkoutPlan workoutPlan)
        {
            string validation = null;

            if (Validation.nullCheck(workoutPlan.name))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Workout Plan Name, " + Environment.NewLine;
            }

            if (Validation.nullCheck(workoutPlan.description))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Workout Plan Description, " + Environment.NewLine;
            }

            if (Validation.nullCheck(workoutPlan.planLevel))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Workout Plan Level, " + Environment.NewLine;
            }

            if (workoutPlan.workoutPlanType == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Workout Plan Type, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }
    }
}
