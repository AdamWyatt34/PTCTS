using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("People")]
    public class People
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int personTypeID { get; set; }
        public DateTime dateEntered { get; set; }

        public People()
        {
            dateEntered = DateTime.Now;
        }

        public People(int ID, string firstName, string lastName, DateTime DOB, int personTypeID, DateTime dateEntered)
        {
            this.ID = ID;
            this.fName = firstName;
            this.lName = lastName;
            this.dateOfBirth = DOB;
            this.personTypeID = personTypeID;
            this.dateOfBirth = dateEntered;
        }

    }
}
