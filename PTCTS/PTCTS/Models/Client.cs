using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PTCTS.Services;

namespace PTCTS.Models
{
    [Table("Client")]
    public class Client
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int personTypeID { get; set; }
        public DateTime dateEntered { get; set; }
        public string shortTermGoal { get; set; }
        public string longTermGoal { get; set; }
        public int activityLevelID { get; set; }
        public int trainerID { get; set; }

        public Client()
        {

        }

        public Client(int ID, string firstName, string lastName, DateTime DOB, int personTypeID, DateTime dateEntered, 
            string shortGoal, string longGoal, int activityLevel, int trainerID)
        {
            this.ID = ID;
            this.fName = firstName;
            this.lName = lastName;
            this.dateOfBirth = DOB;
            this.personTypeID = personTypeID;
            this.dateEntered = dateEntered;
            this.shortTermGoal = shortGoal;
            this.longTermGoal = longGoal;
            this.activityLevelID = activityLevel;
            this.trainerID = trainerID;
        }

        public void createBaseClients()
        {
            Client client1 = new Client(0, "Joe", "Smith", DateTime.Parse("1/22/1966"), 2, DateTime.Now, "Lose Weight", "Reach 12% bodyfat", 1,1);
            Client client2 = new Client(0, "Mike", "Johnson", DateTime.Parse("10/1/1999"), 2, DateTime.Now, "Add Muscle", "Compete in bodybuilding competitions", 2,1);
            Client client3 = new Client(0, "Jane", "Doe", DateTime.Parse("7/12/1986"), 2, DateTime.Now, "Be fitter", "Maintain healthy exercise regimine",3,3);

            App.Database.saveClientAsync(client1);
            App.Database.saveClientAsync(client2);
            App.Database.saveClientAsync(client3);
        
        }

        public string validateClient(Client client)
        {
            string validation = null;

            if (Validation.nullCheck(client.fName))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "First Name, " + Environment.NewLine;
            }

            if (Validation.nullCheck(client.lName))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Last Name, " + Environment.NewLine;
            }

            if (Validation.nullCheck(client.dateOfBirth.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Date of Birth, " + Environment.NewLine;
            }

            if (Validation.nullCheck(client.shortTermGoal))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Short Term Goal, " + Environment.NewLine;
            }

            if (Validation.nullCheck(client.longTermGoal))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Long Term Goal, " + Environment.NewLine;
            }

            if (client.activityLevelID == 0)
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Activity Level, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;
        }
    }
}
