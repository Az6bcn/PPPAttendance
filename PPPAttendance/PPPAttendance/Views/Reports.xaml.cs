using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microcharts;
using PPPAttendance.Dtos;
using PPPAttendance.Services.TableServiceClient;
using SkiaSharp;
//using SkiaSharp;
using Xamarin.Forms;

namespace PPPAttendance.Views
{
    public partial class Reports : ContentPage
    {
        public Reports()
        {
            InitializeComponent();
        }

        void OnDateSelected(object sender,
                            DateChangedEventArgs args)
        {
        }

        async void GetReports(Object sender,
                              EventArgs e)
        {
            var startDate = startDatePicker.Date;
            var endDate = endDatePicker.Date;

            var client = new AzureTableClient();

            var report = await client.GetReport(startDate, endDate);

            if (report.Any())
            {
                var chart = new LineChart() { Entries = GetEntries(report) };

                chartLabel.IsVisible = true;

                chart.LabelTextSize = 10;
                this.chartView.Chart = chart;
            }
        }

        private ChartEntry[] GetEntries(IEnumerable<AttendanceReportDto> report)
        {
            var entries = new List<ChartEntry>();

            foreach (var value in report)
            {
                entries.Add(
                            new ChartEntry(value.Men)
                            {
                                Label = nameof(value.Men),
                                Color = SKColor.Parse("#FF0000"),
                                ValueLabel = value.Men.ToString(),
                            });
                entries.Add(
                            new ChartEntry(value.Women)
                            {
                                Label = nameof(value.Women),
                                Color = SKColor.Parse("77d065"),
                                ValueLabel = value.Women.ToString(),
                            });
                entries.Add(
                            new ChartEntry(value.Children)
                            {
                                Label = nameof(value.Children),
                                Color = SKColor.Parse("#3498db"),
                                ValueLabel = value.Children.ToString(),
                            });
                entries.Add(
                            new ChartEntry(value.Total)
                            {
                                Label
                                    = $"{nameof(value.Total)} - {value.Date.ToString("d", new CultureInfo("en-GB"))}",
                                Color = SKColor.Parse("#696969"),
                                ValueLabel = value.Total.ToString(),
                            });
            }

            return entries.ToArray();
        }
    }
}