using LiveCharts.Uwp;
using Schrittmacher.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Schrittmacher.Views.Dialogs
{
    public sealed partial class PlotEditingDialog : ContentDialog
    {
        CartesianChart cartesianChart;

        PlotData plotData;

        public PlotEditingDialog(CartesianChart chart, PlotData plotData)
        {
            this.cartesianChart = chart;
            this.plotData = plotData;

            this.InitializeComponent();

            ChosePreSettings();
        }

        private void ChosePreSettings()
        {
            if (plotData != null && plotData.DataPoints != null && plotData.DataPoints.Keys.Count() >= 2)
            {
                var keyList = plotData.DataPoints.Keys.ToList();
                this.ComboBox_yParamater.SelectedItem = keyList[0];
                this.ComboBox_xParamater.SelectedItem = keyList[1];
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            UpdatePlotDataProperties();


            if (this.plotData != null && this.cartesianChart != null)
            {
                PlotDecorator plotter = new PlotDecorator();
                try
                {
                    plotter.Plot(plotData, cartesianChart);
                }
                catch
                {
                    // do nothing
                }
            }           
        }

        private void UpdatePlotDataProperties()
        {
            plotData.Name = this.TextBox_Title.Text;
            plotData.XAxisTitle = this.TextBox_xAxisTitle.Text;
            plotData.YAxisTitle = this.TextBox_yAxisTitle.Text;
            plotData.XParameter = this.ComboBox_xParamater.SelectedItem.ToString();
            plotData.YParameter = this.ComboBox_yParamater.SelectedItem.ToString();
            //plotData.NumberOfSteps = Convert.ToUInt32(this.NumberBox_Steps.Value) is uint number ? number : plotData.NumberOfSteps;
            //plotData.IsLineSmothnessOn = this.ToggleSwith_LineSmothness.IsOn;
        }
    }
}
