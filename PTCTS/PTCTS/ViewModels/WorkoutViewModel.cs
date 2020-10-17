using System;
using System.Collections.Generic;
using System.Text;

namespace PTCTS.ViewModels
{
    public class WorkoutViewModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string availableEquipment { get; set; }
        public int estimatedMinutes { get; set; }

        public WorkoutViewModel()
        {

        }
        public WorkoutViewModel(int pkID, string wName, string wDesc, string wEquip, int minutes)

        {
            this.ID = pkID;
            this.name = wName;
            this.description = wDesc;
            this.availableEquipment = wEquip;
            this.estimatedMinutes = minutes;
        }
    }
}
