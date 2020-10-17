using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PTCTS.Models
{
    [Table("Muscle")]
    public class Muscle
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string muscleType { get; set; }

        public Muscle()
        {

        }
        public Muscle (int pkID, string type)
        {
            this.ID = pkID;
            this.muscleType = type;
        }

        public void addDefaultMuscles()
        {
            Muscle muscle = new Muscle(0, "Bicep"); //1
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Pectoral (Chest)"); //2
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Deltoid (Shoulder)"); //3
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Lats (Back)"); //4
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Quadriceps"); //5
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Hamstrings"); //6
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Triceps"); //7
            App.Database.saveMuscle(muscle);

            muscle = new Muscle(0, "Trapezius"); //8
            App.Database.saveMuscle(muscle);
        }
    }
}
