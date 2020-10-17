using System;
using System.Collections.Generic;
using System.Text;

namespace PTCTS.ViewModels
{
    public class ClientBodyFatPercentageReportViewModel
    {
        public DateTime measurementDate { get; set; }
        public decimal weight { get; set; }

        public ClientBodyFatPercentageReportViewModel()
        {

        }

        public ClientBodyFatPercentageReportViewModel(DateTime iDate, decimal iBodyFat)
        {
            this.measurementDate = iDate.Date;
            this.weight = iBodyFat;
        }

    }

}
