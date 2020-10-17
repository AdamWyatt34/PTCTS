using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("WorkoutPlanType")]
    public class WorkoutPlanType
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string Type { get; set; }

        public WorkoutPlanType()
        {

        }

        public WorkoutPlanType(int pkID, string type)
        {
            this.ID = pkID;
            this.Type = type;
        }

        public void addWorkoutPlanTypes()
        {
            WorkoutPlanType planType = new WorkoutPlanType(0, "Muscle Building");
            App.Database.saveWorkoutPlanType(planType);

            planType = new WorkoutPlanType(0, "Weight Loss");
            App.Database.saveWorkoutPlanType(planType);

            planType = new WorkoutPlanType(0, "Gain Strength");
            App.Database.saveWorkoutPlanType(planType);

            planType = new WorkoutPlanType(0, "Get Fit");
            App.Database.saveWorkoutPlanType(planType);

            planType = new WorkoutPlanType(0, "Performance");
            App.Database.saveWorkoutPlanType(planType);
        }
    }
}
