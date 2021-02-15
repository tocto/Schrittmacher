using LiveCharts;
using LiveCharts.Uwp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phileas.Model
{
    public class PlotFactory
    {
        List<string> xData = new List<string>();

        ChartValues<double> yData = new ChartValues<double>();

        SeriesCollection SeriesCollection { get; set; } = new SeriesCollection();

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
        public void MakePlot(PlotData plotData, CartesianChart cartesianChart)
        {
            if (plotData == null || cartesianChart == null) throw new ArgumentNullException();
            if (plotData.DataPoints == null) throw new ArgumentException("There are no data point entries.");

            this.plotData = plotData;
            this.cartesianChart = cartesianChart;

            MakeChart();
        }

        private void MakeChart()
        {
            PrepareAxis();

            FillChartWithData();
        }

        private void PrepareAxis()
        {
            // absziss axis
            Axis xAxis = new Axis()
            {
                Title = plotData.YAxisTitle,
                Labels = xData
            };

            cartesianChart.AxisX.Add(xAxis);

            // ordinate axis
            LineSeries visualData = new LineSeries()
            {
                Title = plotData.XAxisTitle,
                Values = yData
            };

            SeriesCollection.Add(visualData);
        }

        private void FillChartWithData()
        {
            for (int i = 0; i < plotData.DataPoints[plotData.xParameterKey].Count; i++)
            {
                xData.Add(plotData.DataPoints[plotData.xParameterKey][i].ToString(CultureInfo.CreateSpecificCulture("de-DE")));
                yData.Add(plotData.DataPoints[plotData.yParameterKey][i]);
            }

            cartesianChart.Series = this.SeriesCollection;

            cartesianChart.Update();
        }
    }
}
