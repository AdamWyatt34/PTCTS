using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("Trainer")]
    public class Trainer
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int personTypeID { get; set; }
        public DateTime dateEntered { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string description { get; set; }

        public Trainer(int ID, string firstName, string lastName, DateTime DOB, int personTypeID, DateTime dateEntered,
           string userName, string passWord, string description)
        {
            this.ID = ID;
            this.fName = firstName;
            this.lName = lastName;
            this.dateOfBirth = DOB;
            this.personTypeID = personTypeID;
            this.dateEntered = dateEntered;
            this.userName = userName;
            this.passWord = passWord;
            this.description = description;
        }

        public Trainer()
        {

        }

        public Trainer(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;
        }

        public async void createBaseTrainers()
        {
            Trainer trainer1 = new Trainer(0, "Adam", "Wyatt", DateTime.Parse("6/1/1995"), 1, DateTime.Now, "test", "test", "New trainer specializing in functional fitness, bodybuilding, and weight loss.");
            Trainer trainer2 = new Trainer(0, "Jose", "Pinero", DateTime.Parse("11/22/1994"), 1, DateTime.Now, "jose", "pinero", "Experienced trainer who focuses on muscle mass and bodybuilding");
            Trainer trainer3 = new Trainer(0, "Patrick", "Ford", DateTime.Parse("4/6/1994"), 1, DateTime.Now, "patrickFord", "patFord", "Trainer who focuses on basketball conditioning");

            await App.Database.saveTrainerAsync(trainer1);
            await App.Database.saveTrainerAsync(trainer2);
            await App.Database.saveTrainerAsync(trainer3);
        
        }

    }

}
