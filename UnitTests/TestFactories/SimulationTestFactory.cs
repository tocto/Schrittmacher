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
            simulation.Plots.Add(MakePlotData());

            return simulation;
        }

        private static void AddSimulationMetadata(Simulation simulation)
        {
            string simId = DateTime.Now.Ticks.ToString();

            simulation.Name = "Simulation " + simId;
            simulation.Note = "Note " + simId;
        }

        private static void FillMathModel(MathModel mathModel)
        {
            mathModel.Text = "y = x \n x = 1";
        }

        private static PlotData MakePlotData()
        {
            PlotData data = new PlotData()
            {
                Name = "Diagram title",
                XAxisTitle = "x-Axis",
                YAxisTitle = "y-Axis",
                XParameter = "x",
                YParameter = "y",
            };

            List<double> dataPoints = new List<double>();
            for (int i = 0; i < 10; i++) dataPoints.Add(1);

            data.DataPoints.Add(data.XParameter, dataPoints.ToList()); // .toList() ensures unique instances
            data.DataPoints.Add(data.YParameter, dataPoints.ToList());

            return data;
        }
    }
}
