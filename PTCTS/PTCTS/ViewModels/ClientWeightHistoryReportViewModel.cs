using System;
using System.Collections.Generic;
using System.Text;

namespace PTCTS.ViewModels
{
    public class ClientWeightHistoryReportViewModel
    {
        public DateTime measurementDate { get; set; }
        public int weight { get; set; }

        public ClientWeightHistoryReportViewModel()
        {

        }

        public ClientWeightHistoryReportViewModel(DateTime iDate, int iWeight)
        {
            this.measurementDate = iDate;
            this.weight = iWeight;
        }

    }
}
