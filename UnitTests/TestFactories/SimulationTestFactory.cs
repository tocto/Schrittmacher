using Phileas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestFactories
{
    public static class SimulationTestFactory
    {
        /// <summary>
        /// Generates simple test object with data points for x and y parameter
        /// </summary>
        /// <returns></returns>
        public static Simulation Make()
        {
            Simulation simulation = new Simulation();

            AddSimulationMetadata(simulation);

            FillMathModel(simulation.MathModel);

            simulation.Plots.Add(MakePlotData());

            return simulation;
        }

        private static void AddSimulationMetadata(Simulation simulation)
        {
            string simId = DateTime.Now.Ticks.ToString();

            simulation.Title = "Simulation " + simId;
            simulation.Note = "Note " + simId;
        }

        private static void FillMathModel(MathModel mathModel)
        {
            string mmId = DateTime.Now.ToString();

            mathModel.Name = "MathModel " + mmId;
            mathModel.Note = "Note " + mmId;
            mathModel.Text = "y = x \n x = 1";
        }

        private static PlotData MakePlotData()
        {
            PlotData data = new PlotData()
            {
                Title = "Diagram title",
                XAxisTitle = "x-Axis",
                YAxisTitle = "y-Axis",
                XParameterKey = "x",
                YParameterKey = "y",
                NumberOfSteps = 10
            };

            List<double> dataPoints = new List<double>();
            for (int i = 0; i < data.NumberOfSteps; i++) dataPoints.Add(1);

            data.DataPoints.Add(data.XParameterKey, dataPoints.ToList()); // .toList() ensures unique instances
            data.DataPoints.Add(data.YParameterKey, dataPoints.ToList());

            return data;
        }
    }
}
