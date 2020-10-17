using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("TimeType")]
    public class TimeType
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string Type { get; set; }

        public TimeType()
        {

        }

        public TimeType(int pkID, string type)
        {
            this.ID = pkID;
            this.Type = type;
        }

        public void createTimeTypes()
        {
            TimeType timeType = new TimeType(0, "Seconds"); //1
            App.Database.saveTimeType(timeType);

            timeType = new TimeType(0, "Minutes"); //2
            App.Database.saveTimeType(timeType);
        }
    }
}
