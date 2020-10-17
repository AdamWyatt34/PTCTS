using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PTCTS.ViewModels
{
    public class ExerciseViewModel
    {
        public int ID { get; set; }
        public string exerciseName { get; set; }
        public string description { get; set; }
        public string exerciseType { get; set; }
        public string equipment { get; set; }

        public ExerciseViewModel()
        {

        }

        public ExerciseViewModel(int pkID, string name, string desc, string type, string equipment)
        {
            this.ID = pkID;
            this.exerciseName = name;
            this.description = desc;
            this.exerciseType = type;
            this.equipment = equipment;
        }
    }
}
