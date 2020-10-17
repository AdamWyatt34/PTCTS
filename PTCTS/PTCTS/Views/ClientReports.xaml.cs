using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Forms;
using PTCTS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTCTS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientReports : ContentPage
    {
        Client cClient;
        public ClientReports(Client client)
        {
            InitializeComponent();
            cClient = client;
        }

        protected override void OnAppearing()
        {
            reportPicker.Items.Add("Weight History");
            reportPicker.Items.Add("Body Fat History");
            this.Title = "Client Reports";
            base.OnAppearing();
        }

        private void reportPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reportPicker.SelectedIndex == 0) //Weight History
            {
                var entries = App.Database.getClientWeightHistory(cClient);
                var chart = new LineChart() 
                { 
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 8,
                    PointMode = PointMode.Square,
                    PointSize = 18,
                    LineAreaAlpha = 0,
                    LabelTextSize = 32,
                    TextDirection = Topten.RichTextKit.TextDirection.LTR,
                    ValueLabelOrientation = Orientation.Horizontal,
                    LabelOrientation = Orientation.Horizontal,
                };
                chartView.Chart = chart;
                chartView.IsVisible = true;
                chartViewTitle.Text = cClient.fName + " " + cClient.lName + " Weight History";
                chartViewTitle.IsVisible = true;
                column1.Text = "Measurement Date";
                column2.Text = "Weight";
                chartDataListView.ItemsSource = App.Database.getClientWeightHistoryData(cClient);
                chartDataListView.IsVisible = true;

            }
            else if(reportPicker.SelectedIndex == 1) //body fat history
            {
                var entries = App.Database.getClientBodyFatHistory(cClient);
                var chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 8,
                    PointMode = PointMode.Square,
                    PointSize = 18,
                    LineAreaAlpha = 0,
                    LabelTextSize = 32,
                    TextDirection = Topten.RichTextKit.TextDirection.LTR,
                    ValueLabelOrientation = Orientation.Horizontal,
                    LabelOrientation = Orientation.Horizontal,
                };
                chartView.Chart = chart;
                chartView.IsVisible = true;
                chartViewTitle.Text = cClient.fName + " " + cClient.lName + " Body Fat History";
                chartViewTitle.IsVisible = true;
                column1.Text = "Measurement Date";
                column2.Text = "Body Fat %";
                chartDataListView.ItemsSource = App.Database.getClientBodyFatHistoryData(cClient);
                chartDataListView.IsVisible = true;
            }
            else
            {
                chartDataListView.IsVisible = false;
                chartView.IsVisible = false;
            }
        }
    }
}