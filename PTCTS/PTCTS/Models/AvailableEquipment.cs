using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PTCTS.Models
{
    [Table("AvailableEquipment")]
    public class AvailableEquipment
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public string equipment { get; set; }

        public AvailableEquipment()
        {

        }
        public AvailableEquipment(int pkID, string equip)
        {
            this.ID = pkID;
            this.equipment = equip;
        }

        public void addDefaultAvailableEquipment()
        {
            AvailableEquipment equipment = new AvailableEquipment(0, "Full Gym");
            App.Database.saveAvailableEquipment(equipment);

            equipment = new AvailableEquipment(0, "Minimal");
            App.Database.saveAvailableEquipment(equipment);

            equipment = new AvailableEquipment(0, "No Equipment");
            App.Database.saveAvailableEquipment(equipment);
        }
    }
}
