using LiveCharts.Uwp;
using Phileas.Model;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Phileas.Views.Dialogs
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
                Plotter plotter = new Plotter();
                try
                {
                    plotter.Plot(plotData, cartesianChart);
                }
                catch (Exception e)
                {
                    // do nothing
                }
            }           
        }

        private void UpdatePlotDataProperties()
        {
            plotData.Title = this.TextBox_Title.Text;
            plotData.XAxisTitle = this.TextBox_xAxisTitle.Text;
            plotData.YAxisTitle = this.TextBox_yAxisTitle.Text;
            plotData.XParameterKey = this.ComboBox_xParamater.SelectedItem.ToString();
            plotData.YParameterKey = this.ComboBox_yParamater.SelectedItem.ToString();
            plotData.NumberOfSteps = Convert.ToUInt32(this.TextBox_NumberOfSteps.Text) is uint number ? number : plotData.NumberOfSteps;
            plotData.IsLineSmothnessOn = this.ToggleSwith_LineSmothness.IsOn;
        }

        private void TextBox_NumberOfSteps_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                uint number = (uint) Math.Ceiling(Convert.ToDouble(this.TextBox_NumberOfSteps.Text));
                this.TextBox_NumberOfSteps.Text =  number.ToString();
            }
            catch(Exception exception)
            {
                this.TextBox_NumberOfSteps.Text = this.plotData.NumberOfSteps.ToString();
            }
        }
    }
}
