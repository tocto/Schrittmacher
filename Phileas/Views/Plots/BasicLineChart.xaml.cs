
using LiveCharts;
using LiveCharts.Uwp;
using Schrittmacher.Model;
using Schrittmacher.Views.Dialogs;
using Schrittmacher.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Schrittmacher.Views.Plots
{
    public sealed partial class BasicLineChart : UserControl
    {
        PlotData plotData = new PlotData();

        SeriesCollection SeriesCollection = new SeriesCollection();

        public event EventHandler DeletionRequested;

        public BasicLineChart()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Updates the visuals in the chart.
        /// </summary>
        public void UpdateChart()
        {
            if (plotData.XParameter != string.Empty
                    && plotData.YParameter != string.Empty)
            {
                PlotDecorator plotter = new PlotDecorator();
                plotter.Plot(this.plotData, CartesienChart);
            }
        }

        private void UserControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue != null && args.NewValue is PlotData data && data != this.plotData)
            {
                this.plotData = args.NewValue as PlotData;

                UpdateChart();
            }
        }

        private void AppBarButton_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeletionRequested?.Invoke(this, null);
        }

        private async void AppBarButton_Edit_ClickAsync(object sender, RoutedEventArgs e)
        {
            PlotEditingDialog dialog = new PlotEditingDialog(plotData);
            await dialog.ShowAsync();

            UpdateChart();
        }
    } 
    
}