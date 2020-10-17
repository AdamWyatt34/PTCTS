using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("ClientWorkouts")]
    public class ClientWorkouts
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int clientID { get; set; }
        public int workoutID { get; set; }
        public DateTime scheduledDate { get; set; }
        public DateTime completedDate { get; set; }

        public ClientWorkouts()
        {

        }

        public ClientWorkouts(int pkID, int cID, int wID, DateTime scheduledDate)
        {
            this.ID = pkID;
            this.clientID = cID;
            this.workoutID = wID;
            this.scheduledDate = scheduledDate.Date;
        }

        public ClientWorkouts(int pkID, int cID, int wID, DateTime scheduledDate, DateTime completedDate)
        {
            this.ID = pkID;
            this.clientID = cID;
            this.workoutID = wID;
            this.scheduledDate = scheduledDate.Date;
            this.completedDate = completedDate.Date;
        }
    }
}
