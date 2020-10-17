using PTCTS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTCTS.ViewModels
{
    public class WorkoutPlanViewModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string planLevel { get; set; }
        public string workoutPlanType { get; set; }
        public int planLength { get; set; }

        public WorkoutPlanViewModel()
        {

        }

        public WorkoutPlanViewModel(int pkID, string name, string description, string level, string planType, int len)
        {
            this.ID = pkID;
            this.name = name;
            this.description = description;
            this.planLevel = level;
            this.workoutPlanType = planType;
            this.planLength = len;
        }

    }
}
