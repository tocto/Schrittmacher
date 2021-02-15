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
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (this.plotData != null && this.cartesianChart != null)
            {
                PlotDecorator plotFactory = new PlotDecorator();
                plotFactory.DecoratePlot(plotData, cartesianChart);
            }           
        }

        private void ToggleSwith_LineSmothness_Toggled(object sender, RoutedEventArgs e)
        {

        }
    }
}
