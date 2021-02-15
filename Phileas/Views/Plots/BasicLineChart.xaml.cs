
using LiveCharts;
using LiveCharts.Uwp;
using Phileas.Model;
using Phileas.Views.Dialogs;
using System;
using System.Collections.Generic;
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

namespace Phileas.Views.Plots
{
    public sealed partial class BasicLineChart : UserControl
    {
        PlotData plotData = new PlotData();

        SeriesCollection SeriesCollection = new SeriesCollection();

        public BasicLineChart()
        {
            this.InitializeComponent();
        }

        private void MakeChart()
        {
            PlotDecorator plotFactory = new PlotDecorator();
            plotFactory.DecoratePlot(this.plotData, CartesienChart);
        }

        private void UserControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue != null && args.NewValue as PlotData != this.plotData)
            {
                this.plotData = args.NewValue as PlotData;
                MakeChart();
            }
        }

        private void AppBarButton_Delete_Click(object sender, RoutedEventArgs e)
        {
            App.Simulation.Plots.Remove(plotData);
        }

        private async void AppBarButton_Edit_ClickAsync(object sender, RoutedEventArgs e)
        {
            PlotEditingDialog dialog = new PlotEditingDialog(this.CartesienChart, plotData);
            await dialog.ShowAsync();
        }

        private void AppBarButton_Sync_Click(object sender, RoutedEventArgs e)
        {
            uint numberOfSteps = (uint) this.plotData.DataPoints.Values.First().Count - 1;

            this.plotData.DataPoints = App.Simulation.CalcDataPoints(numberOfSteps);
            
            MakeChart();
        }
    } 
    
}