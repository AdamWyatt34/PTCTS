using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PTCTS.Services;

namespace PTCTS.Models
{
    [Table("ClientMeasurements")]
    public class ClientMeasurements
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public int clientID { get; set; }
        public DateTime measurementDate { get; set; }
        public int weight { get; set; }
        public decimal bodyFatPercentage { get; set; }

        public ClientMeasurements()
        {

        }

        public ClientMeasurements(int pkID, int clientID, int weight, decimal bodyFatPercent, DateTime dateMeasure)
        {
            this.ID = pkID;
            this.clientID = clientID;
            this.weight = weight;
            this.bodyFatPercentage = bodyFatPercent;
            this.measurementDate = dateMeasure;
        }

        public string validateMeasurement(ClientMeasurements clientMeasurements)
        {
            string validation = null;

            if (Validation.nullCheck(clientMeasurements.weight.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Client Weight, " + Environment.NewLine;
            }
            else if (clientMeasurements.weight <= 0)
            {
                validation = validation + "Client Weight must be greater than 0, "  + Environment.NewLine;
            }

            if (Validation.nullCheck(clientMeasurements.bodyFatPercentage.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Client Body Fat Percentage, " + Environment.NewLine;
            }
            else if (clientMeasurements.bodyFatPercentage > 100 || clientMeasurements.bodyFatPercentage < 1)
            {
                validation = validation + "Body Fat Percentage must be a value between 1 and 100, " + Environment.NewLine;
            }

            if (Validation.nullCheck(clientMeasurements.measurementDate.ToString()))
            {
                validation = Validation.fieldNullCheck(validation);
                validation = validation + "Measurement Date, " + Environment.NewLine;
            }

            if (validation != null)
            {
                validation = validation.Substring(0, validation.Length - 3);
            }

            return validation;

        }
    }
}
