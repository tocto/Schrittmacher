
using LiveCharts;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Phileas.Controls.Plots
{
    public sealed partial class BasicLineChart : UserControl
    {
        public string Title = "Diagramm";

        string xParameter = "t";

        string yParameer = "s";

        int stepsCount = 100;

        int stepIncrement = 1;

        Calculator calculator = new Calculator();

        public MathModel MathModel { get; set; } = null;

        List<string> xData = new List<string>();

        ChartValues<double> yData = new ChartValues<double>();

        SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();

        public BasicLineChart()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LineSeries visualData = new LineSeries()
            {
                Title = this.Title,
                Values = yData
            };

            CartesienChart.AxisX.Add(new Axis() { Labels = xData });

            SeriesCollection.Add(visualData);

            MakeChart();
        }

        private void MakeChart()
        {
            var results = calculator.Calc(App.Simulation.MathModel, this.xParameter, yParameer);

            foreach (var data in results)
            {
                xData.Add(data.Item1.ToString());
                yData.Add(data.Item2);
            }

            this.CartesienChart.Update();
        }
    } 
    
}