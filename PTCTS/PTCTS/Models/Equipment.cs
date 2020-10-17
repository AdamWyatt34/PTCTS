using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("Equipment")]
    public class Equipment
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string equipmentName { get; set; }
        
        public Equipment()
        {

        }
        public Equipment(int pkID, string name)
        {
            this.ID = pkID;
            this.equipmentName = name;
        }

        public void addDefaultEquipment()
        {
            Equipment equipment = new Equipment(0, "Barbell"); //1
            App.Database.saveEquipment(equipment);

            equipment = new Equipment(0, "Cable"); //2
            App.Database.saveEquipment(equipment);

            equipment = new Equipment(0, "Dumbbell"); //3
            App.Database.saveEquipment(equipment);

            equipment = new Equipment(0, "Body Only"); //4
            App.Database.saveEquipment(equipment);

            equipment = new Equipment(0, "Machine"); //5
            App.Database.saveEquipment(equipment);
        }
    }
}
