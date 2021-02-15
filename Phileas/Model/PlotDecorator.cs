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
    public class PlotDecorator
    {
        List<string> xData = new List<string>();

        ChartValues<double> yData = new ChartValues<double>();

        SeriesCollection SeriesCollection { get; set; } = null; // use the one associated to the cartesian chart object

        PlotData plotData = new PlotData();

        CartesianChart cartesianChart = null;

        /// <summary>
        /// Update plot in the ui.
        /// </summary>
        /// <remarks>
        /// Must run in ui thread.
        /// </remarks>
        /// <param name="plotData"></param>
        /// <param name="cartesianChart"></param>
        public void DecoratePlot(PlotData plotData, CartesianChart cartesianChart)
        {
            if (plotData == null || cartesianChart == null) throw new ArgumentNullException();
            if (plotData.DataPoints == null) throw new ArgumentException("There are no data point entries.");

            this.plotData = plotData;
            this.cartesianChart = cartesianChart;
            this.SeriesCollection = cartesianChart.Series;

            MakeChart();
        }

        private void MakeChart()
        {
            PrepareAxis();

            PrepareLineSeries();

            AddDataToChart();
        }

        private void PrepareAxis()
        {
            Axis xAxis = new Axis()
            {
                Title = plotData.XAxisTitle,
                Labels = xData
            };

            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(xAxis);

            Axis yAxis = new Axis()
            {
                Title = plotData.YAxisTitle,
                LabelFormatter = v => v.ToString()
            };

            cartesianChart.AxisY.Clear();
            cartesianChart.AxisY.Add(yAxis);
        }

        private void PrepareLineSeries()
        {
            SeriesCollection.Add( new LineSeries()
                {
                    Title = plotData.yParameterKey,
                    Values = yData,
                    LineSmoothness = 0
                });
        }

        private void AddDataToChart()
        {
            for (int i = 0; i < plotData.DataPoints[plotData.xParameterKey].Count; i++)
            {
                xData.Add(plotData.DataPoints[plotData.xParameterKey][i].ToString(CultureInfo.CreateSpecificCulture("de-DE")));
                yData.Add(plotData.DataPoints[plotData.yParameterKey][i]);
            }
        }
    }
}
