using LiveCharts;
using LiveCharts.Uwp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phileas.Model
{
    /// <summary>
    /// Fills a chart with data.
    /// </summary>
    public class Plotter
    {
        List<string> xData = new List<string>();

        ChartValues<double> yData = new ChartValues<double>();

        SeriesCollection SeriesCollection { get; set; } = null; // use the one associated to the cartesian chart object

        PlotData plotData = new PlotData();

        CartesianChart cartesianChart = null;

        Simulation simulation;

        /// <summary>
        /// Updates the chart with data points.
        /// </summary>
        /// <remarks>
        /// Thread must be started from UI/Main-Thread.
        /// </remarks>
        /// <param name="plotData"></param>
        /// <param name="cartesianChart"></param>
        public void Plot(Simulation simulation, PlotData plotData, CartesianChart cartesianChart)
        {
            if (simulation == null || plotData == null || cartesianChart == null) throw new ArgumentNullException();
            if (plotData.DataPoints == null) throw new ArgumentException("There are no data point entries.");

            // assign fields for global use
            this.simulation = simulation;
            this.plotData = plotData;
            this.cartesianChart = cartesianChart;
            this.SeriesCollection = cartesianChart.Series;

            ResetChart();

            MakeChart();
        }

        private void ResetChart()
        {
            cartesianChart.Series?.Clear();
            cartesianChart.AxisX?.Clear();
            cartesianChart.AxisY?.Clear();
        }

        private void MakeChart()
        {
            PrepareAxis();

            PrepareLineSeries();

            MakeData();

            AddDataToChart();
        }

        private void PrepareAxis()
        {
            Axis xAxis = new Axis()
            {
                Title = plotData.XAxisTitle,
                Labels = xData
            };

            cartesianChart.AxisX.Add(xAxis);

            Axis yAxis = new Axis()
            {
                Title = plotData.YAxisTitle,
                LabelFormatter = v => v.ToString()
            };

            cartesianChart.AxisY.Add(yAxis);
        }

        private void PrepareLineSeries()
        {
            SeriesCollection.Add( new LineSeries()
                {
                    Title = plotData.YParameterKey,
                    Values = yData,
                    LineSmoothness = Convert.ToDouble(plotData.IsLineSmothnessOn)
                });
        }

        private void MakeData()
        {
            this.plotData.DataPoints = CalcDataPoints(this.simulation, plotData.NumberOfSteps);
        }

        /// <summary>
        /// Calculates all data point based on the current math model.
        /// </summary>
        /// <param name="numberOfSteps"></param>
        /// <returns></returns>
        public Dictionary<string, List<double>> CalcDataPoints(Simulation simulation, uint numberOfSteps)
        {
            Calculator calculator = new Calculator();

            Dictionary<string, List<double>> results = calculator.Calc(simulation.MathModel, numberOfSteps); // exception might thrown

            return results;
        }

        private void AddDataToChart()
        {
            for (int i = 0; i < plotData.DataPoints[plotData.XParameterKey].Count; i++)
            {
                xData.Add(plotData.DataPoints[plotData.XParameterKey][i].ToString(CultureInfo.CreateSpecificCulture("de-DE")));
                yData.Add(plotData.DataPoints[plotData.YParameterKey][i]);
            }
        }
    }
}
