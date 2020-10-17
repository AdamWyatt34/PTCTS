using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("ActivityLevel")]
    public class ActivityLevel
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string activityLevel { get; set; }
        public string activityLevelDescription { get; set; }

        public ActivityLevel()
        {

        }
        public ActivityLevel(int ID, string level, string description)
        {
            this.ID = ID;
            this.activityLevel = level;
            this.activityLevelDescription = description;
        }

        public void createDefaultActivityLevels()
        {
            ActivityLevel level = new ActivityLevel(0, "Sedentary","Little to no exercise");
            App.Database.saveActivityLevel(level);

            level = new ActivityLevel(0, "Moderate", "One to Two hours daily");
            App.Database.saveActivityLevel(level);

            level = new ActivityLevel(0, "Extremely", "Vigourous exercise for 2+ hours daily");
            App.Database.saveActivityLevel(level);

        }
    }
}
