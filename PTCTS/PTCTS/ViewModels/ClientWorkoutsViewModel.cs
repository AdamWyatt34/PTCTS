using System;
using System.Collections.Generic;
using System.Text;
using PTCTS.Models;

namespace PTCTS.ViewModels
{
    public class ClientWorkoutsViewModel
    {
        public int ID { get; set; }
        public int clientID { get; set; }
        public int workoutID { get; set; }
        public DateTime scheduledDate { get; set; }
        public DateTime completedDate { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public ClientWorkoutsViewModel()
        {

        }

        public ClientWorkoutsViewModel(int pkID, int cID, int wID, DateTime scheduledDate, DateTime completedDate, string wkName, string wkDescription)
        {
            this.ID = pkID;
            this.clientID = cID;
            this.workoutID = workoutID;
            this.scheduledDate = scheduledDate.Date;
            this.completedDate = completedDate.Date;
            this.name = wkName;
            this.description = wkDescription;
        }

        public ClientWorkoutsViewModel(ClientWorkouts clientWorkouts)
        {
            Workout workout = App.Database.loadWorkoutFromID(clientWorkouts.workoutID);

            this.ID = clientWorkouts.ID;
            this.clientID = clientWorkouts.clientID;
            this.workoutID = clientWorkouts.workoutID;
            this.scheduledDate = clientWorkouts.scheduledDate;
            this.completedDate = clientWorkouts.completedDate;
            this.name = workout.name;
            this.description = workout.description;
        }

    }
}
